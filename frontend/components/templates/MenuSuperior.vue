<template>
  <div class="wrapper flex_r border-soft">
    <div id="user-profile_container" class="flex_r">
      <div class="pic_container pseudo spare-button">
        <img
          :src="`https://avatars.dicebear.com/api/human/${userData.userName}.svg`"
          alt=""
        />
      </div>
      <div class="flex_c user_info">
        <span>{{ userData.userName }}</span>
        <span>{{ userData.fullName }}</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    userData: Object,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  padding: 12px 40px 20px 40px;
  background: linear-gradient(2deg, rgba(0, 0, 0, 0.1), rgba(0, 0, 0, 0.1));

  &::before {
    box-shadow: 6px 6px 14px #a9a8b7, -6px -6px 14px #fff,
      inset -4px -4px 14px #F0F0F0;
  }

  #user-profile_container {
    gap: 20px;

    .pic_container {
      width: 66px;
      height: 66px;
      overflow: hidden;
      padding: 5px 5px 0 5px;
      border-radius: 100%;
      display: flex;
      align-items: flex-end;
      position: relative;
      cursor: pointer;

      &::before {
        z-index: 5;
        border-radius: 100%;
      }
    }
    .user_info {
      justify-content: center;
      gap: 6px;

      span:nth-child(1) {
        font-size: 22px;
        font-weight: 500;
      }
      span:nth-child(2) {
        font-size: 13px;
        font-weight: 400;
      }
    }
  }
}
</style>
