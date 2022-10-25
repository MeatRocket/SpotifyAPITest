using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using SpotifyAPITest;
using SpotifyAPITest.Shared;
using SpotifyAPI.Web;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Buffers.Text;
using System.Diagnostics;
using SpotifyAPI.Web.Auth;

namespace SpotifyAPITest.Pages
{
	public partial class Index
	{
		int s;
		public string Token { get; set; } = "BQB8sIuQmeKrA0Y1jfxBPdzyGAvEA_dDCdw8K-D2-oLWJ3yOIQ_KmqcEZvUJ-K-9iPRcraOd6y3wkwIlWm4klDka5ZzDEXKriH8P9YEwqRzbgOjF1QbOxjQ97XicKybZea8SZt3cJ4f8vybdc0T2cIyy-7ILJZToafY9hdOCWoGBysj07f-maFem21rm5m5GONnI8l2s9Q";
		public string ClientId { get; set; } = "ba6b181aae70405893be29fa23cd50ec";

		public string ClientSecret { get; set; } = "50a12b910bdf406abd9c9e3bad2e4d15";


		public List<FullArtist>? Artists { get; set; }
		public List<SavedTrack>? Tracks { get; set; }
		SpotifyClient? Spotify { get; set; }
		public string? AccessToken { get; set; } = null;

		public async Task OnButtonClick()
		{
			//SpotifyClientConfig config = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator("2678f1b02ff44006a21eaeabbf58bd1e", "149ce90bb0504abea6744a7a25358371"));

			//SpotifyClient spotify = new(config);

			//var tracks = spotify.UserProfile.Current();

			//s = tracks.Id;

			//Console.WriteLine(spotify.Albums);

			//var content = new FormUrlEncodedContent(new[]
			//{
			//	new KeyValuePair<string, string>("grant_type", "client_credentials")
			//});

			//HttpClient client = new();

			//client.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"))}");
			//client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");

			//HttpResponseMessage message = await client.PostAsync("https://accounts.spotify.com/api/token", content);


			//SpotifyClientConfig config = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator(ClientId, ClientSecret));
			//config.

			//SpotifyClient spotify = new(config);
			
			if (AccessToken == null)
			{
				await RequestUserToken();
			}
			else
			{
				try
				{
					SpotifyClient spotify = new(AccessToken);
					Paging<SavedTrack> libraryTracks = await spotify.Library.GetTracks();
					Tracks = libraryTracks.Items;
				}
				catch (Exception e)
				{
					throw e;
				}
			}
			//SearchResponse response = await Spotify.Search.Item(new SearchRequest(SearchRequest.Types.Artist, "Justin"));

			//Paging<FullArtist, SearchResponse> p = response.Artists;

			//Artists = p.Items;
		}

		private async Task RequestTokenOnce()
		{
			SpotifyClientConfig config = SpotifyClientConfig.CreateDefault();

			ClientCredentialsRequest request = new ClientCredentialsRequest(ClientId, ClientSecret);
			ClientCredentialsTokenResponse response = await new OAuthClient(config).RequestToken(request);

			Spotify = new SpotifyClient(response.AccessToken);
		}

		private async Task RequestUserToken()
		{
			var loginRequest = new LoginRequest(
				  new Uri("https://localhost:7187/callback"),
				  ClientId,
				  LoginRequest.ResponseType.Token
				)
			{
				Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative }
			};
			var uri = loginRequest.ToUri();

			// This call requires Spotify.Web.Auth
			BrowserUtil.Open(uri);
		}

		public void CallHttp()
		{
			var client = new RestClient("https://accounts.spotify.com/authorize");
			var request = new RestRequest();
			request.AddHeader("client_id", "ba6b181aae70405893be29fa23cd50ec");
			request.AddHeader("client_secret", "50a12b910bdf406abd9c9e3bad2e4d15");
			request.AddHeader("grant_type", "client_credentials");
			request.AddHeader("Cookie", "__Host-device_id=AQDmifOv-gqBhaOSxxH_ygGBkJK_n3zpC6N-a2mMM8UyzAquh8aL5wWisQPekZFoFdfRZX1c2_-qsqEbDclX-BVokHnoR2Nbgx0; __Host-sp_csrf_sid=5e83d4c435be37e6f2aa11cff9b055cb0370adc7d29cfb84905fb9c0bceab65a; __Secure-TPASESSION=AQC0GAe6HPai9ppiRbHFAHJ0/7jGTTl7hwmEtc1w77CHKs/k4g9YLQWC3U0u/60gaoaR9laos5LVXGGkcwzRw7fM90GLBaRS4+s=; inapptestgroup=; sp_sso_csrf_token=013acda7196ce33cb2958513f592de6fbee7f1189c31363636363830373235343535; sp_tr=false");
			var response = client.Execute(request);
			Console.WriteLine(response.Content);

		}
	}
}