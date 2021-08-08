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
    <input v-if="isEmail" v-model="model" :type="type" ref="inputRef" />

    <!-- input clear button  -->
    <div class="input-clear" v-if="!isBlank" @click="onClearData">
      <i class="fas fa-times"></i>
    </div>
    <span class="tooltiptext" v-if="!validation.isOK">{{
      validation.error
    }}</span>
  </div>
</template>
<script>
/**
 * ? Hiện tại có 3 loại input: text, date, currency, email
 * ? mỗi loại sẽ nhận các props như bthg
 */
import moment from "moment";
import { ValidateEmailAddress } from "../../common";
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
      require: false,
    },
    max: {
      require: false,
    },
    min: {
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
        this.isEmail = false;
        break;
      }
      case "date": {
        this.isText = false;
        this.isCurrency = false;
        this.isDate = true;
        this.isEmail = false;
        break;
      }
      case "email": {
        this.isText = false;
        this.isCurrency = false;
        this.isDate = false;
        this.isEmail = true;
        break;
      }
      default: {
        this.isText = true;
        this.isDate = false;
        this.isEmail = false;
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
      isEmail: null,
      validation: {
        isOK: true,
        error: null,
      },
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
    /**
     * ? Validate dữ liệu cho các field, hiện tại gồm rule đơn giản: định dạng email, required và length
     */
    validate() {
      this.validation = {
        isOK: true,
        error: null,
      };
      if (this.required && (!this.model || !this.model.length)) {
        this.validation.isOK = false;
        this.validation.error = "Cần điền đầy đủ thông tin";

        return false;
      }
      if (this.max && this.model.length > this.max) {
        this.validation.isOK = false;
        this.validation.error = "Vượt quá ký tự cho phép";

        return false;
      }
      if (this.min && this.model.length < this.mmin) {
        this.validation.isOK = false;
        this.validation.error = "Không đủ độ dài ký tự";

        return false;
      }
      if (this.type == "email") {
        if (this.model) {
          let check = ValidateEmailAddress(this.model);
          if (!check) {
            this.validation.isOK = false;
            this.validation.error = "Không đúng định dạng Email";

            return false;
          }
        }
      }
      return true;
    },
  },
  computed: {
    /**
     * ? Để có thể sử dụng v-model cho component, và check clear data
     */
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