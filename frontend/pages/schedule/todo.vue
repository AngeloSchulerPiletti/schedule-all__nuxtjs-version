<template>
  <div id="todo_wrapper" class="flex_c">
    <div class="categories">
      <categories-selection />
    </div>
    <div class="todos">
      <simple-todo-list :simpletodos="simpletodos" />
    </div>
      <simple-todo-creation />
  </div>
</template>

<script>
import CategoriesSelection from '@/components/templates/Todo/CategoriesSelection.vue'
import SimpleTodoList from '@/components/templates/Todo/SimpleTodoList.vue'
import SimpleTodoCreation from '@/components/templates/Todo/SimpleTodoCreation.vue'

export default {
  layout: 'dashboard',
  data() {
    return {
      simpletodos: [],
    }
  },
  mounted() {
    this.$axios
      .get('v1/SimpleTodo/get-all-user-simpletodos')
      .then((res) => {
        this.simpletodos = res.data
        this.$axios
          .get('v1/Category/get-user-categories')
          .then((res) => {
            this.$store.commit('setCategories', res.data)
            this.$store.commit('setDashboardPageStatus', 'loaded')
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
  gap: 50px;
  position: relative;

}
</style>
