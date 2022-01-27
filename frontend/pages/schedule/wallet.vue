<template>
  <div>
    <wallet-status/>
  </div>
</template>

<script>
import WalletStatus from "@/components/templates/Wallet/WalletStatus.vue";

export default {
  layout: 'dashboard',
  head: {
    title: 'Wallet',
  },
  mounted() {
    this.$store.commit('setDashboardPageStatus', 'loaded')
  },
  components:{
    WalletStatus,
  },
}
</script>

<style></style>
