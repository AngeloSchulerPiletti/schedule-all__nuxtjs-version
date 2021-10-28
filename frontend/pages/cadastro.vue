<template>
  <form-page title="Cadastro" :form_structure="form_structure" :errors="errors">
    <template v-slot:default="form_data">
        <button class="button-1 btn-2"  @click="send(form_data)">Criar Conta</button>
    </template>
  </form-page>
</template>

<script>
import FormPage from '@/components/containers/FormPage.vue'

export default {
  middleware: 'auth_not_allowed',
  data() {
    return {
      errors: [],
      form_structure: {
        inputs: [
          {
            label: 'Nickname',
            id: 'userName',
            placeholder: 'fulano_X123',
            type: 'text',
          },
          {
            label: 'Nome Completo',
            id: 'fullName',
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
            id: 'passwordConfirmation',
            type: 'password',
          },
        ],
      },
    }
  },
  methods:{
    send(data){
      this.$axios.post('/v1/Signup/signup', data.form_data).then((response) => {
          this.$cookies.set('userData', response.data);
          this.$router.push('/schedule');
      }).catch(err => {
        if (err.response.data.messages) {
          this.errors = err.response.data.messages; 
        }
        console.log(err);
        // err.response.data.messages.map(m => console.log(m));
      });
    },
  },
  components: {
    FormPage,
  },
}
</script>

<style lang="scss" scoped></style>
