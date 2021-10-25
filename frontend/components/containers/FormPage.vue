<template>
  <div>
    <back-arrow />
    <div class="form-page flex_c">
      <h1 class="title-1_1">{{ title }}</h1>
      <form class="form-1" @submit.prevent>
        <div class="inputs">
          <div
            v-for="(input, index) in form_structure.inputs"
            :key="index"
            class="input_container"
          >
            <label v-if="input.label" :for="input.id">{{ input.label }}</label>
            <input
              :type="input.type"
              :name="input.id"
              :id="input.id"
              :placeholder="input.placeholder ? input.placeholder : ''"
              v-model="form_data[input.id]"
            />
          </div>
        </div>
        <div class="actions">
          <slot :form_data="form_data"></slot>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import BackArrow from '@/components/BackArrow'

export default {
  data(){
    return{
      form_data: {},
    }
  },
  props: {
    title: String,
    form_structure: Object,
  },
  components: {
    BackArrow,
  },
}
</script>

<style lang="scss" scoped>
.form-page {
  height: 100vh;
  box-sizing: border-box;
  padding: 0 34vw;
  justify-content: center;

  background: linear-gradient(135deg, #e6e6e6 30%, #ffffff);

  align-items: center;

  & h1 {
    margin-bottom: 20px;
  }
  .form-1 {
    display: flex;
    flex-direction: column;
    gap: 30px;
    width: 100%;

    .inputs {
      display: flex;
      flex-direction: column;
      gap: 20px;

      padding: 20px;
      border-radius: 10px;
      background: linear-gradient(-50deg, #e6e6e6 30%, #ffffff);
      box-shadow: 5px 5px 8px #00000040, -5px -5px 8px #ffffff40;

      .input_container {
        display: flex;
        flex-direction: column;
        gap: 4px;

        label {
          text-transform: capitalize;
          font-size: 14px;
          font-weight: 300;
        }
        input {
          padding: 6px 12px;
          border-radius: 6px;
          font-size: 15px;
          box-shadow: 5px 5px 8px #00000000, -5px -5px 8px #ffffff00,
            inset 5px 5px 8px #00000040, inset -5px -5px 8px #ffffff40;
        }
      }
    }
    .actions {
    }
  }
}
</style>
