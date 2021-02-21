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
using System.Net.Http.Headers;
using System.Web;
using CloudSync.Interfaces;
using System.IO;
using System.Threading;

namespace CloudSync
{
	public class OneDriveClient : ICloudStreamProvider, IDeleteSyncItemProvider
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		#region Static functionality

		public static readonly string ClientID = "32171e35-694f-4481-a8bc-0498cb7da487";
		public static readonly string RedirectUri = "msal32171e35-694f-4481-a8bc-0498cb7da487://auth";
		public static readonly string AppSecret = "/ckq6/heZ-@GeOVxGb5jYBZZ59bz5Ag:";
		public static string GetAuthorizationRequestUrl()
		{
			return String.Format("https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id={0}&scope=offline_access user.read files.readwrite&response_type=code&redirect_uri={1}", ClientID, RedirectUri);
		}

		private static string MakeAcquireTokenByAuthorizationCodeContent(string code)
		{
			return String.Format("client_id={0}&redirect_uri={1}&code={2}&grant_type=authorization_code", ClientID, RedirectUri, code);
		}

		private static string MakeRefreshTokenRequestBody(string refreshToken)
		{
			return String.Format("client_id={0}&redirect_uri={1}&refresh_token={2}&grant_type=refresh_token", ClientID, RedirectUri, refreshToken);
			//return String.Format("client_id={0}&redirect_uri={1}&client_secret={2}&refresh_token={3}&grant_type=refresh_token", ClientID, RedirectUri, AppSecret, refreshToken);
		}

		private static readonly string PersonalDataUrl = "https://graph.microsoft.com/v1.0/me";

		public static OneDriveClient AcquireClientByCode(string code)
		{
			string dataResult;
			using (var wc = new WebClient())
			{
				string body = OneDriveClient.MakeAcquireTokenByAuthorizationCodeContent(code);
				wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
				dataResult = wc.UploadString("https://login.microsoftonline.com/common/oauth2/v2.0/token", body);
			}
			try
			{
				var result = new OneDriveClient(JsonConvert.DeserializeObject<Credentials>(dataResult));
				return result;
			}
			catch (System.Exception ex)
			{
				logger.Error(ex, "Parse credentioal data error");
				return null;
			}
		}
		#endregion
		[JsonIgnore]
		public OwnerInfo _userData;
		public OwnerInfo UserData
		{
			get { return _userData; }
			set { _userData = value; }
		}

		private Credentials credentialData;
		public Credentials CredentialData
		{
			get { return credentialData; }

			set
			{
				credentialData = value;
				if (credentialData != null)
					inetClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", credentialData.AccessToken);
			}
		}

		private HttpClient inetClient;
		private HttpClientHandler clientHandler;

		static OneDriveClient()
		{
		}

		public OneDriveClient()
		{
			clientHandler = new HttpClientHandler();
			inetClient = new HttpClient(clientHandler);
		}

		~OneDriveClient()
		{
			inetClient.Dispose();
			clientHandler.Dispose();
			inetClient = null;
			clientHandler = null;
		}

		public OneDriveClient(Credentials accessData)
		{
			credentialData = accessData;
			if (accessData?.AccessToken != null)
			{
				clientHandler = new HttpClientHandler();
				inetClient = new HttpClient(clientHandler)
				{
					DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", accessData.AccessToken) }
				};
				var result = inetClient.GetAsync(PersonalDataUrl).Result;
				UserData = JsonConvert.DeserializeObject<OwnerInfo>(result.Content.ReadAsStringAsync().Result);
			}
			else
				throw new Exception("Wrong credentials");
		}

		public async Task<List<OneDriveFolder>> RequestRootFolders()
		{
			var result = JObject.Parse(await GetHttpContent("https://graph.microsoft.com/v1.0/me/drive/root/children?select=id,name,size,folder,createdBy"));
			var data = result["value"]?.Where(w => w["folder"] != null);
			List<OneDriveFolder> folders = data.Select(s => s.ToObject<OneDriveFolder>()).ToList();
			return folders;
		}		

		public async Task<Exception> DeleteItem(OneDriveItem item)
		{
			if (item == null)
				return new ArgumentNullException("Sync item for delete is null");
			string deleteUrl = String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}", item.Id);
			HttpResponseMessage response;
			try
			{
				response = await inetClient.DeleteAsync(deleteUrl);
				if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					await RenewAccessToken();
					response = await inetClient.DeleteAsync(deleteUrl);
				}
				if (response.StatusCode != HttpStatusCode.NoContent)
					return new WebException(String.Format("Delete file {0} completed with error. Web code = {1}", item.Name, response.StatusCode));
				else
					logger.Debug("File delete success for item = {0}", item);
				return null;
			}
			catch (System.Exception ex)
			{
				logger.Warn(ex, "Delete file {0} failed reason {1}", item.Name, ex);
				return ex;
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
				logger.Error(ex, "Extract of items from 'value' property exception. Message {0}", ex.Message);
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
			System.Net.Http.HttpResponseMessage response;
			try
			{
				if (!CredentialData.IsValid())
					await RenewAccessToken();
				response = await inetClient.GetAsync(url);
				if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					await RenewAccessToken();
					response = await inetClient.GetAsync(url);
				}
				var content = await response.Content.ReadAsStringAsync();
				return content;
			}
			catch (Exception ex)
			{
				if (ex is HttpException)
					throw ex;
			}
			return null;
		}

		private Task currentRenewer = null;
		private Task RenewAccessToken()
		{
			if (CredentialData.IsValid()) return Task.CompletedTask;
			if (currentRenewer != null)
			{
				currentRenewer.Wait();
				return Task.CompletedTask;
			}
			else
				currentRenewer = Task.Run(() =>
			{
				string userResult = null;
				using (var wc = new WebClient())
				{
					try
					{
						if (CredentialData.RefreshToken == null)
							throw new HttpException((int)HttpStatusCode.Unauthorized, " Credential data is empty");
						string body = OneDriveClient.MakeRefreshTokenRequestBody(CredentialData.RefreshToken);
						wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
						userResult = wc.UploadString("https://login.microsoftonline.com/common/oauth2/v2.0/token", body);
					}
					catch (Exception ex)
					{
						logger.Error(ex, "Renew Access token error");
						//NeedRelogin?.Invoke(this);
						//return;
					}
				}
				try
				{
					CredentialData = JsonConvert.DeserializeObject<Credentials>(userResult);
				}
				catch (System.Exception ex)
				{
					logger.Error(ex, "Parse credential data exception");
					if (ex is WebException)
						throw ex;
				}
			}).ContinueWith((t) => currentRenewer = null);
			return currentRenewer;
		}

		public void LogOut()
		{
			//GET https://login.microsoftonline.com/common/oauth2/v2.0/logout?post_logout_redirect_uri={redirect-uri}
		}

		public async Task<Stream> GetStreamToFileAsync(string url)
		{
			if (!CredentialData.IsValid())
				await RenewAccessToken();

			return await inetClient.GetStreamAsync(url);
		}

		private Semaphore accountDownloadConnectionsLimiter = new Semaphore(2, 2);
		public Semaphore GetDownloadSrteamLimiter()
		{
			return accountDownloadConnectionsLimiter;
		}

		[JsonObject]
		public class Credentials
		{
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
			[JsonProperty]
			public DateTime ObtainedTime { get; set; } = DateTime.Now;
			public bool IsValid()
			{
				return (DateTime.Now - ObtainedTime) < (Expires - TimeSpan.FromMinutes(1)); // 1 minute just in case when request make start about expiration time
			}
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
		}
	}
}
