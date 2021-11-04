<template>
  <div class="wrapper grid">
      <div class="card" v-for="(simpletodo, index) in simpletodos" :key="index">
          <div class="category_mark" v-if="simpletodo.categoryId > 0"><span>{{$store.state.dashboardSimpleTodos.categories[simpletodo.categoryId]}}</span></div>
          <div class="left">
              <h6>{{simpletodo.title}}</h6> <!-- Ou um input -->
              <p>{{simpletodo.description}}</p> <!-- Ou um input -->
          </div>
          <div class="right">
              <button class="share"></button>
              <button :class="`finish-${simpletodo.finished}`"></button>
          </div>
      </div>
  </div>
</template>

<script>
export default {
    props:{
        simpletodos: Array,
    },
}
</script>

<style lang="scss" scoped>
.wrapper{
    grid-template-columns: 1fr 1fr;
    gap: 25px 50px;
    padding: 0 80px;

    .card{
        box-shadow: 0 0 10px #000;
        border-radius: 10px;
    }

}

</style>