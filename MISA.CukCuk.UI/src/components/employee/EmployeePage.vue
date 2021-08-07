<template>
  <div class="content">
    <div class="content-title">
      Danh sách nhân viên
      <IconButton
        imageUrl="https://firebasestorage.googleapis.com/v0/b/misa-7824e.appspot.com/o/add.png?alt=media&token=584b4300-687e-44ab-bcc2-6eebcfcb5f22"
        text="Thêm nhân viên"
        v-on:btn-click="showModal"
      />
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
      <BaseTable :headerList="headersList" :fieldList="employeeList" />
    </div>
    <div class="content-bottom"></div>
    <!-- <ModalForm :show="addEmployee && modalloaded" @close-modal="onCloseModal" /> -->
    <modal-form-employee
      :show="addEmployee"
      :newId="newEmployeeId"
      @close-modal="onCloseModal"
      @submit-modal="onAddNewEmployee"
    />
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
// import ModalForm from "../base/BaseModalForm.vue";
import ModalFormEmployee from "./ModalEmployee.vue";
let headers = [
  { text: "Id nhân viên", propName: "EmployeeId", width: "0%", type: "hidden" },
  { text: "#", width: "2%" },
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
    // ModalForm,
    ModalFormEmployee,
  },
  computed: {
    modalloaded: function () {
      if (this.formConfig) {
        return true;
      }
      return false;
    },
  },
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
      addEmployee: false,
      modEmployee: false,
      filterDepartment: null,
      filterPosition: null,
      formConfig: null,
      newEmployeeId: null,
    };
  },
  methods: {
    /**
     * ? lấy dữ liệu nhan vien
     */
    getEmployeeData: function () {
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
    getNewEmployeeId: function () {
      employeeInstance
        .get("/NewEmployeeCode")
        .then((res) => {
          console.log(res);
          this.newEmployeeId = res.data;
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
    /**
     * ? mở/đóng Modal form
     */
    showModal: function () {
      this.getNewEmployeeId();
      this.addEmployee = true;
      this.modEmployee = false;
    },
    onCloseModal: function () {
      this.newEmployeeId = null;
      this.addEmployee = false;
      this.modEmployee = false;
    },
    onAddNewEmployee: function (data) {
      console.log(data)
      alert("submit comming!");
    },
  },
};
</script>