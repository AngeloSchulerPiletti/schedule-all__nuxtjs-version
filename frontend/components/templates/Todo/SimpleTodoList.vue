<template>
  <div class="wrapper grid">
    <div
      :class="`card pseudos_base flex_r ${simpletodo.finished}`"
      v-for="(simpletodo, index) in simpletodos"
      :key="index"
    >
      <div class="category_mark" v-if="simpletodo.categoryId > 0">
        <span>{{
          $store.state.dashboardSimpleTodos.categories[simpletodo.categoryId]
        }}</span>
      </div>
      <div class="left flex_c">
        <div class="title_container pseudo_base">
          <input
            class="title"
            type="text"
            autocomplete="off"
            v-model="simpletodo.title"
            @focusin="fieldIn($event, simpletodo.title)"
            @focusout="fieldOut($event, simpletodo.title, simpletodo)"
          />
        </div>
        <textarea
          data-enable-grammarly="false"
          class="description"
          type="text"
          autocomplete="off"
          v-model="simpletodo.description"
          @focusin="fieldIn($event, simpletodo.description)"
          @focusout="fieldOut($event, simpletodo.description, simpletodo)"
        ></textarea>
      </div>
      <div class="right flex_c">
        <button class="menu"><three-dots-menu class="icon" /></button>
        <div class="right-bottom flex_c">
          <button
            class="important"
            @click="changeSimpletodoImportance(simpletodo.id)"
          >
            <star-icon :class="`icon ${true}`" />
          </button>
          <button class="finish" @click="changeSimpletodoState(simpletodo.id)">
            <check-icon :class="`icon ${simpletodo.finished}`" />
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Star from '@/components/icons/Star'
import ThreeDotsMenu from '@/components/icons/ThreeDotsMenu'
import Check from '@/components/icons/Check'

export default {
  data() {
    return {
      clicked: false,
      fieldCache: '',
    }
  },
  methods: {
    fieldIn(event, oldValue) {
      event.path[1].classList.add('focused')
      this.fieldCache = oldValue
    },
    fieldOut(event, newValue, simpletodo) {
      event.path[1].classList.remove('focused')
      if (newValue != this.fieldCache) this.callToSave(simpletodo)
      this.fieldCache = ''
    },
    callToSave(simpletodo) {
      console.log('saving')
      //manda um axios
    },
    changeSimpletodoState(simpletodoId) {
      console.log(simpletodoId)
      this.$axios
        .patch('v1/SimpleTodo/change-simpletodo-state', simpletodoId, {
          headers: { 'Content-Type': 'application/json' },
        })
        .then((res) => console.log(res))
    },
    changeSimpletodoImportance(simpletodoId) {
      console.log(simpletodoId)
      //Manda um axios pro backend
    },
  },
  props: {
    simpletodos: Array,
  },
  components: {
    'star-icon': Star,
    ThreeDotsMenu,
    'check-icon': Check,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  grid-template-columns: 1fr 1fr;
  gap: 40px 50px;
  padding: 50px 80px;
  block-size: 100%;

  .card {
    padding: 12px 6px 12px 12px;
    gap: 6px;
    background: linear-gradient(160deg, #cfcfcf, #eaeaea);
    box-shadow: 5px 5px 10px #707070, -3px -3px 10px #fff;
    border-radius: 10px;
    height: 100%;
    position: relative;

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

    .category_mark {
      position: absolute;
      top: 0;
      padding: 2px 7px;
      border-top-left-radius: 10px;
      border-top-right-radius: 10px;
      right: 20px;
      transform: translateY(-100%);
      background-color: #e9e9e9;
      box-shadow: inset 0 -2px 3px 0 #909090, 0 0 4px #a0a0a0;
      cursor: pointer;

      span {
        font-size: 12px;
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

        &::-webkit-scrollbar {
          width: 4px;
        }
        &::-webkit-scrollbar-thumb {
          background-color: #a0a0a0;
          border-radius: 10px;
        }
        &::-webkit-scrollbar-track {
          background-color: transparent;
        }
      }
    }
    .right {
      width: 30px;
      align-items: center;
      justify-content: space-between;

      button {
        .icon {
          width: 16px;
          height: 16px;
        }
      }
      .menu {
        justify-self: flex-start;
      }
      .right-bottom {
        align-items: center;
        gap: 6px;

        .important {
          svg::v-deep path {
            fill: transparent;
            stroke: rgb(185, 155, 17);
            stroke-width: 30px;
          }
          .true::v-deep path {
            fill: rgb(185, 155, 17);
          }
        }
        .finish {
          svg {
            border-radius: 100%;
            border: 1px solid #202020;
            &::v-deep polyline {
              stroke: transparent;
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
</style>
