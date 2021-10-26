
export const state = () => ({
    userData: {
        authenticated: false,
    },
})

export const mutations = {
    setUserData(state, data) {
        if (data) {
            window.sessionStorage.setItem('userData', JSON.stringify(data));
            state.userData = data;
            console.log('here1');
        } else {
            console.log('here2');
            window.sessionStorage.removeItem('userData');
            state.userData = { authenticated: false };
        }
    }
}



export const actions = {
    async nuxtServerInit(vuexContext, {app}) {
        var userData_str = app.$cookies.get('userData')
        if (userData_str) {
            var userData_obj = userData_str;
            var refreshToken = userData_obj.refreshToken,
                accessToken = userData_obj.accessToken
        }
        if (refreshToken && accessToken) {
            await app.$axios
                .post('/v1/Auth/refresh', {
                    accessToken: accessToken,
                    refreshToken: refreshToken,
                })
                .then((response) => {
                    app.$cookies.set('userData', response.data);
                    app.$axios.setToken(response.data.accessToken, 'Bearer');
                    app.$axios.setToken(accessToken, 'Bearer')
                }).catch(err => {
                    console.log(`token inválido: ${err}`);
                    app.$cookies.remove('userData');
                });
        }
        else{
            console.log('sem token nos cookies');
        }

        //Checa se o refreshToken é válido
        //Se sim => Pega os dados retornados e salva em userData
        //Se não => Checa se tem userData em cookie
        //Se sim => Deleta esses cookies
    }
}