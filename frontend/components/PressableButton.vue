<template>
  <div :class="`close-open up flex_r right-${goRight}`" :data-state="menuState">
    <button @click="changeMenuState">
      <slot></slot>
    </button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      menuState: 'close',
    }
  },
  methods: {
    changeMenuState() {
      if (this.menuState == 'open') this.menuState = 'close'
      else this.menuState = 'open'
      this.emitState()
    },
    emitState() {
      this.$emit('state', this.menuState)
    },
  },
  props: {
    goRight: Boolean,
  },
}
</script>

<style lang="scss" scoped>
.close-open {
  svg {
    width: 35px;
    height: 35px;
    cursor: pointer;
    padding: 5px;
    border-radius: 100%;
    box-shadow: inset 0 0 12px #00000060;
    transition: 200ms box-shadow;

    &:hover {
      box-shadow: inset 0 0 12px #000000a0;
    }
  }
}
.right-true {
  justify-content: flex-end;
  width: 100%;
}


[data-state='close'] {
  svg {
    box-shadow: 0 0 12px #00000060;
    transition: 200ms box-shadow;

    &:hover {
      box-shadow: 0 0 12px transparent;
    }
  }
}
</style>
