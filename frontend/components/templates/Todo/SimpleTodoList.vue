<template>
  <div class="wrapper grid">
    <div
      :class="`card flex_r ${simpletodo.finished}`"
      v-for="(simpletodo, index) in simpletodos"
      :key="index"
    >
      <div class="category_mark" v-if="simpletodo.categoryId > 0">
        <span>{{
          $store.state.dashboardSimpleTodos.categories[simpletodo.categoryId]
        }}</span>
      </div>
      <div class="left flex_c">
        <input
          class="title"
          type="text"
          autocomplete="off"
          v-model="simpletodo.title"
        /><!-- Ou um input -->
        <textarea
          data-enable-grammarly="false"
          class="description"
          type="text"
          autocomplete="off"
          v-model="simpletodo.description"
        ></textarea
        ><!-- Ou um input -->
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
  props: {
    simpletodos: Array,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  grid-template-columns: 1fr 1fr;
  gap: 40px 50px;
  padding: 0 80px;

  .card {
    padding: 12px;
    background: linear-gradient(160deg, #c9c9c9, #eaeaea);
    box-shadow: 5px 5px 10px #707070, -3px -3px 10px #fff;
    border-radius: 10px;
    position: relative;

    .category_mark {
      position: absolute;
      top: 0;
      padding: 2px 7px;
      border-top-left-radius: 10px;
      border-top-right-radius: 10px;
      right: 20px;
      transform: translateY(-100%);
      background-color: #e9e9e9;
      box-shadow: inset 0 -2px 3px 0 #909090, 0 0 4px #a0a0a0;
      cursor: pointer;

      span {
        font-size: 12px;
      }
      transition: box-shadow 200ms;

      &:hover {
        box-shadow: inset 0 -2px 3px 0 #909090, 0 0 4px transparent;
      }
    }
    .left {
      flex-grow: 1;
      gap: 8px;

      .title {
        text-transform: uppercase;
        font-weight: 500;
      }
      .description {
        box-shadow: inset 2px 2px 6px #707070, inset -2px -2px 6px #fff;
        border-radius: 8px;
        padding: 8px;
        resize: none;
        font-weight: 300;
      }
    }
    .right {
      width: 30px;
    }
  }
}
</style>
