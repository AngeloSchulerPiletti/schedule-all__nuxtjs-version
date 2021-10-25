<template>
  <div class="flex_c">
      <button @click="getPeople">Click to see people</button>
      <h1>{{persons}}</h1>
  </div>
</template>

<script>
export default {
    data(){
        return {
            persons: "nada",
        }
    },
    methods:{
        getPeople(){
            this.$axios.get('v1/Person').then(response => {
                this.persons = response.data
            });
        },
    }
}
</script>

<style>

</style>