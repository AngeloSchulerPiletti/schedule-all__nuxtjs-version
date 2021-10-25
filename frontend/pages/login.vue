<template>
    <form-page title="Entrar" :form_structure="form_structure">
    <template v-slot:default="form_data">
            <button class="button-1 btn-2" @click="send(form_data)">
              Entrar
            </button>
    </template>
    </form-page>
</template>

<script>
import FormPage from "@/components/containers/FormPage.vue";

export default {
  data(){
    return{
      form_structure:{
        inputs: [
          {
            label: 'Nick ou Email',
            id: 'usuario',
            placeholder: 'fulano_X123',
            type: 'text',
          },
          {
            label: 'Senha',
            id: 'password',
            type: 'password',
          },
        ]
      },
    }
  },
  methods:{
    send(data){
      console.log(data.form_data.usuario);
    },
  },
  components:{
    FormPage,
  }
}
</script>

<style lang="scss" scoped>

</style>