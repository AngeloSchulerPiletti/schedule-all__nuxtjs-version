
export const state = () => ({
    
})

export const mutations = {
    
}



export const actions = {
    async nuxtServerInit(vuexContext, {app}) {
        var userData_obj = app.$cookies.get('userData')
        if (userData_obj) {
            var refreshToken = userData_obj.tokenData.refreshToken,
                accessToken = userData_obj.tokenData.accessToken
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
    }
}