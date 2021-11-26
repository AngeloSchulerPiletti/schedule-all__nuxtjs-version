<template>
  <div class="wrapper flex_r border-soft">
    <div id="user-profile-menu_container" class="flex_r"><NuxtLink to="/schedule/profile">
      <div class="pic_container pseudo spare-button">
        <img
          :src="`https://avatars.dicebear.com/api/human/${userData.userName}.svg`"
          alt=""
        />
      </div></NuxtLink>
      <div class="flex_c user_info">
        <span>{{ userData.userName }}</span>
        <span>{{ userData.fullName }}</span>
      </div>
    </div>
    <div id="dashboard-options_container" class="flex_r">
      <button @click="openInviteFriendModal">
        <invite-friend class="widther" />
      </button>
      <button>
        <notification />
      </button>
      <button><wallet/></button>
      <button><configuration /></button>
    </div>
  </div>
</template>

<script>
import InviteFriend from '@/components/icons/InviteFriend'
import Notification from '@/components/icons/Notification'
import Wallet from '@/components/icons/Wallet'
import Configuration from '@/components/icons/Configuration'

export default {
  methods: {
    openInviteFriendModal() {
      this.$store.commit('changeInviteFriendModalVisibility')
    },
  },
  props: {
    userData: Object,
  },
  components: {
    InviteFriend,
    Notification,
    Configuration,
    Wallet,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  padding: 12px 40px 20px 40px;
  background: linear-gradient(2deg, rgba(0, 0, 0, 0.1), rgba(0, 0, 0, 0.1));
  justify-content: space-between;

  &::before {
    box-shadow: 6px 6px 14px #a9a8b7, -6px -6px 14px #fff,
      inset -4px -4px 14px #f0f0f0;
  }

  #user-profile-menu_container {
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
      $img_scale: 1.1;
      img{
        transform: scale($img_scale);
        transition: transform 200ms;
      }
      &:hover img{
        transform: scale($img_scale) translate(3px, 3px);
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

  #dashboard-options_container {
    align-items: center;
    gap: 8px;
    position: relative;
    z-index: 10;

    svg {
      width: 20px;
      height: 20px;
      cursor: pointer;

      &::v-deep {
        path {
          transition: fill 500ms;
          fill: #303030;
        }
        &:hover {
          path {
            fill: #000;
          }
        }
      }
    }
    .widther {
      width: 28px !important;
    }
  }
}
</style>
