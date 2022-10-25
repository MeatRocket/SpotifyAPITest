using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.JSInterop;
using SpotifyAPI.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Buffers.Text;
using System.Diagnostics;
using SpotifyAPI.Web.Auth;

namespace SpotifyWeb.Client.Pages
{
	public partial class Index
	{
		int s;
		public string ClientId { get; set; } = "ba6b181aae70405893be29fa23cd50ec";

		public string ClientSecret { get; set; } = "50a12b910bdf406abd9c9e3bad2e4d15";


		public List<FullArtist>? Artists { get; set; }
		public List<SavedTrack>? Tracks { get; set; }
		SpotifyClient Spotify { get; set; }

		[Parameter]
		[SupplyParameterFromQuery(Name = "token")]
		public string? Token { get; set; }

		//protected override void OnAfterRender(bool firstRender)
		//{
		//	base.OnAfterRender(firstRender);
		//}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (String.IsNullOrEmpty(Token))
				await RequestUserToken();
		}

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

			if (Token == null)
			{
				return;
			}

			try
			{
				Spotify = new(Token);
				Paging<SavedTrack> libraryTracks = await Spotify.Library.GetTracks();
				Tracks = libraryTracks.Items;
			}
			catch (Exception e)
			{
				throw e;
			}
			//SearchResponse response = await Spotify.Search.Item(new SearchRequest(SearchRequest.Types.Artist, "Justin"));

			//Paging<FullArtist, SearchResponse> p = response.Artists;

			//Artists = p.Items;
		}

		//private async Task RequestTokenOnce()
		//{
		//	SpotifyClientConfig config = SpotifyClientConfig.CreateDefault();

		//	ClientCredentialsRequest request = new ClientCredentialsRequest(ClientId, ClientSecret);
		//	ClientCredentialsTokenResponse response = await new OAuthClient(config).RequestToken(request);

		//	Spotify = new SpotifyClient(response.AccessToken);
		//}

		private async Task RequestUserToken()
		{
			var loginRequest = new LoginRequest(
				  new Uri("https://localhost:7096/callback"),
				  ClientId,
				  LoginRequest.ResponseType.Token
				)
			{
				Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.Streaming, Scopes.UserLibraryRead }
			};
			Uri uri = loginRequest.ToUri();

			// This call requires Spotify.Web.Auth
			//BrowserUtil.Open(uri);

			Navigation.NavigateTo(uri.AbsoluteUri);
		}

		protected async Task PlayTrack(string id)
		{
			PlayerAddToQueueRequest queueRequest = new(id);

			try
			{
				await Spotify.Player.AddToQueue(queueRequest);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		protected async Task PlayNextSong()
		{
			await Spotify.Player.SkipNext();
		}
	}
}