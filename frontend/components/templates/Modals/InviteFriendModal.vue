<template>
  <modal-back v-if="show">
    <div class="modal_container flex_c">
      <div id="close" @click="close"><add-icon /></div>
      <div class="top flex_c">
        <h6 class="title-1_2">Adicionar amigos</h6>
        <p class="paragraph-2">
          Digite o username único do seu amigo. Por exemplo, o seu é
          <strong>{{ userData.userName }}</strong>
        </p>
      </div>
      <div :class="`bottom flex_r ${showClass}`">
        <input
          class="input_text-1"
          type="text"
          placeholder="unique_username123"
          v-model="nickname"
        />
        <button class="button-1 btn-3" @click="invite">Convidar</button>
      </div>
      <div v-show="errors.length > 0" class="errors">
        <ul class="flex_c">
          <li v-for="(error, index) in errors" :key="index">{{ error }}</li>
        </ul>
      </div>
    </div>
  </modal-back>
</template>

<script>
import ModalBack from './ModalBack'
import Add from '@/components/icons/Add'

export default {
  data() {
    return {
      show: false,
      showClass: '',
      nickname: null,
      errors: [],
    }
  },
  computed:{
    
  },
  methods: {
    close() {
      this.showClass = ''
      this.$store.commit('changeInviteFriendModalVisibility')
    },
    invite() {
      this.$axios
        .post('v1/Friendship/invite-friend', this.nickname)
        .then((res) => {
          this.close();
          //Mostra mensagem de sucesso
        })
        .catch((err) => {
          var messages = err.response.data.messages;
          this.errors = messages ? messages : ["Esse usuário não existe"];
        })
    },
  },
  computed: {
    userData() {
      return this.$cookies.get('userData')
    },
    modalShow() {
      this.show = this.$store.state.inviteFriendModal.called
      if (this.show) {
        setTimeout(() => {
          this.showClass = 'ended'
        }, 10)
      }
      return
    },
  },
  watch: {
    modalShow() {},
  },
  components: {
    ModalBack,
    'add-icon': Add,
  },
}
</script>

<style lang="scss" scoped>
.modal_container {
  max-width: 600px;
  width: 90vw;
  background: linear-gradient(160deg, #bbb, #f0f0f0);
  border-radius: 10px;
  box-shadow: -2px -2px 20px #404040;
  position: relative;
  gap: 20px;
  padding: 20px 20px;

  #close {
    position: absolute;
    right: 14px;
    top: 14px;
    cursor: pointer;
    transform: rotate(45deg);
  }

  .top {
    gap: 5px;
  }
  .bottom {
    gap: 15px;
    input {
      flex-grow: 1;
      opacity: 0;
      transition: opacity 350ms;
    }
    button {
      transform: translateX(-150%);
      transition: transform 250ms ease, box-shadow 300ms;
    }

    &.ended {
      input {
        opacity: 1;
      }
      button {
        transform: translateX(0);
      }
    }
  }
}
</style>
