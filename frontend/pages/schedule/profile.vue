<template>
  <div class="flex_r" id="profile-container">
    <div id="profile-configs">
      <profile-config />
    </div>
    <div id="friends-list">
      <friends-list />
    </div>
  </div>
</template>

<script>
import ProfileConfig from '@/components/templates/Profile/ProfileConfig'
import FriendsList from '@/components/templates/Profile/FriendsList'

export default {
  layout: 'dashboard',
  components: {
    ProfileConfig,
    FriendsList,
  },
}
</script>

<style lang="scss" scoped>
#profile-container {
  width: 100%;
  height: 100%;

  #profile-configs {
    flex-grow: 1;
    height: 100%;
  }
  #friends-list {
    height: fit-content;
    max-height: calc(100% - 50px);
  }
}
</style>
