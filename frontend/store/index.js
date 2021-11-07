
export const state = () => ({
    dashboardPageStatus: 'loading',
    dashboardSimpleTodos: {
        categories: {},
        simpletodos: {
            updated: 0,
            data: [],
        },
    },
    confirmationModal: {
        called: false,
        subject: null,
        data: {
            title: null,
            paragraph: null,
        },
        answer: null,
    },
})

export const mutations = {
    openModal(state, subject, title = null, paragraph = null) {
        console.log(subject, state.confirmationModal.subject);

        console.log('else');
        state.confirmationModal.called = true;
        state.confirmationModal.subject = subject;
        state.confirmationModal.data.title = title;
        state.confirmationModal.data.paragraph = paragraph;

    },
    closeModal(state, answer) {
        console.log('close is been called');
        state.confirmationModal.called = false;
        state.confirmationModal.answer = answer;
    },
    cleanAnswer(state) {
        state.confirmationModal.answer = null;
    },
    setDashboardPageStatus(state, setting) {
        state.dashboardPageStatus = setting;
    },
    setCategories(state, categories) {
        categories.forEach(category => {
            state.dashboardSimpleTodos.categories[category.categoryId] = category.title;
        });
    },
    setSimpletodos(state, simpletodos) {
        state.dashboardSimpleTodos.simpletodos.data = simpletodos;
        state.dashboardSimpleTodos.simpletodos.updated++;
    },
    updateSimpletodo(state, simpletodo) {
        state.dashboardSimpleTodos.simpletodos.data.forEach((task, index) => {
            if (task.id == simpletodo.id) {
                state.dashboardSimpleTodos.simpletodos.data[index] = simpletodo;
            }
        });
        state.dashboardSimpleTodos.simpletodos.updated++;
    },
    addSimpletodo(state, simpletodo){
        //Para isso funcionar, o backend precisa retornar o simpletodo adicionado;
        state.dashboardSimpleTodos.simpletodos.updated++;
    },
    deleteSimpletodo(state, simpletodoId){
        state.dashboardSimpleTodos.simpletodos.data.forEach((task, index) => {
            if (task.id == simpletodoId) {
                state.dashboardSimpleTodos.simpletodos.data.splice(index, 1);
            }
        });
        state.dashboardSimpleTodos.simpletodos.updated++;
    },

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