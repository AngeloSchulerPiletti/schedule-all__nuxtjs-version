<template>
  <div id="dashboard-layout" class="flex_c">
    <menu-superior :userData="userData" />
    <div id="vertical-container" class="flex_r">
      <menu-lateral />
      <div id="view_container" class="border-soft">
        <div v-if="$store.state.dashboardPageStatus == 'loading'" class="loading">
          loading...
      </div>
      <div v-else-if="$store.state.dashboardPageStatus == 'error'" class="error">
        error
      </div>
      <div v-show="$store.state.dashboardPageStatus == 'loaded'">
        <Nuxt />
      </div>
      </div>
    </div>
  </div>
</template>

<script>
import MenuLateral from '@/components/templates/MenuLateral'
import MenuSuperior from '@/components/templates/MenuSuperior'

export default {
  middleware: 'auth',
  computed: {
    userData() {
      return this.$cookies.get('userData')
    },
  },
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
  gap: $dashboard_components_spacing;
  // background-color: #00000050;

  #vertical-container {
    flex-grow: 1;
    gap: $dashboard_components_spacing;

    #view_container {
      padding: 20px;
      flex-grow: 1;
      border-top-left-radius: 15px;

      background: linear-gradient(
        135deg,
        rgba(0, 0, 0, 0.1),
        rgba(0, 0, 0, 0.1)
      );

      &::before {
        border-top-left-radius: 15px;
        box-shadow: 6px 6px 14px #a9a8b7, -6px -6px 14px #fff,
          inset -4px -4px 14px #fff, inset 4px 4px 14px #a9a8b7;
      }
    }
  }
}
</style>
