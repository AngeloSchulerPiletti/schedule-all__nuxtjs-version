<template>
  <FormPage title="Cadastro" :form_structure="form_structure">
    <template v-slot:default="form_data">
        <button class="button-1 btn-2"  @click="send(form_data)">Criar Conta</button>
    </template>
  </FormPage>
</template>

<script>
import FormPage from '@/components/containers/FormPage.vue'

export default {
  data() {
    return {
      form_structure: {
        inputs: [
          {
            label: 'Nickname',
            id: 'nickname',
            placeholder: 'fulano_X123',
            type: 'text',
          },
          {
            label: 'Nome Completo',
            id: 'name',
            placeholder: 'Fulano de Tal',
            type: 'text',
          },
          {
            label: 'Email',
            id: 'email',
            placeholder: 'fulano@example.com',
            type: 'email',
          },
          {
            label: 'Senha',
            id: 'password',
            type: 'password',
          },
          {
            label: 'Confirmar Senha',
            id: 'password_confirmation',
            type: 'password',
          },
        ],
      },
    }
  },
  methods:{
    send(data){
      console.log(data.form_data.usuario);
    },
  },
  components: {
    FormPage,
  },
}
</script>

<style lang="scss" scoped></style>
