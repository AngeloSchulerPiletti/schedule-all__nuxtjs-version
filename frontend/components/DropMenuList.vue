<template>
  <div class="dropmenu-container" @mouseleave="hideMenu">
    <button class="menu up" @click="showMenu">
      <three-dots-menu />
    </button>
    <menu
      :class="`menu_options menu-1 flex_c ${defaultPosition}`"
      v-if="show"
      :id="`${menu.info.idPrefix}-${uniqueKey}`"
    >
      <li
        v-for="(action, index) in menu.data"
        :key="index"
        class="flex_r"
        @click="action.callback(uniqueKey)"
      >
        <component v-if="action.icon" :is="action.icon" class="icon" /><span
          v-if="action.text"
          >{{ action.text }}</span
        >
      </li>
    </menu>
  </div>
</template>

<script>
import ThreeDotsMenu from '@/components/icons/ThreeDotsMenu.vue'
export default {
  data() {
    return {
      show: false,
    }
  },
  methods: {
    showMenu() {
      this.show = true
      setTimeout(() => {
        let menu = this.$el.querySelector('.menu_options')
        this.checkMenuPositionOnScreen(menu);
        if (!menu.classList.contains('normal')) menu.classList.add('normal')
      }, 0)
    },
    hideMenu() {
      let menu = this.$el.querySelector('.menu_options')
      if (menu && menu.classList.contains('normal')) menu.classList.remove('normal')
      setTimeout(() => {
        this.show = false
      }, 200)
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
      }, 1)
    },
  },
  props: {
    menu: Object,
    // exemplo-menu: {
    //     info: {
    //         idPrefix: "id-prefix",
    //         // ...
    //     },
    //     data:[
    //        {
    //            text: "Texto do bagui",
    //            icon: "element",
    //            callback: ()=>{},
    //            submenu: {
    //              text: "",
    //              icon: "element",
    //              callback: ()=>{},
    //            },
    //        },
    //     ]
    // }
    uniqueKey: String,
    defaultPosition: String,
  },
  components: {
    ThreeDotsMenu,
  },
}
</script>

<style lang="scss" scoped>
.dropmenu-container {
  position: relative;
  .menu {
    width: 16px;
    height: 16px;
  }
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
</style>
