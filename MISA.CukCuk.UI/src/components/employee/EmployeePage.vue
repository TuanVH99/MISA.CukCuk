<template>
  <div class="content">
    <div class="content-title">
      Danh sách nhân viên

      <div style="display: flex">
        <IconButton
          imageUrl="https://firebasestorage.googleapis.com/v0/b/misa-7824e.appspot.com/o/image%2Ftrash_remove_icon_145922.svg?alt=media&token=63796a56-9701-4bf8-b926-167cfe0ed2f1"
          text="Xóa nhân viên"
          :option="'delete'"
          :disabled="selected.length ? false : true"
          @btn-click="removeEmployee"
        />
        <IconButton
          imageUrl="https://firebasestorage.googleapis.com/v0/b/misa-7824e.appspot.com/o/add.png?alt=media&token=584b4300-687e-44ab-bcc2-6eebcfcb5f22"
          text="Thêm nhân viên"
          @btn-click="showModalAdd('add')"
        />
      </div>
    </div>

    <div class="content-filter">
      <div class="left-part">
        <InputIcon placeholder="Tìm kiếm theo tên nhân viên" />
        <Select
          :options="departmentList"
          placeholder="Tất cả phòng ban"
          v-model="filterDepartment"
        />
        <Select
          :options="positionList"
          placeholder="Tất cả vị trí"
          v-model="filterPosition"
        />
      </div>
      <div class="refresh">
        <button class="refresh-button" @click="getEmployeeData"></button>
      </div>
    </div>
    <div class="content-data">
      <BaseTable
        :headerList="headersList"
        :fieldList="employeeList"
        @tr-click="showModalMod"
        @select-one="selectEmployee"
        @remove-one="deselectEmployee"
      />
    </div>
    <div class="content-bottom"></div>

    <modal-form-employee ref="modalEmployee" @complete-task="onCompleteModal" />
  </div>
</template>
<script>
import InputIcon from "../base/BaseInputIcon.vue";
import {
  employeeInstance,
  departmentInstance,
  positionInstance,
} from "../../common";
import Select from "../base/BaseDropdown.vue";
import IconButton from "../base/BaseIconButton.vue";
import BaseTable from "../base/BaseTable.vue";
import ModalFormEmployee from "./ModalEmployee.vue";
let headers = [
  { text: "Id nhân viên", propName: "EmployeeId", width: "0%", type: "hidden" },
  // { text: "#", width: "2%" },
  { text: "Mã nhân viên", propName: "EmployeeCode", width: "6%" },
  { text: "Họ và tên", propName: "FullName", width: "10%" },
  { text: "Giới tính", propName: "GenderName", width: "7%" },
  { text: "Ngày Sinh", propName: "DateOfBirth", width: "6%", type: "date" },
  { text: "Điện thoại", propName: "PhoneNumber", width: "6%" },
  { text: "Email", propName: "Email", width: "15%" },
  { text: "Chức vụ", propName: "PositionName", width: "6%" },
  { text: "Phòng ban", propName: "DepartmentName", width: "8%" },
  {
    text: "Mức lương cơ bản",
    propName: "Salary",
    width: "10%",
    type: "currency",
  },
  { text: "Tình trạng công việc", propName: "WorkStatus", width: "10%" },
];

export default {
  name: "EmployeePage",
  components: {
    InputIcon,
    Select,
    IconButton,
    BaseTable,
    ModalFormEmployee,
  },
  computed: {},
  created() {
    this.getEmployeeData();
    this.getDepartmentData();
    this.getPositionData();
  },
  mounted() {},
  data() {
    return {
      departmentList: [],
      positionList: [],
      headersList: headers,
      employeeList: null,
      filterDepartment: null,
      filterPosition: null,
      modalMode: null,
      selected: [],
    };
  },
  methods: {
    /**
     * ? lấy dữ liệu nhan vien
     */
    getEmployeeData() {
      this.modalMode = null;
      this.selected = [];
      this.employeeList = [];
      employeeInstance
        .get("/")
        .then((res) => {
          console.log(res);
          this.employeeList = res.data;
        })
        .catch((res) => {
          console.log(res);
          alert("Có lỗi xảy ra!");
        });
    },
    /**
     * ? lấy dữ liệu phòng ban
     */
    getDepartmentData: function () {
      departmentInstance
        .get("/")
        .then((res) => {
          var tmp = res.data;
          //   console.log(res.data);
          tmp.forEach((item) => {
            this.departmentList.push({
              value: item["DepartmentId"],
              text: item["DepartmentName"],
            });
          });
        })
        .catch((err) => {
          console.log(err);
          alert("Có lỗi xảy ra");
        });
    },
    /**
     * ? lấy dữ liệu vị trí
     */
    getPositionData: function () {
      positionInstance
        .get("/")
        .then((res) => {
          var tmp = res.data;
          //   console.log(res.data);
          tmp.forEach((item) => {
            this.positionList.push({
              value: item["PositionId"],
              text: item["PositionName"],
            });
          });
        })
        .catch((err) => {
          alert("Có lỗi xảy ra");
          console.log(err);
        });
    },
    async removeEmployee() {
      let id = this.selected[0];
      try {
        const response = await employeeInstance.delete("/" + id);
        console.log(response);
      } catch (err) {
        console.log(err);
        alert("Có lỗi xảy ra khi xóa nhân viên!");
      }
    },

    /**
     * ? mở Modal form
     */
    showModalAdd(mode) {
      this.modalMode = mode;
      this.$refs.modalEmployee.onShow(this.modalMode);
    },
    showModalMod(obj) {
      this.modalMode = "modify";
      this.$refs.modalEmployee.onShow(this.modalMode, obj.EmployeeId);
    },
    /**
     * ? Chọn từng id
     */
    selectEmployee(obj) {
      // console.log("add " + obj.EmployeeId);
      this.selected.push(obj.EmployeeId);
    },
    deselectEmployee(obj) {
      // console.log("remove " + obj.EmployeeId);
      this.selected = this.selected.filter((item) => item != obj.EmployeeId);
    },
    onCompleteModal() {
      this.getEmployeeData();
    },
  },
};
</script>