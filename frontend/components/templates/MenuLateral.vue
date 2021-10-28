<template>
  <div class="wrapper">
      <ul class="page-list">
          <li>
              <NuxtLink to="/schedule/todo" class="flex_r"><todo class="menu-icon"/> Tarefas</NuxtLink>
          </li>
      </ul>
  </div>
</template>

<script>
import Todo from "@/components/icons/Todo";

export default {
    components: {
        Todo,
    },
}
</script>

<style lang="scss" scoped>
.wrapper{
    background-color: red;
    width: 20vw;
    max-height: 100vh;
    height: 100%;
    .page-list{
        li{
            a{
                align-items: center;

                .menu-icon{
                    width: 24px;
                    height: 24px;
                }
            }
        }
    }
}
</style>