const https = require('https')

export default function ({ $axios, redirect, $cookies }) {
    $axios.defaults.httpsAgent = new https.Agent({ rejectUnauthorized: false });

    $axios.onRequest(config => {
        var userData = $cookies.get('userData');
        if (userData) {
            console.log('o token enviado é: ' + userData.accessToken);
            $axios.setToken(userData.accessToken, 'Bearer');
        }
    });

    $axios.onResponse(response => {
        // console.log(response);
    });

    $axios.onError(error => {
        console.log(error);
    });
}