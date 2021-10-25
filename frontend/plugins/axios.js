
export default function ({ $axios, redirect, store }) {
    $axios.onRequest(config => {
        var userInfo_str = window.sessionStorage.getItem('userInfo');
        if (userInfo_str) {
            $axios.setToken(JSON.parse(userInfo_str).accessToken, 'Bearer');
        }
        console.log(config);
    });

    $axios.onResponse(response => {
        if (response.data.authenticated) {
            window.sessionStorage.setItem('userInfo', JSON.stringify(response.data));
        }
    });

    $axios.onError(error => {
      console.log(error);
    });
  }