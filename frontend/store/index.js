
export const state = () => ({
    dashboardPageStatus: 'loading',
    confirmationModal: {
        called: false,
        subject: null,
        data: {
            title: null,
            paragraph: null,
        },
        answer: null,
    },
    inviteFriendModal:{
        called: false,
    },
})

export const mutations = {
    openModal(state, {subject, title = null, paragraph = null}) {
        state.confirmationModal.called = true;
        state.confirmationModal.subject = subject;
        state.confirmationModal.data.title = title;
        state.confirmationModal.data.paragraph = paragraph;

    },
    closeModal(state, answer) {
        state.confirmationModal.called = false;
        state.confirmationModal.answer = answer;
    },
    cleanAnswer(state) {
        state.confirmationModal.answer = null;
    },
    setDashboardPageStatus(state, setting) {
        state.dashboardPageStatus = setting;
    },
    changeInviteFriendModalVisibility(state){
        state.inviteFriendModal.called = !state.inviteFriendModal.called;
    }
}



export const actions = {
    async nuxtServerInit(vuexContext, { app, redirect }) {
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
                    app.$axios.setToken(response.data.tokenData.accessToken, 'Bearer');
                }).catch(err => {
                    console.log(`token inválido: ${err}`);
                    app.$cookies.remove('userData');
                });
        }
        else {
            console.log('sem token nos cookies');
        }
    }
}