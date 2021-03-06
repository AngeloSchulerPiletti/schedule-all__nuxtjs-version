<template>
  <div class="wrapper border-soft flex_c" :data-state="menuState">
    <pressable-button @state="listenState"><friends /></pressable-button>
    <hr class="division_3d" />
    <div id="friends-view-container" class="up scroll-1">
      <div v-if="haveFriends" id="friend-cards-container" class="flex_c">
        <div
          class="friend-card flex_r"
          v-for="(user, index) in friendsList"
          :key="index"
        >
          <drop-menu-list
            :menu="menu"
            :uniqueKey="user.friendshipId.toString()"
            defaultPosition="goRight"
          />
          <div class="data-user flex_c">
            <h6 class="title-1_4 fullname upper">{{ user.fullName }}</h6>
            <span class="username">{{ user.userName }}</span>
          </div>
        </div>
      </div>
      <creative-img-error
        v-else-if="haveFriends == false || fetchError"
        imgName="alone-man.jpg"
        :text="fetchError ? 'Houve um erro' : 'Você ainda não tem amigos'"
      />
    </div>
  </div>
</template>

<script>
import Friends from '@/components/icons/Friends.vue'
import PressableButton from '@/components/PressableButton'
import DropMenuList from '@/components/DropMenuList'
import CreativeImgError from '@/components/errors/CreativeImgError'
import Trash from '@/components/icons/Trash.vue'

export default {
  data() {
    return {
      menuState: 'null',
      friendsList: [],
      haveFriends: null,
      fetchError: false,
      modalConfirmation: {
        title: 'Quer mesmo deixar de ser amigo dessa pessoa?',
        subject: 'deleteFriendship',
        paragraph: 'Você poderá voltar a ser amigo dessa pessoa no futuro.',
        friendshipOnDelete: null,
      },
      menu: {
        info: {
          idPrefix: 'friendslist-actions',
        },
        data: [
          {
            icon: Trash,
            text: 'Excluir',
            callback: (friendshipId) => {
              let modalData = this.modalConfirmation
              this.$store.commit('openModal', {
                subject: modalData.subject,
                title: modalData.title,
                paragraph: modalData.paragraph,
              })
              this.modalConfirmation.friendshipOnDelete = friendshipId
            },
          },
          // Futuramente um link de share de algum amigo
          // Futuramente um action de copy o username de um amigo
        ],
      },
    }
  },
  computed: {
    deleteAnswer() {
      if (
        this.$store.state.confirmationModal.subject ==
        this.modalConfirmation.subject
      ) {
        return this.$store.state.confirmationModal.answer
      }
    },
  },
  watch: {
    deleteAnswer(newValue) {
      if (newValue) {
        this.$axios
          .delete('v1/Friendship/delete-friendship', {
            data: this.modalConfirmation.friendshipOnDelete,
          })
          .then((res) => {
            for (let i = 0; i < this.friendsList.length; i++) {
              if (
                this.friendsList[i].friendshipId ==
                this.modalConfirmation.friendshipOnDelete
              ) {
                console.log('using the data stored')
                this.friendsList.splice(i, 1)
              }
            }
            if (this.friendsList.length <= 0) this.haveFriends = false
            // Mostra mensagem de sucesso
          })
          .catch((err) => {
            // displays an error
          })
      }
      console.log('Cleaning answer')
      this.$store.commit('cleanAnswer')
    },
    haveFriends(newValue) {
      if (newValue != null) {
        this.$store.commit('setDashboardPageStatus', 'loaded')
      }
    },
  },
  methods: {
    listenState(state) {
      if (state) this.menuState = state
    },
    getFriendsList() {
      this.$axios
        .get('v1/Friendship/get-all-friends')
        .then((res) => {
          this.friendsList = res.data
          this.haveFriends = res.data.length > 0 ? true : false
          res.data.forEach(element => {
            console.log(element.fullName);
          });
        })
        .catch((err) => {
          this.$store.commit('setDashboardPageStatus', 'error')
        })
    },
  },
  beforeMount() {
    this.getFriendsList()
  },
  components: {
    Friends,
    PressableButton,
    DropMenuList,
    CreativeImgError,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  height: fit-content;
  max-height: calc(100% - 50px);
  border-top-left-radius: 15px;
  border-bottom-left-radius: 15px;
  padding: 20px 18px;
  gap: 20px;
  box-sizing: content-box;

  & * {
    box-sizing: border-box;
  }

  &::before {
    border-top-left-radius: 15px;
    border-bottom-left-radius: 15px;
    box-shadow: 6px 6px 14px #a9a8b7, -6px -6px 14px #ffffff,
      inset -4px -4px 14px #ffffff, inset 4px 4px 14px #a9a8b7;
  }

  $open-width: 300px;

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
    #friends-view-container {
      opacity: 1;
    }
  }
  &[data-state='null']{
    width: 40px;
    #friends-view-container {
      opacity: 0;
    }
  }
  &[data-state='close'] {
    animation: close_anim 300ms ease 0ms 1 normal forwards;
    #friends-view-container {
      opacity: 0;
    }
  }
  #friends-view-container {
    transition: opacity 200ms;
    height: 100%;
    overflow-y: auto;
    overflow-x: hidden;

    #friend-cards-container {
      gap: 16px;

      #no-friends {
        position: absolute;
        width: $open-width;
      }

      .friend-card {
        width: $open-width;
        gap: 6px;

        .data-user {
          gap: 5px;

          .fullname,
          .username {
            text-overflow: ellipsis;
            display: -webkit-box;
            -webkit-line-clamp: 1; /* number of lines to show */
            line-clamp: 1;
            -webkit-box-orient: vertical;
            overflow: hidden;
          }
          .username {
            font-size: 14px;
            font-style: italic;
          }
        }
      }
    }
  }
}
</style>
