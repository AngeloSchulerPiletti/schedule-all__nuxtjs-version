<template>
  <div id="dashboard-layout" class="flex_c">
    <menu-superior v-if="userData" :userData="userData" />
    <div id="vertical-container" class="flex_r">
      <menu-lateral />
      <div id="view_container" class="border-soft">
        <div
          v-if="dashboardPageStatus == 'loading'"
          class="loading"
        >
          loading...
        </div>
        <div
          v-else-if="dashboardPageStatus == 'error'"
          class="error"
        >
          error
        </div>
        <div
          v-show="dashboardPageStatus == 'loaded'"
          class="nuxt_wrapper"
        >
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
      return this.$cookies.get('userData');
    },
    dashboardRouter(){
      return this.$route.name;
    },
    dashboardPageStatus(){
      return this.$store.state.dashboardPageStatus;
    },
  },
  watch:{
    dashboardRouter(oldVal, newVal){
        this.$store.commit('setDashboardPageStatus', 'loading');
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
      padding: 20px 0 0 20px;
      flex-grow: 1;
      border-top-left-radius: 15px;
      position: relative;

      .nuxt_wrapper {
        position: relative;
        z-index: 10;
        height: 100%;
      }

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
