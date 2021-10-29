<template>
  <div class="wrapper border-soft" :data-state="menuState">
    <div class="close-open flex_r">
      <span @click="changeMenuState"><go-back-arrows /></span>
    </div>
    <ul class="page-list flex_c">
      <li>
        <NuxtLink to="/schedule" class="flex_r">
          <span class="menu-icon spare-button pseudo"><grid/></span>
          <span class="link-legend">Workspace</span>
        </NuxtLink>
      </li>
      <li>
        <NuxtLink to="/schedule/todo" class="flex_r">
          <span class="menu-icon spare-button pseudo"><todo /></span>
          <span class="link-legend">Tarefas</span>
        </NuxtLink>
      </li>
      <li>
        <NuxtLink to="/schedule/projects" class="flex_r">
          <span class="menu-icon spare-button pseudo"><case /></span>
          <span class="link-legend">Tarefas</span>
        </NuxtLink>
      </li>
    </ul>
  </div>
</template>

<script>
import Todo from '@/components/icons/Todo'
import Arrows from '@/components/icons/Arrows'
import Case from '@/components/icons/Case'
import Grid from '@/components/icons/Grid'

export default {
  data() {
    return {
      menuState: 'close',
    }
  },
  methods: {
    changeMenuState() {
      if (this.menuState == 'open') {
        this.menuState = 'close'
        return
      }
      this.menuState = 'open'
    },
  },
  components: {
    Todo,
    'go-back-arrows': Arrows,
    Case,
    Grid,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  max-height: 100vh;
  padding: 20px 10px 20px 0;
  border-top-right-radius: 15px;
  background: linear-gradient(-105deg, rgba(0, 0, 0, 0.1), rgba(0, 0, 0, 0.1));

  &::before {
    border-top-right-radius: 15px;
    box-shadow: 6px 6px 14px #a9a8b7, -6px -6px 14px #ffffff,
      inset -4px -4px 14px #ffffff, inset 4px 4px 14px #a9a8b7;
  }

  .close-open {
    position: relative;
    z-index: 5;
    justify-content: center;

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

  .page-list {
    position: relative;
    z-index: 5;
    align-items: center;
    padding: 30px 0;
    gap: 15px;

    li {
      a {
        align-items: center;

        .menu-icon {
          border-radius: 100%;
          width: 30px;
          height: 30px;
          // padding: 8px;

          &::before {
            border-radius: 100%;
          }

          svg {
            transition: transform 300ms;
          }
          &:hover {
            svg {
              transform: translate(3px, 3px);
            }
          }
        }
      }
    }
  }

  @keyframes open_anim {
    0% {
      width: 76px;
    }
    100% {
      width: 280px;
    }
  }
  @keyframes close_anim {
    0% {
      width: 280px;
    }
    100% {
      width: 76px;
    }
  }

  &[data-state='open'] {
    animation: open_anim 300ms ease 0ms 1 normal forwards;
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

    .page-list {
      li {
        a {
          .link-legend {
            display: none;
          }
        }
      }
    }
  }
}
</style>
