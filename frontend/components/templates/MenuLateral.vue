<template>
  <div class="wrapper border-soft">
    <ul class="page-list">
      <li>
        <NuxtLink to="/schedule/todo" class="flex_r"
          ><todo class="menu-icon" /> Tarefas</NuxtLink
        >
      </li>
    </ul>
  </div>
</template>

<script>
import Todo from '@/components/icons/Todo'

export default {
  components: {
    Todo,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  width: 20vw;
  max-height: 100vh;
  padding: 20px 0;
  border-top-right-radius: 15px;
  background: linear-gradient(-105deg, rgba(0, 0, 0, 0.1), rgba(0, 0, 0, 0.1));

  &::before {
    border-top-right-radius: 15px;
    box-shadow: 6px 6px 14px #a9a8b7, -6px -6px 14px #ffffff,
      inset -4px -4px 14px #ffffff, inset 4px 4px 14px #a9a8b7;
  }


  .page-list {
    position: relative;
    z-index: 5;
    li {
      a {
        align-items: center;

        .menu-icon {
          width: 24px;
          height: 24px;
        }
      }
    }
  }
}
</style>
