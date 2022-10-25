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
	public partial class Callback
	{
		protected override void OnAfterRender(bool firstRender)
		{
			
			base.OnAfterRender(firstRender);
		}
	}
}