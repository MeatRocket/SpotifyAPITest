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
		public string? DeviceId { get; set; }

		//protected override void OnAfterRender(bool firstRender)
		//{
		//	base.OnAfterRender(firstRender);
		//}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (string.IsNullOrEmpty(Token))
				await RequestUserToken();
		}

		public object? SpotifyPlayer { get; set; }

		public async Task OnButtonClick()
		{

			if (Token == null)
				return;

			var m = await Module;
			SpotifyPlayer = await m.InvokeAsync<object>("init", Token);

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
				Scope = new[] {
				Scopes.PlaylistReadPrivate,
				Scopes.PlaylistReadCollaborative,
				Scopes.Streaming,
				Scopes.UserLibraryRead,
				Scopes.AppRemoteControl,
				Scopes.UserTopRead,
				Scopes.UserReadPlaybackState,
				Scopes.UgcImageUpload,
				Scopes.UserFollowRead,
				Scopes.UserReadCurrentlyPlaying,
				Scopes.UserReadEmail,
				Scopes.UserReadPlaybackPosition,
				Scopes.UserReadPrivate,
				Scopes.UserReadRecentlyPlayed}
			};
			Uri uri = loginRequest.ToUri();

			// This call requires Spotify.Web.Auth
			//BrowserUtil.Open(uri);

			Navigation.NavigateTo(uri.AbsoluteUri);
		}

		protected async Task PlayTrack(string id)
		{
			if (DeviceId == null)
			{
				var m = await Module;
				DeviceId = await m.InvokeAsync<string>("getDeviceId");
			}

			PlayerAddToQueueRequest queueRequest = new(id)
			{
				DeviceId = DeviceId
			};

			DeviceResponse deviceResponse = await Spotify.Player.GetAvailableDevices();
			Device device = deviceResponse.Devices.First(key => key.Id == DeviceId);

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

		private Task<IJSObjectReference> _module;
		private Task<IJSObjectReference> Module => _module ??= GetIJSObjectReference(jsRuntime, "js/spotify.js");

		internal static Task<IJSObjectReference> GetIJSObjectReference(IJSRuntime jsRuntime, string path)
		{
			string importPath = $"./{path}?v={Guid.NewGuid()}";

			return jsRuntime.InvokeAsync<IJSObjectReference>("import", importPath).AsTask();
		}
	}
}