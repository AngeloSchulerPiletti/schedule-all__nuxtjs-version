<template>
  <div :class="`wrapper border-soft flex_c ${closed}`">
    <button id="new_simpletodo" class="border-soft flex_c" @click="addClicked">
      <add-icon />
    </button>
    <button
      id="close_simpletodo"
      :class="`border-soft flex_c ${closed}`"
      @click="closeClicked"
    >
      <add-icon />
    </button>
    <div class="top pullUp flex_r">
      <input
        autocomplete="off"
        class="input_text-1"
        type="text"
        name="title"
        id="title"
        v-model="simpletodo.title"
        placeholder="Título"
      />
      <div class="select_container flex_r">
        <div class="category_selection flex_r" @click="openCategoryOptions">
          <span>{{ selectedCategory }}</span
          ><button :class="`${closedCategories} flex_r`">
            <arrow-simple />
          </button>
        </div>
        <transition name="optionsUp">
          <ul
            v-show="!closedCategories"
            :class="`category_options grid ${closedCategories}`"
          >
            <li
              v-for="(title, id) in categoriesFromStore"
              :key="id"
              @click="categorySelected(id)"
              class="link-2_tiny"
            >
              {{ title }}
            </li>
          </ul>
        </transition>
      </div>
    </div>
    <textarea
      class="bottom pullUp input_text-1"
      name="description"
      id="description"
      placeholder="Descrição (opcional)"
      v-model="simpletodo.description"
    ></textarea>
  </div>
</template>

<script>
import Add from '@/components/icons/Add'
import ArrowSimple from '@/components/icons/ArrowSimple'

export default {
  data() {
    return {
      closed: true,
      simpletodo: {
        title: null,
        description: null,
        categoryId: 0,
      },
      closedCategories: true,
    }
  },
  computed: {
    selectedCategory() {
      let id = this.simpletodo.categoryId
      return id == 0
        ? 'opcional'
        : this.$store.state.dashboardSimpleTodos.categories[id]
    },
    categoriesFromStore(){
      return this.$store.state.dashboardSimpleTodos.categories;
    }
  },
  methods: {
    addClicked() {
      if (!this.closed && this.simpletodo.title) {
        console.log('criando novo item')
        this.simpletodo = { title: null, description: null, categoryId: 0 }
        this.closedCategories = true
      }
      this.closed = !this.closed
    },
    closeClicked() {
      this.closed = true
      this.closedCategories = true
    },
    openCategoryOptions() {
      this.closedCategories = !this.closedCategories
    },
    categorySelected(categoryId) {
      this.simpletodo.categoryId = categoryId
      this.openCategoryOptions()
    },
  },
  components: {
    'add-icon': Add,
    ArrowSimple,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  border-top-left-radius: 15px;
  border-top-right-radius: 15px;
  background-color: #efefef;
  z-index: 100;
  padding: 12px;
  gap: 14px;
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;

  &::before {
    border-top-left-radius: 15px;
    border-top-right-radius: 15px;
    box-shadow: -3px -3px 6px #dadada, -6px -6px 12px #7f7f7f,
      inset 0 0 10px 10px #dfdfdf;
  }

  transition: transform 300ms;

  &.true {
    transform: translateY(100%);
  }

  #close_simpletodo,
  #new_simpletodo {
    position: absolute;
    top: 0;
    height: 50px;
    padding: 6px;
    background-color: #efefef;
    z-index: -1;
    border-radius: 10px;

    &::before {
      box-shadow: 0 -2px 4px #dadada, 0px -4px 8px #7f7f7f;
      border-radius: 10px;
    }

    svg {
      width: 20px;
      height: 20px;
    }
  }

  #new_simpletodo {
    left: 100px;
    transform: translateY(calc(-100% + 10px));
  }
  #close_simpletodo {
    left: 50px;
    transform: translateY(calc(-100% + 10px));
    opacity: 1;

    svg {
      transform: rotate(45deg);
    }

    transition: transform 200ms, opacity 200ms;

    &.true {
      opacity: 0;
      transform: translateY(0);
    }
  }

  .top {
    gap: 30px;
    align-items: center;

    .select_container {
      position: relative;
      .category_selection {
        align-items: center;
        background: linear-gradient(
          135deg,
          rgba(0, 0, 0, 0.15),
          rgba(255, 255, 255, 0.2)
        );
        border-radius: 100px;

        span {
          font-weight: 300;
          font-size: 15px;
          padding: 7px 7px 7px 14px;
          text-transform: uppercase;
        }
        button {
          padding: 7px 14px 7px 14px;
          border-top-right-radius: 100px;
          border-bottom-right-radius: 100px;
          transition: box-shadow 200ms;
          box-shadow: inset 2px 2px 4px 2px #a7a7a7;

          svg {
            width: 17px;
            height: 17px;
            transform: rotate(90deg) scaleX(-1);
            transition: transform 300ms;
          }
          &.true {
            box-shadow: inset 2px 2px 4px 2px transparent;
            svg {
              transform: rotate(90deg);
            }
          }
        }
      }
      .category_options {
        position: absolute;
        background: linear-gradient(45deg, #ababab, #f0f0f0);
        box-shadow: 0 0 10px #707070;
        border-radius: 17px;
        padding: 10px;
        top: -6px;
        right: 0;
        gap: 10px;
        transform: translateY(-100%);
        grid-template-rows: auto auto auto;
        grid-auto-columns: auto;
        grid-auto-flow: column;
      }
    }

    input {
      max-width: 500px;
      width: 100%;
    }
  }
  .bottom {
    resize: none;
    height: 80px;
  }
}

.optionsUp-enter-active,
.optionsUp-leave-active {
  transition: opacity 0.1s;
}
.optionsUp-enter,
.optionsUp-leave-to {
  opacity: 0;
}
</style>
