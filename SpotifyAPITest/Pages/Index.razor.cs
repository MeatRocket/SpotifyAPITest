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

namespace SpotifyAPITest.Pages
{
    public partial class Index
    {
        int s;
        public async Task OnButtonClick()
        {
            SpotifyClientConfig config = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator("2678f1b02ff44006a21eaeabbf58bd1e", "149ce90bb0504abea6744a7a25358371"));

            SpotifyClient spotify = new(config);

            var tracks = spotify.UserProfile.Current();

            s = tracks.Id;

            Console.WriteLine(spotify.Albums);
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