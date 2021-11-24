<template>
  <div class="flex_r">
    <div id="profile-configs">
      
    </div>
    <div id="friends-list">
      <div v-if="fetchError" id="fetch-error">Houve um erro</div>
      <div v-if="haveFriends == false" id="no-friends">Você não tem amigos</div>
      <div v-else id="friend-card-container">
        <button>
          <friends />
        </button>
        <div
          class="friend-card"
          v-for="(user, index) in friendsList"
          :key="index"
        >
          <span>{{ user.fullName }}</span>
          <span>{{ user.userName }}</span>
          <button><three-dots-menu /></button>
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

<style lang="scss" scoped></style>
