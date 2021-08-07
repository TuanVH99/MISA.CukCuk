<template>
  <div class="input-label" :class="{ 'input-money': isCurrency }">
    <label>{{ label }}</label>
    <span v-if="required">(<span style="color: red">*</span>)</span>
    <br />
    <!-- input type normal: text -->
    <input v-if="isText" v-model="model" :type="type" ref="inputRef" />

    <!-- input type normal: date -->
    <input v-if="isDate" v-model="model" :type="type" ref="inputRef" />

    <!-- input type currency -->
    <input
      v-if="isCurrency"
      v-model="model"
      :type="type"
      v-mask="'###.###.###.###'"
      ref="inputRef"
    /><span class="vnd" v-if="isCurrency">VND</span>

    <!-- input type email -->
    <!-- input clear button  -->
    <div class="input-clear" v-if="!isBlank" @click="onClearData">
      <i class="fas fa-times"></i>
    </div>
  </div>
</template>
<script>
/**
 * ? Hiện tại có 3 loại input: text, date, currency, email
 * ? mỗi loại sẽ nhận các props như bthg
 */

import moment from "moment";
export default {
  name: "InputLabel",
  props: {
    label: {
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
  },
  created() {
    /**
     * xử lý định dạng trước
     */
    switch (this.type) {
      case "currency": {
        this.isText = false;
        this.isCurrency = true;
        this.isDate = false;
        break;
      }
      case "date": {
        this.isText = false;
        this.isCurrency = false;
        this.isDate = true;

        break;
      }
      default: {
        this.isText = true;
        this.isDate = false;
        this.isCurrency = false;
        break;
      }
    }
    if (this.value && this.value.length) {
      this.isBlank = false;
    }
  },
  mounted() {},
  data() {
    return {
      optionList: [],
      isBlank: this.value == null ? true : false,
      isText: null,
      isCurrency: null,
      isDate: null,
    };
  },
  methods: {
    onClearData: function () {
      this.model = null;
      this.isBlank = true;
    },
    onFocus() {
      this.$nextTick(() => {
        this.$refs.inputRef.focus();
      });
    },
    onInputCurrency() {
      if (this.type == "currency") this.$nextTick(() => {});
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
        if (!val || !val.length) {
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