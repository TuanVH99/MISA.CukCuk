<template>
  <button class="button" :type="type" @click="$emit('btn-click')">
    {{ text }}
  </button>
</template>

<script>
/**
 * TODO: Truyền vào text
 * ! chưa xử lý action
 */
export default {
  name: "Button",
  props: ["text", "type"],
};
</script>

<style>
@import "../../css/Components/Button.css";
</style>