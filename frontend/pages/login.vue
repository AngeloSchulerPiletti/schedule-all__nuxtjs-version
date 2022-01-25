<template>
  <form-page title="Entrar" :form_structure="form_structure" :errors="errors">
    <template v-slot:default="form_data">
      <button
        :class="
          walletConnected ? 'button-1 btn-2' : 'button-disabled button-1 btn-2'
        "
        @click="send(form_data)"
      >
        Entrar
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

export default {
  middleware: 'auth_not_allowed',
  head: {
    title: 'Login',
  },
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
        await connectWallet(this.$userWeb3, this.$store)
      } else {
        this.errors.push(
          'Você precisa da MetaMask para acessar os recursos do Schedule-All'
        )
      }
    },
    send(data) {
      this.$axios
        .post('/v1/Auth/signin', data.form_data)
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
