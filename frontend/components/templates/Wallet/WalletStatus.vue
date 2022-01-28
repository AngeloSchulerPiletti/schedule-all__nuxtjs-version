<template>
  <div id="wallet-status-container" class="flex_c wrapper border-soft">
    <h6 class="upper flex_r">
      Status da wallet <connection :class="`status-${isWalletSigned}`" />
      <pressable-button
        class="show-status"
        :goRight="false"
        @state="listenState"
        ><arrows
      /></pressable-button>
    </h6>
    <transition name="dropping">
    <div v-show="showStatus" id="explanation-container" class="flex_c up">
      <div :class="`status-container status-${isWalletSigned}`">
        <p v-if="isWalletSigned">
          A wallet conectada é a wallet que você utilizou no seu cadastro.
          Aproveite todas as funcionalidades do aplicativo!
        </p>
        <p v-else-if="isWalletSigned === null">
          Você precisa conectar sua wallet para a aplicação funcionar
          corretamente
        </p>
        <p v-else>
          A wallet conectada <b>não</b> é a wallet que você utilizou no seu
          cadastro. Sugerimos que você se conecte a ela para poder aproveitar
          todas as funcionalidades do aplicativo. <br />
          Acreditamos que a wallet do seu cadastro seja:
          <b>{{ getSignedWallet }}</b>
        </p>
      </div>
      <div class="actions flex_r">
        <button @click="updateWallet" class="button-3 btn-4_tiny">
          Atualizar
        </button>
        <button
          :class="
            !isWalletConnected
              ? 'button-1 btn-4_tiny'
              : 'button-disabled button-1 btn-4_tiny'
          "
          @click="connectWallet"
        >
          Conectar
        </button>
        <transition name="fade"
          ><span v-show="wasUpdated">Atualizado!</span></transition
        >
      </div>
    </div>
    </transition>
  </div>
</template>

<script>
import {
  connectWallet,
  checkWalletConnection,
} from '@/utils/walletConnectionManager.js'
import Connection from '@/components/icons/Connection.vue'
import Arrows from '@/components/icons/Arrows.vue'
import PressableButton from '@/components/PressableButton.vue'

export default {
  data() {
    return {
      wasUpdated: false,
      showStatus: false,
    }
  },
  computed: {
    isWalletConnected() {
      return this.$store.state.wallet.connected
    },
    isWalletSigned() {
      return this.$store.state.wallet.isTheConnectedWalletSigned
    },
    getSignedWallet() {
      return this.$cookies.get('userData').walletAddress
    },
  },
  methods: {
    async connectWallet() {
      await connectWallet(this.$userWeb3, this.$store, this.$cookies)
    },
    async updateWallet() {
      await checkWalletConnection(this.$store, this.$userWeb3, this.$cookies)
      this.wasUpdated = true
      setTimeout(() => {
        this.wasUpdated = false
      }, 3000)
    },
    listenState(state) {
      if (state == 'open') this.showStatus = true;
      else this.showStatus = false
    },
  },
  components: {
    Connection,
    Arrows,
    PressableButton,
  },
}
</script>

<style lang="scss" scoped>
#wallet-status-container {
  margin: 20px 30px 30px 20px;
  padding: 12px 25px;
  gap: 8px;

  &::before {
    border-radius: 8px;
    box-shadow: 6px 6px 14px #a9a8b7, -6px -6px 14px #ffffff,
      inset -4px -4px 14px #ffffff, inset 4px 4px 14px #a9a8b7;
  }

  h6 {
    gap: 8px;
    align-items: center;

    svg {
      width: 0.85em;
      height: 0.85em;

      &::v-deep {
        & path {
          fill: $check_red;
        }
        &.status-true path {
          fill: $check_green;
        }
      }
    }

    .show-status {
      margin-left: auto;

      svg {
        transform: rotate(90deg);
        width: 30px;
        height: 30px;
        &::v-deep {
          & path {
            fill: #303030;
          }
        }
      }
    }
  }
  #explanation-container {
    gap: 16px;

    .status-container {
      font-size: 14px;
      line-height: 1.4em;
      padding-left: 6px;
      border-left: 2px solid $check_red;

      &.status-true {
        border-left: 2px solid $check_green;
      }
    }
    .actions {
      gap: 10px;
      align-items: flex-end;

      span {
        font-size: 0.6em;
        margin-left: auto;
      }
    }
  }
}
</style>
