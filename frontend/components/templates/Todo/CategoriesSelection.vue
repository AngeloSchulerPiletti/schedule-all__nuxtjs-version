<template>
  <div class="wrapper flex_r">
    <div class="categories">
      <span class="link-2 activated_link" @click="categorySelected(0, $event)"
        >Todos</span
      >
      <span class="link-2" @click="categorySelected(null, $event)"
        >Importantes</span
      >
      <span
        class="link-2 deletable"
        @click="categorySelected(id, $event)"
        @mouseover="categoryHovered = id"
        @mouseleave="categoryHovered = 0"
        v-for="(title, id) in $store.state.dashboardSimpleTodos.categories"
        :key="id"
      >
        {{ title }}
        <transition name="slide">
          <button v-show="categoryHovered == id" @click="deleteCategory(id)">
            X
          </button>
        </transition>
      </span>
    </div>
    <div class="category_creation flex_r">
      <input
        class="input_text-1"
        type="text"
        name="title"
        id="title"
        placeholder="Nova Categoria"
        v-model="title"
        autocomplete="off"
      />
      <button class="spare-button pseudo" @click="createCategory">
        <add-icon />
      </button>
    </div>
  </div>
</template>

<script>
import Add from '@/components/icons/Add'

export default {
  data() {
    return {
      title: null,
      categoryHovered: 0,
      modalSubjects: {
        onDelete: 'deleteCategory',
      },
      categoryOnDelete: 0,
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
        this.$axios.delete('v1/Category/delete-category', {
          data: this.categoryOnDelete,
        })
      }
      this.$store.commit('cleanAnswer')
    },
  },
  methods: {
    categorySelected(id, event) {
      var lastOne = this.$el.querySelector('.activated_link')
      lastOne.classList.remove('activated_link')
      event.target.classList.add('activated_link')

      //id == null é pra pegar só os importantes
      //Manda request pro backend pra pedir paginação por categoria
    },
    createCategory() {
      if (!this.title) return null
      this.$axios
        .post('v1/Category/create-category', JSON.stringify(this.title))
        .then((res) => console.log(res))
    },
    deleteCategory(categoryId) {
      this.$store.commit('openModal', this.modalSubjects.onDelete)
      this.categoryOnDelete = categoryId
    },
  },
  components: {
    'add-icon': Add,
  },
}
</script>

<style lang="scss" scoped>
.slide-enter-active,
.slide-leave-active {
  transition: margin-left 200ms, opacity 200ms;
}
.slide-enter,
.slide-leave-to {
  margin-left: 0 !important;
  opacity: 0;
}

.wrapper {
  padding-bottom: 20px;
  .categories {
    width: calc(100% - 300px);

    span {
      white-space: nowrap;
      display: inline-block;
      margin: 5px;
    }
    .deletable {
      padding-right: 26px;
      position: relative;

      button {
        position: absolute;
        right: 10px;
        top: 5px;
        transform: translateY(25%);
        font-weight: 400;
        font-size: 0.8em;
      }
    }
  }

  .category_creation {
    width: 300px;
    height: fit-content;
    gap: 14px;
    align-items: center;
    justify-content: flex-end;
    margin-right: 30px;

    input {
      width: 160px;
    }
    button {
      padding: 8px;
      border-radius: 100%;

      &::before {
        border-radius: 100%;
      }
      svg {
        width: 17px;
        height: 17px;
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
</style>
