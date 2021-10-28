<template>
  <div id="dashboard-layout" class="flex_c">
    <menu-superior />
    <div id="vertical-container" class="flex_r">
      <menu-lateral />
      <div>
        <Nuxt />
      </div>
    </div>
  </div>
</template>

<script>
import MenuLateral from '@/components/templates/MenuLateral'
import MenuSuperior from '@/components/templates/MenuSuperior'

export default {
  components: {
    MenuLateral,
    MenuSuperior,
  },
}
</script>

<style lang="scss" scoped>
#dashboard-layout {
  overflow: hidden;
  height: 100vh;

  #vertical-container {
    flex-grow: 1;
    background-color: green;
  }
}
</style>
