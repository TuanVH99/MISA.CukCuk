<template>
  <div class="custom-dropdown" tabindex="1" @blur="onBlur">
    <!-- <select tabindex="-1" :name="name" id="123" v-model="currentData.value">
      <option value="null">Chọn một giá trị</option>
      <option
        v-for="option in options"
        :key="option.value"
        :value="option.value"
      >
        {{ option.text }}
      </option>
    </select> -->

    <div class="input-clear" v-if="currentData.value" @click="onClearData">
      <i class="fas fa-times"></i>
    </div>

    <div
      class="dropdown"
      :class="{ 'dropdown-arrow-active': show }"
      @click="onDropdownAction"
    >
      <div>{{ currentData.text ? currentData.text : placeholder }}</div>
      <button type="button" class="btn-dropdown">
        <i class="fas fa-chevron-down"></i>
      </button>
    </div>

    <div class="dropdown-items" :class="{ 'dropdown-hide': !show }">
      <div
        v-for="(item, index) in options"
        :key="index"
        @click="onChooseItem(item.text, item.value)"
        :class="{
          'dropdown-item-selected':
            item.value == currentData.value ? true : false,
        }"
      >
        <i class="fas fa-check" v-if="item.value == currentData.value"></i
        >{{ item.text }}
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "Select",
  props: {
    options: {
      type: Array,
      required: true,
    },
    name: {
      type: String,
      required: false,
    },
    placeholder: {
      type: String,
      required: false,
    },
    tabindex: {
      type: Number,
      required: false,
      default: 0,
    },
    value: {
      required: false,
    },
  },
  data() {
    return {
      currentData: {
        text: null,
        value: null,
      },
      show: false,
    };
  },
  mounted() {},
  watch: {
    value: function () {
      console.log("watch running!");
      this.onSelected(this.value);
    },
  },
  computed: {},
  methods: {
    /**
     * ? Chọn item
     */
    onChooseItem(text, value) {
      this.currentData.value = value;
      this.currentData.text = text;
      this.show = false;
      this.$emit("input", value);
    },
    /**
     * ? Xóa dữ liệu
     */
    onClearData() {
      this.currentData.value = null;
      this.currentData.text = null;
      this.show = false;
      this.$emit("input", null);
    },
    /**
     * ? Hiện dữ liệu hoặc đóng
     */
    onDropdownAction() {
      this.show = !this.show;
    },
    /**
     * ? Xử lý click ngoài component
     */
    onBlur() {
      this.show = false;
    },
    /**
     * ? Xử lý dữ liệu mà có trước
     */
    onSelected(val) {
      if (!val) {
        this.onClearData();
      }
      this.options.forEach((item) => {
        if (item.value == val) {
          this.currentData.text = item.text;
          this.currentData.value = item.value;
        }
      });
    },
  },
};
</script>
<style>
@import "../../css/Components/Dropdown.css";
</style>