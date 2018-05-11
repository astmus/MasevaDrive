using Newtonsoft.Json;
using System;
using CloudSync.Converters;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using CloudSync.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace CloudSync.OneDrive
{
	public class OneDriveClient
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		#region Static functionality

		public static string ClientID = "32171e35-694f-4481-a8bc-0498cb7da487";
		public static string RedirectUri = "msal32171e35-694f-4481-a8bc-0498cb7da487://auth";
		public static string GetAuthorizationRequestUrl()
		{
			return String.Format("https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id={0}&scope=offline_access user.read files.readwrite.all&response_type=code&redirect_uri={1}", ClientID, RedirectUri);
		}

		private static string MakeAcquireTokenByAuthorizationCodeContent(string code)
		{
			return String.Format("client_id={0}&redirect_uri={1}&code={2}&grant_type=authorization_code", ClientID, RedirectUri, code);
		}

		private static string MakeRefreshTokenRequestBody(string refreshToken)
		{
			return String.Format("client_id={0}&redirect_uri={1}&refresh_token={2}&grant_type=refresh_token",ClientID,RedirectUri,refreshToken);
		}

		public static OneDriveClient AcquireUserByAuthorizationCode(string code)
		{
			string userResult;
			using (var wc = new WebClient())
			{
				string body = OneDriveClient.MakeAcquireTokenByAuthorizationCodeContent(code);
				wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
				userResult = wc.UploadString("https://login.microsoftonline.com/common/oauth2/v2.0/token", body);
			}
			try
			{
				OneDriveClient newClient = JsonConvert.DeserializeObject<OneDriveClient>(userResult);
				if (Settings.Instance.Accounts.ContainsKey(newClient.UserData.Id))
					Settings.Instance.Accounts[newClient.UserData.Id] = newClient;
				else
					Settings.Instance.Accounts.Add(newClient.UserData.Id,newClient);
				return newClient;
			}
			catch (System.Exception ex)
			{
				return null;
			}
		}
#endregion

		[JsonProperty("token_type")]
		public string TokenType { get; set; }
		[JsonProperty("scope")]
		[JsonConverter(typeof(StringToListJsonConverter))]
		public string[] Scopes { get; set; }
		[JsonProperty("expires_in")]
		[JsonConverter(typeof(TimeSpanJsonConverter))]
		public TimeSpan Expires { get; set; }
		[JsonProperty("ext_expires_in")]
		public int ExpiresIn { get; set; }
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }

		public OwnerInfo _userData;
		public OwnerInfo UserData {
			get
			{
				return _userData ?? (_userData = new OwnerInfo(AccessToken));
			}
			set
			{
				_userData = value;
			}
		}

		public async Task<List<OneDriveItem>> GetRootFolders()
		{
			var result = JObject.Parse(await GetHttpContent("https://graph.microsoft.com/v1.0/me/drive/root/children?select=id,name,size,folder,createdBy"));
			var data = result["value"]?.Where(w => w["folder"] != null);
			List<OneDriveItem> folders = data.Select(s => s.ToObject<OneDriveItem>()).ToList();
			return folders;
		}

		public async Task<bool> DeleteItem(OneDriveItem item)
		{
			if (item == null)
				return false;
			string deleteUrl = String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}", item.Id);
			using (HttpClient client = new HttpClient())
			{
				System.Net.Http.HttpResponseMessage response;
				try
				{
					HttpRequestMessage request = CreateRequestWithAuthorizationData(deleteUrl, HttpMethod.Delete);
					response = await client.SendAsync(request);
					if (response.StatusCode == HttpStatusCode.Unauthorized)
					{
						RenewAccessToken();
						request = CreateRequestWithAuthorizationData(deleteUrl, HttpMethod.Delete);
						response = await client.SendAsync(request);						
					}
					if (response.StatusCode != HttpStatusCode.NoContent)
						logger.Warn("Delete file failed StatusCode = " + response.StatusCode + " for item " + item.ToString());
					else
						logger.Debug("File delete success for item = {0}", item);
					return response.StatusCode == HttpStatusCode.NoContent;
				}
				catch (System.Exception ex)
				{
					logger.Warn(ex, "Delete item failed reason {0}", ex);
					return false;
				}
			}
		}

		/// <summary>
		/// Return list of items in folder which sorted by lastModifiedDateTime property
		/// </summary>
		/// <param name="folderId"></param>
		/// <returns></returns>
		public async Task<Queue<OneDriveItem>> GetListOfItemsInFolder(string folderId)
		{
			string getItemsUrl = String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/children?orderby=lastModifiedDateTime", folderId);
			string result = await GetHttpContent(getItemsUrl);
			var jres = JObject.Parse(result);
			Queue<OneDriveItem> res = null;
			try
			{
				res = jres["value"].ToObject<Queue<OneDriveItem>>();
			}
			catch (System.Exception ex)
			{
				logger.Warn(ex, "Extract of items from 'value' property exception. Message {0}", ex.Message);
				return res;

			}
			return res;				
		}

		/// <summary>
		/// Perform an HTTP GET request to a URL using an HTTP Authorization header
		/// </summary>
		/// <param name="url">The URL</param>
		/// <param name="token">The token</param>
		/// <returns>String containing the results of the GET operation</returns>
		public async Task<string> GetHttpContent(string url)
		{
			using (var httpClient = new System.Net.Http.HttpClient())
			{
				System.Net.Http.HttpResponseMessage response;
				try
				{
					var request = CreateRequestWithAuthorizationData(url, HttpMethod.Get);
					response = await httpClient.SendAsync(request);
					if (response.StatusCode == HttpStatusCode.Unauthorized)
					{
						RenewAccessToken();
						request = CreateRequestWithAuthorizationData(url, HttpMethod.Get);
						response = await httpClient.SendAsync(request);
					}
					var content = await response.Content.ReadAsStringAsync();
					return content;
				}
				catch (Exception ex)
				{
					return ex.ToString();
				}
			}			
		}		

		private HttpRequestMessage CreateRequestWithAuthorizationData(string url, HttpMethod methodType)
		{
			var request = new System.Net.Http.HttpRequestMessage(methodType, url);
			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
			return request;
		}

		private void RenewAccessToken()
		{
			string userResult;
			using (var wc = new WebClient())
			{
				string body = OneDriveClient.MakeRefreshTokenRequestBody(RefreshToken);
				wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
				userResult = wc.UploadString("https://login.microsoftonline.com/common/oauth2/v2.0/token", body);
			}
			try
			{
				var client = JsonConvert.DeserializeObject<OneDriveClient>(userResult);
				this.AccessToken = client.AccessToken;
				this.Expires = client.Expires;
				this.ExpiresIn = client.ExpiresIn;
				this.RefreshToken = client.RefreshToken;
				this.Scopes = client.Scopes;
				this.TokenType = client.TokenType;
			}
			catch (System.Exception ex)
			{				
			}
		}

		public void LogOut()
		{
			//GET https://login.microsoftonline.com/common/oauth2/v2.0/logout?post_logout_redirect_uri={redirect-uri}
		}		

		public class OwnerInfo
		{
			[JsonProperty("displayName")]
			public string DisplayName { get; set; }
			[JsonProperty("id")]
			public string Id { get; set; }
			[JsonProperty("userPrincipalName")]
			public string PrincipalName { get; set; }
			public OwnerInfo()
			{

			}
			public OwnerInfo(string accessToken)
			{
				using (var httpClient = new System.Net.Http.HttpClient())
				{
					System.Net.Http.HttpResponseMessage response;
					try
					{
						var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
						//Add the token in Authorization header
						request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
						response = httpClient.SendAsync(request).Result;
						var content = response.Content.ReadAsStringAsync().Result;
						OwnerInfo oi = JsonConvert.DeserializeObject<OwnerInfo>(content);
						this.DisplayName = oi.DisplayName;
						this.Id = oi.Id;
						this.PrincipalName = oi.PrincipalName;
					}
					catch (Exception ex)
					{

					}
				}
			}
		}
	}	
}
