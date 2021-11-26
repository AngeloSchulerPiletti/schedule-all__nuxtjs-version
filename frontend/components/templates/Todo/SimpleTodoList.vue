<template>
  <div class="wrapper">
    <div v-if="simpletodos[0].length > 0" class="cards-container grid">
      <div
        :class="`card pseudos_base flex_r ${simpletodo.finished}`"
        v-for="(simpletodo, index) in simpletodos[0]"
        :key="index"
        @mouseleave="hideMenu"
        :id="`task-${simpletodo.id}`"
      >
        <div class="category_mark" v-if="simpletodo.categoryId > 0">
          <span>{{ categories[0][simpletodo.categoryId] }}</span>
        </div>
        <div class="left flex_c">
          <div class="title_container pseudo_base">
            <input
              class="title"
              type="text"
              autocomplete="off"
              :value="simpletodo.title"
              @focusin="fieldIn($event, simpletodo.title)"
              @focusout="fieldOut($event, simpletodo, 'title')"
            />
          </div>
          <textarea
            v-show="checkIfHaveDescription(simpletodo)"
            data-enable-grammarly="false"
            class="description scroll-1 scroll-tiny"
            type="text"
            autocomplete="off"
            :ref="`description-${simpletodo.id}`"
            :value="simpletodo.description"
            @focusin="fieldIn($event, simpletodo.description)"
            @focusout="fieldOut($event, simpletodo, 'description')"
          ></textarea>
        </div>
        <div
          :class="`right ${
            checkIfHaveDescription(simpletodo) ? 'flex_c' : 'flex_r'
          }`"
        >
          <div class="menu_container">
            <button
              class="menu_button"
              @click="showMenu(simpletodo.id, $event)"
            >
              <three-dots-menu class="icon" />
            </button>
            <menu
              class="menu_options menu-1 flex_c"
              v-show="cardMenu == simpletodo.id"
              :id="`menu-${simpletodo.id}`"
              @mouseleave="hideMenu"
            >
              <li class="flex_r" @click="deleteSimpletodo(simpletodo.id)">
                <trash-icon class="icon trash" /><span>Delete</span>
              </li>
              <li v-if="!simpletodo.finished" class="flex_r">
                <friends-icon class="icon" /><span>Convidar amigo</span>
              </li>
              <li
                v-if="!simpletodo.finished"
                class="flex_r extensible"
                @mouseleave="hideSubMenu"
                @mouseenter="showSubMenu(simpletodo.id, $event)"
              >
                <edit-icon class="icon" /><span
                  >{{
                    simpletodo.categoryId != 0 ? 'Alterar' : 'Adicionar'
                  }}
                  categoria</span
                >
                <menu
                  v-show="simpletodoSubOptionId == simpletodo.id"
                  class="
                    flex_c
                    menu_suboptions
                    upper
                    menu-1
                    scroll-1 scroll-tiny
                  "
                  :id="`submenu-${simpletodo.id}`"
                >
                  <li
                    v-for="(title, id) in categories[0]"
                    :key="id"
                    @click="changeSimpletodoCategory(id, simpletodo)"
                  >
                    <span>{{ title }}</span>
                  </li>
                </menu>
              </li>
              <li
                v-if="!simpletodo.finished && simpletodo.categoryId != 0"
                class="flex_r"
                @click="changeSimpletodoCategory(0, simpletodo)"
              >
                <edit-icon class="icon" /><span>Remover categoria</span>
              </li>
              <li
                v-if="
                  !simpletodo.finished && !checkIfHaveDescription(simpletodo)
                "
                class="flex_r"
                @click="addDescription(simpletodo.id)"
              >
                <edit-icon class="icon" /><span>Adicionar descrição</span>
              </li>
            </menu>
          </div>
          <div
            :class="`options_container  ${
              checkIfHaveDescription(simpletodo) ? 'flex_c' : 'flex_r'
            }`"
          >
            <button
              class="important"
              @click="changeSimpletodoImportance(simpletodo.id)"
            >
              <star-icon :class="`icon ${simpletodo.important}`" />
            </button>
            <button
              class="finish"
              @click="changeSimpletodoState(simpletodo.id)"
            >
              <check-icon :class="`icon ${simpletodo.finished}`" />
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="error-container flex_c" v-else>
      <img src="~/assets/images/errors/sad-baby.png" />
      <p class="advice">Você ainda não tem nenhuma tarefa...</p>
    </div>
  </div>
</template>

<script>
import Star from '@/components/icons/Star'
import ThreeDotsMenu from '@/components/icons/ThreeDotsMenu'
import Check from '@/components/icons/Check'
import Trash from '@/components/icons/Trash'
import Edit2 from '@/components/icons/Edit2'
import Friends from '@/components/icons/Friends'

export default {
  data() {
    return {
      clicked: false,
      fieldCache: '',
      cardMenu: 0,
      simpletodoSubOptionId: 0,
      modalSubjects: {
        onDelete: 'deleteSimpletodo',
      },
      simpletodoOnDelete: 0,
      menuElementCache: null,
      submenuElementCache: null,
      descriptionOnShowId: 0,
    }
  },
  computed: {
    deleteAnswer() {
      if (
        this.$store.state.confirmationModal.subject ==
        this.modalSubjects.onDelete
      ) {
        return this.$store.state.confirmationModal.answer
      }
    },
  },
  watch: {
    deleteAnswer(oldValue, newValue) {
      if (newValue) {
        this.$axios
          .delete('v1/SimpleTodo/delete-simpletodo', {
            data: this.simpletodoOnDelete,
          })
          .then((res) => {
            this.$el
              .querySelector(`#task-${this.simpletodoOnDelete}`)
              .classList.add('being_removed')
            setTimeout(() => {
              this.$el
                .querySelector(`#task-${this.simpletodoOnDelete}`)
                .classList.remove('being_removed')
              this.$store.commit(
                'simpletodo/deleteSimpletodo',
                this.simpletodoOnDelete
              )
            }, 210)
          })
          .catch((err) => {
            //joga o erro para as notificações
          })
      }
      this.$store.commit('cleanAnswer')
    },
  },
  methods: {
    checkIfHaveDescription(simpletodo) {
      if (simpletodo.description || this.descriptionOnShowId == simpletodo.id) {
        return true
      }
    },
    addDescription(simpletodoId) {
      this.descriptionOnShowId = simpletodoId
      setTimeout(() => {
        this.$refs[`description-${simpletodoId}`][0].focus()
      }, 0)
      this.hideMenu()
    },
    closeDescription(fieldValue) {
      if (fieldValue == '') this.descriptionOnShowId = 0
    },
    showMenu(simpletodoId) {
      this.cardMenu = simpletodoId
      let element = this.$el.querySelector(`#menu-${simpletodoId}`)
      this.menuElementCache = element
      this.checkMenuPositionOnScreen(element)
    },
    hideMenu() {
      this.cardMenu = 0
      this.menuElementCache
        ? this.menuElementCache.classList.remove('normal')
        : null
      this.menuElementCache = null
    },
    showSubMenu(simpletodoId) {
      this.simpletodoSubOptionId = simpletodoId
      let element = this.$el.querySelector(`#submenu-${simpletodoId}`)
      this.submenuElementCache = element
      this.checkMenuPositionOnScreen(element)
    },
    hideSubMenu() {
      this.simpletodoSubOptionId = 0
      this.submenuElementCache
        ? this.submenuElementCache.classList.remove('normal')
        : null
      this.submenuElementCache = null
    },
    checkMenuPositionOnScreen(element) {
      setTimeout(() => {
        let elRight = element.getBoundingClientRect().right,
          elBottom = element.getBoundingClientRect().bottom,
          bodyRight = document.body.offsetWidth,
          bodyBottom = document.body.offsetHeight

        if (elRight > bodyRight && elBottom > bodyBottom) {
          element.classList.add('goBoth')
        } else if (elBottom > bodyBottom) {
          element.classList.add('goTop')
        } else if (elRight > bodyRight) {
          element.classList.add('goLeft')
        }
        element.classList.add('normal')
      }, 1)
    },
    fieldIn(event, oldValue) {
      event.path[1].classList.add('focused')
      this.fieldCache = oldValue
    },
    fieldOut(event, simpletodo, fieldName) {
      var fieldValue = event.target.value
      event.path[1].classList.remove('focused')

      if (fieldValue != this.fieldCache) {
        this.callToSaveSimpletodo(fieldValue, simpletodo, fieldName).then(
          (res) => {
            if (fieldName == 'description') {
              this.closeDescription(fieldValue)
            }
          }
        )
      } else if (fieldName == 'description') {
        this.closeDescription(fieldValue)
      }

      this.fieldCache = ''
    },
    async callToSaveSimpletodo(fieldNewValue, simpletodo, fieldName) {
      let newSimpletodo = { ...simpletodo }
      newSimpletodo[fieldName] = fieldNewValue
      await this.updateSimpletodo(newSimpletodo)
    },
    async updateSimpletodo(newSimpletodo) {
      await this.$axios
        .put('v1/SimpleTodo/update-simpletodo', newSimpletodo)
        .then((res) => {
          this.$store.commit('simpletodo/updateSimpletodo', newSimpletodo)
        })
    },
    changeSimpletodoState(simpletodoId) {
      this.$axios
        .patch('v1/SimpleTodo/change-simpletodo-state', simpletodoId)
        .then((res) => {
          this.$store.commit('simpletodo/updateSimpletodo', res.data)
        })
        .catch((err) => {
          //Futuramente irá throw notificação de erro
        })
    },
    changeSimpletodoImportance(simpletodoId) {
      this.$axios
        .patch('v1/SimpleTodo/change-simpletodo-importance', simpletodoId)
        .then((res) => {
          this.$store.commit('simpletodo/updateSimpletodo', res.data)
        })
        .catch((err) => {
          //Futuramente irá throw notificação de erro
        })
    },
    changeSimpletodoCategory(newCategoryId, simpletodo) {
      let newSimpletodo = { ...simpletodo }
      newSimpletodo.categoryId = newCategoryId
      this.updateSimpletodo(newSimpletodo).then((res) => {
        this.hideMenu()
      })
    },
    deleteSimpletodo(simpletodoId) {
      this.$store.commit('openModal', { subject: this.modalSubjects.onDelete })
      this.simpletodoOnDelete = simpletodoId
    },
  },
  props: {
    simpletodos: Array,
    categories: Array,
  },
  components: {
    'star-icon': Star,
    ThreeDotsMenu,
    'check-icon': Check,
    'trash-icon': Trash,
    'edit-icon': Edit2,
    'friends-icon': Friends,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  padding: 30px 80px;

  .cards-container {
    grid-auto-rows: min-content;
    grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
    gap: 40px 50px;

    .card {
      padding: 12px 6px 12px 12px;
      gap: 6px;
      background: linear-gradient(160deg, #cfcfcf, #eaeaea);
      box-shadow: 5px 5px 10px #707070, -3px -3px 10px #fff;
      border-radius: 10px;
      height: fit-content;
      position: relative;

      transition: opacity 200ms, transform 200ms;
      &::before,
      &::after {
        left: 0;
        right: 100%;
        transition: left 350ms, right 350ms;
      }

      &.true {
        opacity: 0.7;
        &::before,
        &::after {
          left: -18px;
          right: -18px;
          height: 1px;
          background-color: #202020c9;
        }
        &::before {
          top: 30%;
        }
        &::after {
          top: 70%;
        }
      }

      &.being_removed {
        transform: scale(0.3);
        opacity: 0;
      }

      .category_mark {
        position: absolute;
        top: 0;
        padding: 0 7px 2px 7px;
        border-top-left-radius: 10px;
        border-top-right-radius: 10px;
        right: 20px;
        transform: translateY(-100%);
        background-color: #e9e9e9;
        box-shadow: inset 0 -2px 3px 0 #909090, 0 0 4px #a0a0a0;
        cursor: pointer;

        span {
          font-size: 10px;
          font-weight: 300;
          text-transform: uppercase;
        }
        transition: box-shadow 200ms;

        &:hover {
          box-shadow: inset 0 -2px 3px 0 #909090, 0 0 4px transparent;
        }
      }
      .left {
        flex-grow: 1;
        gap: 8px;

        .title_container {
          .title {
            text-transform: uppercase;
            font-weight: 500;
            border-radius: 3px;
            padding: 3px 5px;
            cursor: default;
            width: 100%;
            transition: margin-left 100ms;
            text-overflow: ellipsis;
          }
          &.focused .title {
            margin-left: 10px;
          }
          &::before {
            left: 0;
            top: 0;
            bottom: 0;
            width: 8px;
            border-radius: 10px;
            box-shadow: inset 2px 2px 6px transparent,
              inset -2px -2px 6px transparent;
            transition: box-shadow 200ms;
          }
          &.focused::before {
            box-shadow: inset 2px 2px 6px #707070, inset -2px -2px 6px #fff;
          }
        }
        .description {
          box-shadow: inset 2px 2px 6px #707070, inset -2px -2px 6px #fff;
          border-radius: 8px;
          padding: 8px;
          font-weight: 300;
          flex-grow: 1;
          overflow-y: auto;
          resize: none;
          cursor: default;
        }
      }
      .right {
        &.flex_r {
          width: fit-content;
          padding-right: 12px;
        }
        width: 30px;
        align-items: center;
        justify-content: space-between;

        button {
          .icon {
            width: 16px;
            height: 16px;
          }
        }
        .menu_container {
          position: relative;

          .menu_options {
            li {
              .icon {
                &.trash::v-deep {
                  path {
                    stroke: rgb(143, 5, 5);
                  }
                }
              }
            }
          }
        }
        .options_container {
          align-items: center;
          gap: 6px;

          .important {
            svg::v-deep path {
              fill: transparent;
              stroke: rgb(185, 155, 17);
              stroke-width: 30px;
              transition: fill 300ms;
            }
            .true::v-deep path {
              fill: rgb(185, 155, 17);
            }
          }
          .finish {
            svg {
              border-radius: 100%;
              border: 1px solid #202020;
              transition: border 300ms;
              &::v-deep polyline {
                stroke: transparent;
                transition: stroke 300ms;
              }

              &.true {
                border: 1px solid transparent;
                transform: scale(1.3);

                &::v-deep polyline {
                  stroke: #202020;
                }
              }
            }
          }
        }
      }
    }
  }
  .error-container {
    align-items: center;
    gap: 20px;
    height: 100%;

    img {
      opacity: 0.6;
      width: 180px;
      max-width: 80%;
    }
  }
}
</style>
