<template>
  <div class="error-container flex_c">
    <img :src="require(`~/assets/images/errors/${imgName}`)" />
    <p class="advice">{{ text }}</p>
  </div>
</template>

<script>
export default {
  props: {
    imgName: String,
    text: String,
  },
}
</script>

<style lang="scss" scoped>
.error-container {
  align-items: center;
  gap: 20px;
  height: 100%;
  overflow: hidden;

  p {
    min-width: 180px;
  }
  img {
    width: 180px;
    height: 180px;
    opacity: 0.6;
    border-radius: 100%;
    object-fit: cover;
  }
}
</style>
