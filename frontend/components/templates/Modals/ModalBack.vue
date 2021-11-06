<template>
  <transition name="apper">
    <div class="back flex_c">
      <slot></slot>
    </div>
  </transition>
</template>

<script>
export default {}
</script>

<style lang="scss" scoped>
.apper-enter-active,
.apper-leave-active {
  transition: opacity 300ms;
}
.apper-enter,
.apper-leave-active {
  opacity: 0;
}

.back {
  position: fixed;
  z-index: 1000;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: #0003;
  align-items: center;
  justify-content: center;
}
</style>
