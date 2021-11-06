<template>
  <modal-back v-if="modal.called">
    <div class="modal_container flex_c">
      <div class="top flex_c">
        <h6 class="title-1_2">
          {{ modal.data.title ? modal.data.title : 'Tem certeza?' }}
        </h6>
        <p class="paragraph-2">
          {{
            modal.data.paragraph
              ? modal.data.paragraph
              : 'Esta ação não poderá ser desfeita'
          }}
        </p>
      </div>
      <div class="bottom flex_r">
        <button @click="sendAnswer(false)">Cancelar</button>
        <button @click="sendAnswer(true)">Confirmar</button>
      </div>
    </div>
  </modal-back>
</template>

<script>
import ModalBack from './ModalBack'
export default {
  data() {
    return {
      //   dontCallAgain: false,
      modal: {},
    }
  },
  methods: {
    sendAnswer(answer) {
      this.$store.commit('closeModal', answer)
    },
  },
  computed: {
    allModalInfo() {
      return this.$store.state.confirmationModal.called
    },
  },
  watch: {
    allModalInfo() {
      this.modal = this.$store.state.confirmationModal
    },
  },
  components: {
    ModalBack,
  },
}
</script>

<style lang="scss" scoped>
.modal_container {
  max-width: 420px;
  width: 90vw;
  background: linear-gradient(160deg, #bbb, #f0f0f0);
  border-radius: 10px;
  box-shadow: -2px -2px 20px #404040;

  .top {
    gap: 5px;
    padding: 16px 24px;
  }
  .bottom {
    justify-content: space-around;

    button {
      box-shadow: inset 0 0 8px transparent;
      flex-grow: 1;
      padding: 16px 24px;
      box-shadow: inset 0 5px 10px #909090;
      transition: box-shadow 250ms;

      &:hover {
        box-shadow: inset 0 5px 10px #747474;
      }
    }
  }
}
</style>
