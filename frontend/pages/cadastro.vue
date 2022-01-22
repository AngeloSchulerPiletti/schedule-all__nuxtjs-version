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
import taskTokenService from '@/services/taskTokenService.js'

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
    getWalletAddress() {
      return this.$store.state.wallet.walletAddress
    },
  },
  methods: {
    connectWallet() {
      if (this.hasMetaMask) {
        this.$userWeb3.eth
          .requestAccounts()
          .then((accounts) => {
            let walletAddress = accounts[0]
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
      if (!data.userName) return (this.errors = ['Preencha o nickname'])

      this.checkTaskTokenUserEventLog(data)
        .then(async (nicknameAlreadyLogged) => {
          if (!nicknameAlreadyLogged) {
            this.taskTokenUserEventRequest(data).then(() => {
              this.apiUserRequest(data)
            })
          }
          this.apiUserRequest(data)
        })
        .catch((err) => {
          this.errors = [
            'Houve um erro ao verificar os eventos da rede, tente novamente mais tarde',
          ]
        })
    },
    checkTaskTokenUserEventLog(data) {
      return taskTokenService
        .eventUserSignedUp(this.$smartContract, this.getWalletAddress)
        .then((receipt) => {
          var nickname = ''
          if (receipt[0] && receipt[0].returnValues[1])
            nickname = receipt[0].returnValues[1]

          return data.userName == nickname
        })
    },
    async taskTokenUserEventRequest(data) {
      await taskTokenService.sendUserSignedUp(
        this.$smartContract,
        data.userName,
        this.getWalletAddress
      )
    },
    apiUserRequest(data) {
      this.$axios
        .post('/v1/Signup/signup', data)
        .then((response) => {
          this.$cookies.set('userData', response.data)
          this.$router.push('/schedule')
        })
        .catch((err) => {
          if (!navigator.onLine)
            this.errors = [
              'Você está offline. Se você já pagou a transação não precisará pagar novamente, apenas use o mesmo nickname',
            ]
          else
            this.errors = [
              'Houve um erro. Se você já pagou a transação não precisará pagar novamente, apenas use o mesmo nickname',
            ]
        })
    },
  },
  components: {
    FormPage,
  },
}
</script>

<style lang="scss" scoped></style>
