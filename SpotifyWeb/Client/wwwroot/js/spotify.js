export async function init(token) {

	const player = new Spotify.Player({
		name: 'Angry Spotify',
		getOAuthToken: cb => { cb(token); }
	});

	player.addListener('ready', ({ device_id }) => {
		console.log(device_id);
		document.querySelector('#SpotifyDeviceId').value = device_id;
	});

	player.addListener('initialization_error', ({ message }) => {
		console.log("Angry Initialization Error.");
		console.error(message);
	});

	player.addListener('authentication_error', ({ message }) => {
		console.log("Angry Authentication Error.");
		console.error(message);
	});

	player.addListener('account_error', ({ message }) => {
		console.log("Angry Account Error.");
		console.error(message);
	});

	//player.addListener('player_state_changed', (state => {
	//	debugger;
	//}));

	await player.connect();

	return player;
}

export function getDeviceId() {
	return document.querySelector('#SpotifyDeviceId').value;
}

export function play() {

}