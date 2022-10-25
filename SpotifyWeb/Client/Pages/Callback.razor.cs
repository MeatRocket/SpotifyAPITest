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
using SpotifyAPI.Web.Http;

namespace SpotifyWeb.Client.Pages
{
	public partial class Callback
	{
		[Parameter]
		[SupplyParameterFromQuery(Name = "access_token")]
		public string Token { get; set; }

		protected override void OnAfterRender(bool firstRender)
		{
			string uri = Navigation.Uri;

			if (uri.Contains("callback#"))
			{
				uri = uri.Replace("callback#", "callback?");
				Navigation.NavigateTo(uri);
				return;
			}

			Navigation.NavigateTo($"/?token={Token}");
		}
	}
}