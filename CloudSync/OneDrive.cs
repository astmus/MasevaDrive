using CloudSync.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace CloudSync
{
    class OneDriveStat
    {
        private static string ClientId = "32171e35-694f-4481-a8bc-0498cb7da487";
        public static PublicClientApplication PublicClientApp = new PublicClientApplication(ClientId);
        static string[] _scopes = new string[] { "user.read","files.readwrite", "files.readwrite.all" };
        public static AuthenticationResult authResult = null;
        public static async Task<AuthenticationResult> Authenticate()
        {  

            try
            {
                string s = "https://login.microsoftonline.com/common/";
                authResult = await OneDriveStat.PublicClientApp.AcquireTokenSilentAsync(_scopes, OneDriveStat.PublicClientApp.Users.FirstOrDefault(), s, true);
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilentAsync. This indicates you need to call AcquireTokenAsync to acquire a token
                System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                try
                {
                    authResult = await OneDriveStat.PublicClientApp.AcquireTokenAsync(_scopes);
                }
                catch (MsalException msalex)
                {
                    //ResultText.Text = $"Error Acquiring Token:{System.Environment.NewLine}{msalex}";
                }
            }
            catch (Exception ex)
            {
                //ResultText.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
            }

            return authResult;
        }

        /// <summary>
		/// Perform an HTTP GET request to a URL using an HTTP Authorization header
		/// </summary>
		/// <param name="url">The URL</param>
		/// <param name="token">The token</param>
		/// <returns>String containing the results of the GET operation</returns>
		public static async Task<string> GetHttpContentWithToken(string url, string token)
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
            try
            {
                var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                //Add the token in Authorization header
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
		/// Perform an HTTP GET request to a URL using an HTTP Authorization header
		/// </summary>
		/// <param name="url">The URL</param>
		/// <param name="token">The token</param>
		/// <returns>String containing the results of the GET operation</returns>
		public static async Task<string> GetHttpContentWithToken(string url)
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
            try
            {
                var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                //Add the token in Authorization header
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        /// <summary>
		/// Perform an HTTP GET request to a URL using an HTTP Authorization header
		/// </summary>
		/// <param name="url">The URL</param>
		/// <param name="token">The token</param>
		/// <returns>String containing the results of the GET operation</returns>
		public static async Task<Stream> GetHttpStreamWithToken(string url)
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
            try
            {
                var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                //Add the token in Authorization header
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStreamAsync();
                return content;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //
        public async static Task<List<OneDriveItem>> GetRootFolders(string token)
        {
            var result = JObject.Parse(await GetHttpContentWithToken("https://graph.microsoft.com/v1.0/me/drive/root/children?select=id,name,size,folder", token));            
            var data = result["value"]?.Where(w => w["folder"] != null); ;
            List<OneDriveItem> folders = data.Select(s => s.ToObject<OneDriveItem>()).ToList();
            return folders;
        }

        public static void SignOut()
        {
            if (OneDriveStat.PublicClientApp.Users.Any())
            {
                try
                {
                    OneDriveStat.PublicClientApp.Remove(OneDriveStat.PublicClientApp.Users.FirstOrDefault());
                    /*this.ResultText.Text = "User has signed-out";
                    this.CallGraphButton.Visibility = Visibility.Visible;
                    this.SignOutButton.Visibility = Visibility.Collapsed;*/
                }
                catch (MsalException ex)
                {
                    //ResultText.Text = $"Error signing-out user: {ex.Message}";
                }
            }
        }
    }
}
