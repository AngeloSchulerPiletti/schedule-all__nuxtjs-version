<template>
  <transition>
    <div id="notification-container" v-show="openNotifications">
      <div
        v-for="(notif, index) in getNotifications"
        :key="index"
        class="notify-card flex_r"
      >
        <div class="flex_c data">
          <div class="content flex_c">
            <h1 v-if="notif.title">{{ notif.title }}</h1>
            <p class="paragraph-2" v-if="notif.text">{{ notif.text }}</p>
          </div>
          <div v-if="notif.hasQuestion" class="answer flex_r">
            <button class="btn-4_tiny">aceitar</button>
            <button class="btn-4_tiny">negar</button>
          </div>
        </div>
        <button class="delete">
          <add />
        </button>
      </div>
    </div>
  </transition>
</template>

<script>
import Add from '../../../icons/Add.vue'
export default {
  data() {
    return {}
  },
  computed: {
    getNotifications() {
      return this.$store.state.notifications.notifications
    },
    openNotifications() {
      return this.$store.state.notifications.openNotifications
    },
  },
  watch: {
    getNotifications(newValue, oldValue) {
      console.log(`notifications: ${oldValue} to ${newValue}`)
    },
    openNotifications(newValue, oldValue) {},
  },
  components: {
    Add,
  },
}
</script>

<style lang="scss" scoped>
#notification-container {
  position: absolute;
  bottom: 0;
  right: 0;
  transform: translateY(100%);
  width: 300px;
  max-height: 65vh;
  z-index: 100;

  background: linear-gradient(
    135deg,
    rgba(0, 0, 0, 0.22),
    rgba(255, 255, 255, 0.2),
    rgba(0, 0, 0, 0.22)
  );
  box-shadow: 0 0 6px #404040a0;
  backdrop-filter: blur(4px);
  border-radius: 10px;
  padding: 5px 0;

  .notify-card {
    padding: 8px 10px;
    &:not(:last-child) {
      border-bottom: 1px solid #60606060;
    }
    gap: 10px;

    .data {
      flex-grow: 1;
      gap: 8px;
      .content {
        gap: 3px;
        h1 {
          display: -webkit-box;
          -webkit-line-clamp: 1;
          -webkit-box-orient: vertical;
          overflow: hidden;
        }
        p {
          font-size: 11px;
          display: -webkit-box;
          -webkit-line-clamp: 3;
          -webkit-box-orient: vertical;
          overflow: hidden;
        }
      }
      .answer {
        justify-content: space-evenly;
      }
    }
    .delete {
      align-self: flex-start;
      svg {
        transform: rotate(45deg);
        opacity: 0.7;
        transition: transform 300ms ease;
        height: fit-content;

        &:hover {
          transform: rotate(135deg);
        }
      }
    }
  }
}
</style>
