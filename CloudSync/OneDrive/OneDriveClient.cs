using Newtonsoft.Json;
using System;
using CloudSync.Converters;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CloudSync.OneDrive
{
	public class OneDriveClient
	{
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
				return JsonConvert.DeserializeObject<OneDriveClient>(userResult);
			}
			catch (System.Exception ex)
			{
				return null;
			}
		}

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
	}

	
}
