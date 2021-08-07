<template>
  <button class="button" v-on:click="showModal">
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
  props: ["imageUrl", "text"],
  mounted() {},
  methods:{
    showModal:function(){
      this.$emit('btn-click');
    }
  }
};
</script>
<style>
@import "../../css/Components/Button.css";
</style>

