export const state = () => ({
    dashboardSimpleTodos: {
        categories: {
            updated: 0,
            data: {},
        },
        simpletodos: {
            updated: 0,
            data: [],
        },
    },
})

export const mutations = {
    addCategory(state, category) {
        state.dashboardSimpleTodos.categories.data[category.categoryId] = category.title;
        state.dashboardSimpleTodos.categories.updated++;
    },
    setCategories(state, categories) {
        categories.forEach(category => {
            state.dashboardSimpleTodos.categories.data[category.categoryId] = category.title;
        });
        state.dashboardSimpleTodos.categories.updated++;
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
    addSimpletodo(state, simpletodo) {
        state.dashboardSimpleTodos.simpletodos.data.push(simpletodo);
        state.dashboardSimpleTodos.simpletodos.updated++;
    },
    deleteSimpletodo(state, simpletodoId) {
        console.log(state.dashboardSimpleTodos.simpletodos.data);
        state.dashboardSimpleTodos.simpletodos.data.forEach((task, index) => {
            if (task.id == simpletodoId) {
                state.dashboardSimpleTodos.simpletodos.data.splice(index, 1);
            }
        });
        state.dashboardSimpleTodos.simpletodos.updated++;
    },
    deleteCategory(state, categoryId) {
        if (state.dashboardSimpleTodos.categories.data[categoryId]) delete state.dashboardSimpleTodos.categories.data[categoryId];
        state.dashboardSimpleTodos.categories.updated++;
    }
}



export const actions = {
    //
}