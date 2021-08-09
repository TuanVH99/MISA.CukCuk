<template>
  <button
    class="button"
    v-on:click="showModal"
    :disabled="disabled"
    type="button"
    :option="option"
  >
    <div
      class="btn-icon-icon"
      :style="{ 'background-image': 'url(' + imageUrl + ')' }"
    ></div>
    <div class="btn-icon-text">{{ text }}</div>
  </button>
</template>
<script>
/**
 * TODO: Button với icon
 * ! Chưa xử lý event
 */
export default {
  name: "IconButton",
  props: ["imageUrl", "text", "option", "disabled"],
  mounted() {},
  methods: {
    showModal: function () {
      this.$emit("btn-click");
    },
  },
};
</script>
<style>
@import "../../css/Components/Button.css";
</style>

