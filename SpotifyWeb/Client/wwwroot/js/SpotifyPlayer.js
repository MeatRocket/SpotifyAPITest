export function testMethod(token) {
    alert(token);

    //window.Spotify.onSpotifyWebPlaybackSDKReady = () => {
    //    const token = token;
    //    const player = new Spotify.Player({
    //        name: 'Web Playback SDK Quick Start Player',
    //        getOAuthToken: cb => { cb(token); },
    //        volume: 0.5
    //    });
    //    alert("running web playback sdk before .js");
    //    // Ready
    //    player.addListener('ready', ({ device_id }) => {
    //        console.log('Ready with Device ID', device_id);
    //    });

    //    // Not Ready
    //    player.addListener('not_ready', ({ device_id }) => {
    //        console.log('Device ID has gone offline', device_id);
    //    });

    //    player.addListener('initialization_error', ({ message }) => {
    //        console.error(message);
    //    });

    //    player.addListener('authentication_error', ({ message }) => {
    //        console.error(message);
    //    });

    //    player.addListener('account_error', ({ message }) => {
    //        console.error(message);
    //    });

    //    document.getElementById('togglePlay').onclick = function() {
    //        player.togglePlay();
    //    };

    //    player.connect().then(success => {
    //        if (success) {
    //            alert('The Web Playback SDK successfully connected to Spotify!');
    //        }
    //    });
    //};

    alert("done");

}