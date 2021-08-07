<template>
  <div class="custom-table">
    <table>
      <thead>
        <tr>
          <th
            v-for="(item, index) in headerList"
            :key="index"
            :style="{
              width: item.width,
            }"
            :type="item.type"
          >
            {{ item.text }}
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in fields" :key="index" @dblclick="showId(item)">
          <td
            v-for="header in headerList"
            :key="header.propName"
            :title="item[header.propName]"
            :style="{
              width: header.width,
            }"
            :type="header.type"
          >
            {{ item[header.propName] ? item[header.propName] : "" }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { formatDate, formatCurrency } from "../../common";
export default {
  name: "BaseTable",
  props: ["headerList", "fieldList"],
  data() {
    return {
      fields: [],
      headerProps: [],
    };
  },
  created() {},
  mounted() {},
  watch: {
    fieldList: function (val) {
      this.getData(val);
    },
  },
  computed: {},
  methods: {
    //? Chuyển dữ liệu ver2 + loại bỏ data dư thừa
    //? tạo fiedl dữ liệu với các header tương ứng
    getData: function (fieldList) {
      this.fields = [];
      fieldList.forEach((field) => {
        let tmp = {};
        this.headerList.forEach((header) => {
          if (header.type == "date") {
            tmp[header.propName] = formatDate(
              field[header.propName],
              "dd/mm/yyyy"
            );
          } else if (header.type == "currency") {
            tmp[header.propName] = formatCurrency(field[header.propName]);
          } else {
            tmp[header.propName] = field[header.propName];
          }
        });
        this.fields.push(tmp);
      });
    },
    showId(obj) {
      this.$emit("tr-click", obj);
    },
  },
};
</script>

<style scoped>
@import "../../css/Components/Table.css";
</style>