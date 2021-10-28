<template>
  <form-page title="Entrar" :form_structure="form_structure" :errors="errors">
    <template v-slot:default="form_data">
      <button class="button-1 btn-2" @click="send(form_data)">Entrar</button>
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
            label: 'Nick ou Email',
            id: 'userName',
            placeholder: 'fulano_X123',
            type: 'text',
          },
          {
            label: 'Senha',
            id: 'password',
            type: 'password',
          },
        ],
      },
    }
  },
  methods: {
    send(data) {
      this.$axios.post('/v1/Auth/signin', data.form_data).then((response) => {
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
