<template>
  <div class="content">
    <div class="content-title">
      Danh sách nhân viên
      <IconButton
        imageUrl="https://firebasestorage.googleapis.com/v0/b/misa-7824e.appspot.com/o/add.png?alt=media&token=584b4300-687e-44ab-bcc2-6eebcfcb5f22"
        text="Thêm nhân viên"
        @btn-click="showModalAdd('add')"
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
      <BaseTable
        :headerList="headersList"
        :fieldList="employeeList"
        @tr-click="showModalMod"
      />
    </div>
    <div class="content-bottom"></div>

    <modal-form-employee ref="modalEmployee" @complete-task="getEmployeeData" />
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
    /**
     * ? mở/đóng Modal form
     */
    showModalAdd(mode) {
      this.modalMode = mode;
      this.$refs.modalEmployee.onShow(this.modalMode);
    },
    showModalMod(obj) {
      this.modalMode = "modify";
      this.$refs.modalEmployee.onShow(this.modalMode, obj.EmployeeId);
    },
  },
};
</script>