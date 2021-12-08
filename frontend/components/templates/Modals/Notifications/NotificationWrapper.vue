<template>
  <transition name="wrapper">
    <div id="notification-container" v-show="openNotifications">
      <div class="error_container" v-if="getNotifications.length == 0">
        <creative-img-error
        imgName="jacket-dog2.jpg"
        text="As coisas estão quietas por aqui"/>
      </div>
      <transition-group name="card">
        <div
          v-for="(notif, index) in getNotifications"
          :key="notif.id"
          class="notify-card flex_r"
        >
          <div class="flex_c data">
            <div class="content flex_c">
              <h1
                v-if="notif.title"
                @click="moreLines($event)"
                @mouseleave="lessLines($event)"
              >
                {{ notif.title }}
              </h1>
              <p class="paragraph-2" v-if="notif.text">{{ notif.text }}</p>
            </div>
            <div v-if="notif.hasQuestion" class="answer flex_r">
              <button
                class="btn-4_tiny"
                @click="answerQuestion(2, notif.id, index)"
              >
                aceitar
              </button>
              <button
                class="btn-4_tiny"
                @click="answerQuestion(1, notif.id, index)"
              >
                negar
              </button>
            </div>
          </div>
          <button class="delete" @click="deleteNotification(notif.id, index)">
            <add />
          </button>
        </div>
      </transition-group>
    </div>
  </transition>
</template>

<script>
import Add from '@/components/icons/Add'
import CreativeImgError from '@/components/errors/CreativeImgError'

export default {
  methods: {
    moreLines(event) {
      event.target.classList.add('more')
    },
    lessLines(event) {
      if (event.target.classList.contains('more')) {
        event.target.classList.remove('more')
      }
    },
    answerQuestion(answer, notificationId, index) {
      this.$axios
        .put('/v1/Notification/answer-notification', {
          id: notificationId,
          answer: answer,
        })
        .then((res) => {
          this.$store.commit('notifications/deleteNotification', index)
        })
        .catch((err) => {
          console.log(err.response)
        })
    },
    deleteNotification(notificationId, index) {
      this.$axios
        .delete('/v1/Notification/delete-notification', {
          data: notificationId,
        })
        .then((res) => {
          this.$store.commit('notifications/deleteNotification', index)
        })
        .catch((err) => {
          console.log(err.response)
        })
    },
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
  },
  components: {
    Add,
    CreativeImgError,
  },
}
</script>

<style lang="scss" scoped>
.wrapper-enter-active,
.wrapper-leave-active {
  transition: opacity 200ms;
}
.wrapper-enter,
.wrapper-leave-active {
  opacity: 0;
}

@keyframes deletingCard {
  0% {
    transform: translateX(0);
    opacity: 1;
  }
  100% {
    transform: translateX(20%);
    opacity: 0;
  }
}

// .card-enter-active,
.card-leave-active {
  animation: deletingCard 300ms ease 0ms 1 normal both;
}

// .card-enter, .card-leave-to{
//   opacity: 0;
// }
// .card-enter-to, .card-leave{
//   opacity: 1;
// }

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

  .error_container{
    padding: 10px 5px;
  }
  .notify-card {
    padding: 8px 10px;
    box-sizing: border-box;
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
          cursor: default;

          &.more {
            -webkit-line-clamp: 3;
          }
        }
        p {
          font-size: 11px;
          display: -webkit-box;
          -webkit-line-clamp: 3;
          -webkit-box-orient: vertical;
          overflow: hidden;
          cursor: default;

          &:focus {
            -webkit-line-clamp: 5;
          }
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
