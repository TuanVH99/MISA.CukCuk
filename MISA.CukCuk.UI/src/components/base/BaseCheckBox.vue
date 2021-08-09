<template>
  <!-- ver2 -->
  <div class="custom-checkbox">
    <div class="checkbox-form">
      <div class="answers">
        <label class="item">
          <input v-model="checked" type="checkbox" @change="onChange" />
          <span class="checkmark" />
        </label>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "CustomCB",
  data() {
    return {
      checked: null,
    };
  },
  methods: {
    onChange() {
      this.$emit("input", this.checked);
    },
    outterChange() {
      this.onChange();
      return this.checked;
    },
  },
};
</script>
<style scoped>
/* ver2 */

.custom-checkbox .checkbox-form .answers {
  width: 100%;
}

.custom-checkbox .checkbox-form .item {
  display: block;
  position: relative;
  padding-left: 35px;
  cursor: pointer;
  height: 20px;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}

.custom-checkbox .checkbox-form .item input {
  position: absolute;
  opacity: 0;
  cursor: pointer;
  height: 0;
  width: 0;
}

.custom-checkbox .checkbox-form .checkmark {
  position: absolute;
  top: 0;
  left: 0;
  height: 18px;
  width: 18px;
  background-color: #fff;
  border: 1px solid #bbb;
  border-radius: 4px;
}

.custom-checkbox .checkbox-form .item:hover input ~ .checkmark {
  background-color: #bbb;
}

.custom-checkbox .checkbox-form .item input:checked ~ .checkmark {
  background-color: #019160;
}

.custom-checkbox .checkbox-form .checkmark:after {
  content: "";
  position: absolute;
  display: none;
}

.checkbox-form .item input:checked ~ .checkmark:after {
  display: block;
}

.checkbox-form .item .checkmark:after {
  left: 5px;
  top: 2px;
  width: 5px;
  height: 10px;
  border: solid white;
  border-width: 0 2px 2px 0;
  -webkit-transform: rotate(45deg);
  -ms-transform: rotate(45deg);
  transform: rotate(45deg);
}
</style>