<template>
  <div class="flex_r" id="profile-container">
    <div id="profile-configs"></div>
    <div id="friends-list" class="border-soft flex_c" :data-state="menuState">
      <div class="close-open up flex_r">
        <button @click="changeMenuState">
          <friends />
        </button>
      </div>
      <hr class="division_3d" />
      <div v-if="fetchError" id="fetch-error">Houve um erro</div>
      <div v-if="haveFriends == false" id="no-friends">Você não tem amigos</div>
      <div v-else id="friend-card-container" class="flex_c scroll-1 up">
        <div
          class="friend-card flex_r"
          v-for="(user, index) in friendsList"
          :key="index"
        >
          <button class="menu up"><three-dots-menu /></button>
          <div class="data-user flex_c">
            <h6 class="title-1_4">{{ user.fullName }}</h6>
            <span class="username">{{ user.userName }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Friends from '@/components/icons/Friends.vue'
import ThreeDotsMenu from '@/components/icons/ThreeDotsMenu.vue'

export default {
  layout: 'dashboard',
  data() {
    return {
      menuState: 'close',
      friendsList: [],
      haveFriends: null,
      fetchError: false,
    }
  },
  watch: {
    haveFriends(newValue) {
      if (newValue != null) {
        this.$store.commit('setDashboardPageStatus', 'loaded')
      }
    },
  },
  beforeMount() {
    this.getFriendsList()
  },
  methods: {
    changeMenuState() {
      if (this.menuState == 'open') {
        this.menuState = 'close'
        return
      }
      this.menuState = 'open'
    },
    getFriendsList() {
      this.$axios
        .get('v1/Friendship/get-all-friends')
        .then((res) => {
          this.friendsList = res.data
          this.haveFriends = res.data.length > 0 ? true : false
        })
        .catch((err) => {
          this.$store.commit('setDashboardPageStatus', 'error')
        })
    },
  },
  components: {
    Friends,
    ThreeDotsMenu,
  },
}
</script>

<style lang="scss" scoped>
#profile-container {
  position: relative;
  width: 100%;
  height: 100%;

  #profile-configs {
    background-color: blue;
  }
  #friends-list {
    border-top-left-radius: 15px;
    position: absolute;
    right: 0;
    top: 0;
    bottom: 0;
    padding: 20px 18px;
    gap: 20px;
    box-sizing: content-box;

    & * {
      box-sizing: border-box !important;
    }

    &::before {
      border-top-left-radius: 15px;
      box-shadow: 6px 6px 14px #a9a8b7, -6px -6px 14px #ffffff,
        inset -4px -4px 14px #ffffff, inset 4px 4px 14px #a9a8b7;
    }

    $open-width: 300px;
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

    @keyframes open_anim {
      0% {
        width: 40px;
      }
      100% {
        width: $open-width;
      }
    }
    @keyframes close_anim {
      0% {
        width: $open-width;
      }
      100% {
        width: 40px;
      }
    }

    &[data-state='open'] {
      animation: open_anim 300ms ease 0ms 1 normal forwards;
      #friend-card-container {
        opacity: 1;
      }
    }
    &[data-state='close'] {
      animation: close_anim 300ms ease 0ms 1 normal forwards;

      .close-open {
        svg {
          box-shadow: 0 0 12px #00000060;
          transition: 200ms box-shadow;

          &:hover {
            box-shadow: 0 0 12px transparent;
          }
        }
      }
      #friend-card-container {
        opacity: 0;
      }
    }
    #friend-card-container {
      transition: opacity 200ms;
      height: 100%;
      gap: 14px;
      overflow-y: auto;
      overflow-x: hidden;

      .friend-card {
        width: $open-width;
        gap: 6px;

        .data-user {
          gap: 5px;

          .username {
            font-size: 14px;
            font-style: italic;
          }
        }
        .menu {
          width: 16px;
          height: 16px;
        }
      }
    }
  }
}
</style>
