<template>
  <div id="app">
    <TopHeader />
    <MenuLeft />
    <Content />
  </div>
</template>

<script>
import TopHeader from "./components/layout/TheTopHeader.vue";
import MenuLeft from "./components/layout/TheMenuLeft.vue";
import Content from "./components/layout/TheContent.vue";

export default {
  name: "App",
  components: {
    TopHeader,
    MenuLeft,
    Content,
  },
  methods: {
    onClickAll: function () {
      console.log(this);
    },
  },
};
</script>

<style>
body {
  margin: 0;
}
</style>
