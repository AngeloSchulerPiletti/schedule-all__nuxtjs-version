<template>
  <div class="wrapper border-soft flex_c" :data-state="menuState">
    <div class="close-open up flex_r">
      <span @click="changeMenuState"><go-back-arrows /></span>
    </div>
    <hr class="division_3d"/>
    <ul class="page-list up flex_c">
      <li>
        <NuxtLink to="/schedule" class="flex_r">
          <span class="menu-icon spare-button pseudo"><grid /></span>
          <span class="link-legend link-2">Workspace</span>
        </NuxtLink>
      </li>
      <li>
        <NuxtLink to="/schedule/todo" class="flex_r">
          <span class="menu-icon spare-button pseudo"><todo /></span>
          <span class="link-legend link-2">Tarefas</span>
        </NuxtLink>
      </li>
      <li>
        <NuxtLink to="/schedule/projects" class="flex_r">
          <span class="menu-icon spare-button pseudo"><case /></span>
          <span class="link-legend link-2">Projetos</span>
        </NuxtLink>
      </li>
    </ul>
    <hr class="division_3d"/>
    <div class="logout_container up">
      <logout :turnOff="menuState"/>
    </div>
  </div>
</template>

<script>
import Todo from '@/components/icons/Todo';
import Arrows from '@/components/icons/Arrows';
import Case from '@/components/icons/Case';
import Grid from '@/components/icons/Grid';
import Logout from "@/components/Logout";

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
    Logout,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  max-height: 100vh;
  gap: 25px;
  align-items: center;
  padding: 20px;
  border-top-right-radius: 15px;
  background: linear-gradient(-105deg, rgba(0, 0, 0, 0.1), rgba(0, 0, 0, 0.1));

  &::before {
    border-top-right-radius: 15px;
    box-shadow: 6px 6px 14px #a9a8b7, -6px -6px 14px #ffffff,
      inset -4px -4px 14px #ffffff, inset 4px 4px 14px #a9a8b7;
  }

  .up{
    position: relative;
    z-index: 5;
  }

  .close-open {
    justify-content: flex-end;
    width: 100%;

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
    gap: 15px;
    width: 100%;

    li {
      a {
        position: relative;
        width: fit-content;

        .menu-icon {
          border-radius: 100%;
          width: 30px;
          height: 30px;

          &::before {
            border-radius: 100%;
          }

          svg {
            transition: transform 300ms;
          }
        }
        .link-legend {
          position: absolute;
          right: -20px;
          top: 50%;
          transform: translate(100%, -50%);
        }

        &:hover, &.nuxt-link-exact-active{
          .menu-icon {
          box-shadow: 2px 2px 8px #00000060, -2px -2px 8px #ffffff60;
          &::before {
            box-shadow: inset 2px 2px 8px #00000060,
              inset -2px -2px 8px #ffffff60;
          }
          svg {
            transform: translate(2px, 2px);
          }
        }
        .link-legend{
          box-shadow: inset 2px 2px 5px 1px rgba(0, 0, 0, 0.2),
              inset -2px -2px 5px 1px rgba(255, 255, 255, 0.5),
              2px 2px 5px 1px rgba(102, 76, 76, 0),
              -2px -2px 5px 1px rgba(255, 255, 255, 0);
          
        }
        }
      }
    }
  }

  .logout_container{
    margin: auto auto 0 0;
  }

  @keyframes open_anim {
    0% {
      width: 76px;
    }
    100% {
      width: 230px;
    }
  }
  @keyframes close_anim {
    0% {
      width: 230px;
    }
    100% {
      width: 76px;
    }
  }

  &[data-state='open'] {
    animation: open_anim 300ms ease 0ms 1 normal forwards;

    @keyframes show-legends {
      0% {
        opacity: 0;
      }
      100% {
        opacity: 1;
      }
    }

    .page-list {
      li {
        a {
          .link-legend {
            display: block;
            animation: show-legends 200ms linear 230ms 1 normal both;
          }
        }
      }
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

    .link-legend {
      display: none;
    }
  }
}
</style>
