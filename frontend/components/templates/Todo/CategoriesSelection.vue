<template>
  <div class="wrapper">
      <div class="categories">
          <span>Todos</span>
          <span v-for="(category, index) in categories" :key="index">
              {{category.title}}
          </span>
      </div>
      <div class="category_creation">
          <input type="text" name="title" id="title">
          <button></button>
      </div>
  </div>
</template>

<script>
export default {
    props:{
        categories: Array,
    }
}
</script>

<style lang="scss" scoped>

</style>