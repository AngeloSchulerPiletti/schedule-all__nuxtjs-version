<template>
  <div class="wrapper flex_r">
    <div class="categories grid">
      <span class="link-2 activated_link" @click="categorySelected(0, $event)"
        >Todos</span
      >
      <span
        class="link-2"
        @click="categorySelected(id, $event)"
        v-for="(title, id) in $store.state.dashboardSimpleTodos.categories"
        :key="id"
      >
        {{ title }}
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
    }
  },
  methods: {
    categorySelected(id, event) {
      var lastOne = this.$el.querySelector('.activated_link')
      lastOne.classList.remove('activated_link')
      event.target.classList.add('activated_link')

      //Manda request pro backend pra pedir paginação por categoria
    },
    createCategory() {
      if (!this.title) return null
      this.$axios
        .post('v1/Category/create-category', this.title, {
          headers: {
            'Content-type': 'application/json',
          },
        })
        .then((res) => console.log(res))
    },
  },
  components: {
    'add-icon': Add,
  },
}
</script>

<style lang="scss" scoped>
.wrapper {
  .categories {
    flex-grow: 1;
    height: 0%;
    gap: 8px;
    grid-auto-flow: column;
    justify-content: flex-start;

    span {
      white-space: nowrap;
    }
  }

  .category_creation {
    flex-grow: 0;
    width: 300px;
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
