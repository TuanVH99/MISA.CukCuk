<template>
  <div class="input-icon">
    <div class="input-icon-icon"></div>
    <input type="text" :placeholder="placeholder" />
    <span class="input-clear" hidden><i class="fas fa-times"></i></span>
    <span class="tooltiptext" hidden>Thông tin không đúng định dạng</span>
  </div>
</template>
<script>
/**
 * TODO: Input với icon
 * ! CHưa có sự kiện cụ thể
 */
export default {
  name: "InputIcon",
  props: ["placeholder"],
};
</script>


<style>
@import "../../css/Components/FieldInputIcon.css";
</style>