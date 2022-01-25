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
        @click="connectWalletIfMetaMask"
      >
        Conectar Wallet
      </button>
    </template>
  </form-page>
</template>

<script>
import FormPage from '@/components/containers/FormPage.vue'
import {connectWallet} from '@/utils/walletConnectionManager.js'
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
    async connectWalletIfMetaMask() {
      if (this.hasMetaMask) {
        await connectWallet(this.$userWeb3, this.$store, this.$cookies);
      } else {
        this.errors.push(
          'Você precisa da MetaMask para acessar os recursos do Schedule-All'
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
          } else this.apiUserRequest(data)
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
          if (receipt.length > 0) {
            var nicknames = []
            for (let i = 0; i < receipt.length; i++) {
              nicknames.push(receipt[i].returnValues[1])
            }
            var nicknamesStr = nicknames.join(', ')

            if (nicknames.includes(data.userName)) {
              return true
            }
            this.errors = [
              `Você já pagou pelos seguintes nicknames: ${nicknamesStr}`,
              `Para não pagar uma nova taxa utilize um dos nicknames já pagos`,
              'Caso queira usar um nickname diferente, continue',
            ]
          }
          return false
        })
    },
    async taskTokenUserEventRequest(data) {
      return await taskTokenService.sendUserSignedUp(
        this.$smartContract,
        data.userName,
        this.getWalletAddress
      )
    },
    apiUserRequest(data) {
      data.walletAddress = this.getWalletAddress
      this.$axios
        .post('/v1/Signup/signup', data)
        .then((response) => {
          this.$cookies.set('userData', response.data)
          this.$router.push('/schedule')
        })
        .catch((err) => {
          if (err.response && err.response.data.messages) {
            this.errors = err.response.data.messages
          } else if (!navigator.onLine)
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
