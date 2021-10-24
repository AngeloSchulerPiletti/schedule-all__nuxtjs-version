<template>

</template>

<script>
export default {
  computed:{
    counter(){
      return this.$store.state.counter;
    }
  },
  methods:{
    incrementState(){
      this.$store.commit('increment');
    }
  }
}
</script>