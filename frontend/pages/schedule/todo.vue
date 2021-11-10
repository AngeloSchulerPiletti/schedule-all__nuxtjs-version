<template>
  <div id="todo_wrapper" class="flex_c" v-if="dataLoaded">
    <div class="categories">
      <categories-selection :categories="categoriesDataStoreArray"/>
    </div>
    <div class="todos flex_c scroll-1">
      <simple-todo-list :simpletodos="simpletodosDataStoreArray" :categories="categoriesDataStoreArray"/>
    </div>
    <simple-todo-creation :categories="categoriesDataStoreArray"/>
  </div>
</template>

<script>
import CategoriesSelection from '@/components/templates/Todo/CategoriesSelection.vue'
import SimpleTodoList from '@/components/templates/Todo/SimpleTodoList.vue'
import SimpleTodoCreation from '@/components/templates/Todo/SimpleTodoCreation.vue'

export default {
  layout: 'dashboard',
  head:{
    title: "Tarefas",
  },
  data() {
    return {
      dataLoaded: false,
    }
  },
  computed: {
    simpletodosDataStoreArray() {
      let pref = this.$store.state.simpletodo.dashboardSimpleTodos.simpletodos
      return [pref.data, pref.updated]
    },
    categoriesDataStoreArray() {
      let pref = this.$store.state.simpletodo.dashboardSimpleTodos.categories
      return [pref.data, pref.updated]
    },
  },
  watch: {
    simpletodosDataStoreArray: {
      immediate: true,
      handler(oldValue, newValue) {},
    },
    categoriesDataStoreArray: {
      immediate: true,
      handler(oldValue, newValue) {},
    },
  },
  mounted() {
    this.$axios
      .get('v1/SimpleTodo/get-all-user-simpletodos')
      .then((res) => {
        this.$store.commit('simpletodo/setSimpletodos', res.data);
        this.$axios
          .get('v1/Category/get-user-categories')
          .then((res) => {
            this.$store.commit('simpletodo/setCategories', res.data)
            this.$store.commit('setDashboardPageStatus', 'loaded')
            this.dataLoaded = true
          })
          .catch((err) => {
            this.$store.commit('setDashboardPageStatus', 'error')
            console.log(err)
          })
      })
      .catch((err) => {
        this.$store.commit('setDashboardPageStatus', 'error')
      })
  },
  components: {
    CategoriesSelection,
    SimpleTodoList,
    SimpleTodoCreation,
  },
}
</script>

<style lang="scss" scoped>
#todo_wrapper {
  height: 100%;
  position: relative;

  .todos {
    position: relative;
    overflow-y: auto;
    overflow-x: hidden;
    height: 100%;
  }
}
</style>
