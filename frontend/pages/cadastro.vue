<template>
  <form-page title="Cadastro" :form_structure="form_structure" :errors="errors">
    <template v-slot:default="form_data">
      <button
        :class="
          walletConnected ? 'button-1 btn-2' : 'button-disabled button-1 btn-2'
        "
        @click="createNewUser(form_data.form_data)"
      >
        Criar Conta
      </button>
      <button
        :class="
          hasMetaMask && !walletConnected
            ? 'button-1 btn-2'
            : 'button-disabled button-1 btn-2'
        "
        @click="connectWallet"
      >
        Conectar Wallet
      </button>
    </template>
  </form-page>
</template>

<script>
import FormPage from '@/components/containers/FormPage.vue'

export default {
  middleware: 'auth_not_allowed',
  head: {
    title: 'Cadastro',
  },
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
  computed: {
    walletConnected() {
      return this.$store.state.wallet.connected
    },
    hasMetaMask() {
      return this.$store.state.wallet.hasMetaMask
    },
  },
  methods: {
    async checkTaskTokenUserEventLog() {
      var receipt = await this.$smartContract.getPastEvents('SignedUpUser')
      console.log(receipt)

      return false
    },
    connectWallet() {
      if (this.hasMetaMask) {
        this.$userWeb3.eth
          .requestAccounts()
          .then((accounts) => {
            var walletAddress = accounts[0]
            this.$store.dispatch('wallet/connectedWallet', walletAddress)
          })
          .catch((err) => {
            //Dá nada, mas criar botão "universal" para a pessoa poder conectar enquanto não estiver conectada
          })
      } else {
        this.errors.push(
          'Você pode usar o website sem a MetaMask, mas com ela você tem acesso a muitos outros recursos web3!'
        )
      }
    },
    createNewUser(data) {
      this.taskTokenUserEventRequest(data).then(() => {
        this.checkTaskTokenUserEventLog().then(() => {
          console.log('loguei aquilo')
        })
      })
      // this.checkTaskTokenUserEventLog()
      //   .then((isLogged) => {
      //     if (!isLogged) {
      //       console.log('Não emitiu o evento')
      //       this.taskTokenUserEventRequest(data)
      //         .then((res) => {
      //           // this.apiUserRequest(data)
      //           console.log('tentando criar novo usuário')
      //         })
      //         .catch((err) => {
      //           console.log(err)
      //         })
      //     } else {
      //       console.log('Já emitiu o evento')
      //       // this.apiUserRequest(data)
      //     }
      //   })
      //   .catch((err) => {
      //     throw new Error(err);
      //   })
    },
    async taskTokenUserEventRequest(data) {
      var res = await this.$smartContract.methods
        .userSignedUp(data.userName)
        .send(
          { from: this.$store.state.wallet.walletAddress },
          function (error, transactionHash) {
            console.log(error, transactionHash)
          }
        )
        console.log(res);
    },
    apiUserRequest(data) {
      this.$axios
        .post('/v1/Signup/signup', data)
        .then((response) => {
          this.$cookies.set('userData', response.data)
          this.$router.push('/schedule')
        })
        .catch((err) => {
          if (err.response.data.messages) {
            this.errors = err.response.data.messages
            return
          }
          this.errors = ['Problemas internos, tente mais tarde']
        })
    },
  },
  components: {
    FormPage,
  },
}
</script>

<style lang="scss" scoped></style>
