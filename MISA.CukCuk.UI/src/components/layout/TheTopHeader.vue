<template>
  <div class="top-header">
    <div class="top-left">
      <div class="top-icon"></div>
      <div class="top-logo"></div>
    </div>
    <div class="top-right">
      <div class="top-name">
        <span>Nhà hàng Hạ Long</span>
        <button class="btn-dropdown">
          <font-awesome-icon icon="chevron-down" />
        </button>
      </div>
      <div class="top-account">
        <div class="avatar L"></div>
        <div class="name">Nguyễn Văn A</div>
        <div class="icon"></div>
      </div>
    </div>
  </div>
</template>
<script>
import { library } from "@fortawesome/fontawesome-svg-core";
import { faChevronDown } from "@fortawesome/free-solid-svg-icons";
library.add(faChevronDown);
export default {
  name: "TopHeader",
};
</script>

<style scoped>
@import "../../css/layout/topheader.css";
</style>