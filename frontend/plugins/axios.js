const https = require('https')

export default function ({ $axios, redirect, $cookies }) {
    $axios.defaults.httpsAgent = new https.Agent({ rejectUnauthorized: false });
    $axios.setHeader('Content-Type', 'application/json');

    $axios.onRequest(config => {
        var userData = $cookies.get('userData');
        if (userData) {
            console.log('o token enviado é: ' + userData.tokenData.accessToken);
            $axios.setToken(userData.tokenData.accessToken, 'Bearer');
        }
    });

    $axios.onResponse(response => {
        // console.log(response);
    });

    $axios.onError(error => {
        console.log(error);
    });
}