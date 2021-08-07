<template>
  <div class="modal-form-bg" :class="{ hide: !show }">
    <div class="modal-form-content">
      <span class="close" @click="onCloseModal"
        ><i class="fas fa-times"></i
      ></span>
      <form>
        <!-- title form -->
        <div class="form-title">{{ title }}</div>
        <!-- nội dung chính  -->
        <div class="form-content">
          <!-- avatar / form trái -->
          <div class="form-left">
            <div class="form-avatar"></div>
            <div class="avatar-text">
              (Vui lòng chọn ảnh có định dạng .jpg .jpeg .png .gif)
            </div>
          </div>
          <!-- form bên phải, chia part -->
          <div class="form-right">
            <div class="form-part" v-for="(part, index) in config" :key="index">
              <!-- title của part> -->
              <div class="form-part-title">{{ part.title }}</div>
              <div class="title-line"></div>
              <!-- phần chính, chia 2 cột, chia từng hàng -->
            </div>
          </div>
        </div>
        <!-- các nút -->
        <div class="form-bottom">
          <button type="submit" class="button">
            <i style="margin-right: 8px" class="far fa-save"></i>Lưu
          </button>
          <button type="button" class="cancel-button btn-close-box close">
            Hủy
          </button>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
export default {
  name: "ModalForm",
  props: {
    title: {
      type: String,
      require: false,
    },
    config: {
      type: Array,
      require: false,
    },
    show: {
      type: Boolean,
      require: true,
    },
  },
  computed: {
    data: function () {
      return this.config;
    },
  },
  data() {
    return {};
  },
  methods: {
    onCloseModal: function () {
      this.$emit("close-modal");
    },
  },
};
</script>

<style>
@import "../../css/Components/ModalForm.css";
</style>