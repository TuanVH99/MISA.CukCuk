<template>
  <div class="input-label" :class="{ 'input-money': isCurrency }">
    <label>{{ label }}</label>
    <span v-if="required">(<span style="color: red">*</span>)</span>
    <br />
    <!-- input type normal: text & date -->

    <input
      v-if="isNormalType"
      v-model="model"
      :type="type"
      :name="name"
     
    />
    <!-- input type currency -->

    <input
      v-if="isCurrency"
      v-model="model"
      :type="type"
      :name="name"
      v-mask="'###.###.###.###'"
      ref="money"
    /><span class="vnd" v-if="isCurrency">VND</span>
    <!-- input type select, modfify -->
    <Select
      v-if="isSelectType"
      :placeholder="placeholder"
      :options="options"
      v-model="model"
    />
    <div
      class="input-clear"
      v-if="!isBlank && !isSelectType"
      @click="onClearData"
    >
      <i class="fas fa-times"></i>
    </div>
  </div>
</template>
<script>
/**
 * ? Hiện tại có 4 loại input: text, date, currency và select
 * ? mỗi loại sẽ nhận các props như bthg
 * ? riêng đối với select: cần có props options, value = initialValue
 */
import Select from "./BaseDropdown.vue";
import moment from "moment";
export default {
  name: "InputLabel",
  components: {
    Select,
  },
  props: {
    label: {
      type: String,
      require: false,
    },
    name: {
      type: String,
      require: false,
    },
    type: {
      type: String,
      require: true,
    },
    required: {
      type: Boolean,
      require: false,
      default: false,
    },
    placeholder: {
      type: String,
      require: false,
    },
    value: {
      type: String,
      require: false,
    },
    options: {
      type: Array,
      require: false,
    },
    rule: {
      require: false,
    },
    focus: {
      require: false,
      type: Boolean,
    },
  },
  created() {
    /**
     * xử lý định dạng trước
     */
    switch (this.type) {
      case "currency": {
        this.isNormalType = false;
        this.isSelectType = false;
        this.isCurrency = true;
        break;
      }
      case "select": {
        this.isNormalType = false;
        this.isSelectType = true;
        this.isCurrency = false;
        break;
      }
      default: {
        this.isNormalType = true;
        this.isSelectType = false;
        this.isCurrency = false;
        break;
      }
    }
    
  },
  data() {
    return {
      optionList: [],
      isBlank: this.value == null ? true : false,
      isNormalType: null,
      isSelectType: null,
      isCurrency: null,
    };
  },
  methods: {
    onClearData: function () {
      this.model = null;
      this.isBlank = true;
    },
    onInput: function () {
      console.log(this.model);
      if (!this.model.length) {
        this.isBlank = true;
      } else {
        this.isBlank = false;
      }
    },
  },
  computed: {
    model: {
      get() {
        if (this.type == "date") {
          return moment(this.value).format("YYYY-MM-DD");
        }
        return this.value;
      },
      set(val) {
        if (!val) {
          this.isBlank = true;
        } else {
          this.isBlank = false;
        }
        this.$emit("input", val);
      },
    },
  },
};
</script>
<style>
</style>