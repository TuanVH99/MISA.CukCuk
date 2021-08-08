<template>
  <div class="modal-form-bg" :class="{ hide: !show }">
    <div class="modal-form-content">
      <span class="close" @click="onClose"><i class="fas fa-times"></i></span>
      <form @submit="onSubmitForm($event)">
        <!-- title form -->
        <div class="form-title">Thông tin nhân viên</div>
        <!-- nội dung chính  -->
        <div class="form-content">
          <!-- avatar / form trái -->
          <div class="form-left">
            <div class="form-avatar"></div>
            <div class="avatar-text">
              (Vui lòng chọn ảnh có định dạng .jpg .jpeg .png .gif)
            </div>
          </div>
          <!-- form bên phải, chia part -->
          <div class="form-right">
            <div class="form-part">
              <div class="form-part-title">A.Thông tin chung</div>
              <div class="title-line"></div>
              <div class="form-line">
                <label-input
                  label="Mã nhân viên"
                  type="text"
                  required
                  ref="EmployeeCode"
                  v-model="employeeData.EmployeeCode"
                />
                <label-input
                  label="Họ và tên"
                  type="text"
                  required
                  ref="FullName"
                  v-model="employeeData.FullName"
                />
              </div>
              <div class="form-line">
                <label-input
                  label="Ngày sinh"
                  type="date"
                  v-model="employeeData.DateOfBirth"
                />

                <div class="input-label">
                  <label>Giới tính</label>
                  <br />
                  <custom-select
                    label="Giới tính"
                    :options="genderList"
                    v-model="employeeData.Gender"
                  />
                </div>
              </div>
              <div class="form-line">
                <label-input
                  label="Số CMND/Căn cước"
                  type="text"
                  required
                  ref="IdentityNumber"
                  v-model="employeeData.IdentityNumber"
                />
                <label-input
                  label="Ngày cấp"
                  type="date"
                  v-model="employeeData.IdentityDate"
                />
              </div>
              <div class="form-line">
                <label-input
                  label="Nơi cấp"
                  type="text"
                  v-model="employeeData.IdentityPlace"
                />
              </div>
              <div class="form-line">
                <label-input
                  label="Email"
                  type="email"
                  required
                  ref="Email"
                  v-model="employeeData.Email"
                />
                <label-input
                  label="Số diện thoại"
                  type="text"
                  required
                  ref="PhoneNumber"
                  v-model="employeeData.PhoneNumber"
                />
              </div>
            </div>
            <div class="form-part">
              <!-- title của part> -->
              <div class="form-part-title">B.Thông tin công việc</div>
              <div class="title-line"></div>
              <div class="form-line">
                <div class="input-label">
                  <label>Vị trí</label>
                  <br />
                  <custom-select
                    label="Vị trí"
                    :options="positionList"
                    v-model="employeeData.PositionId"
                  />
                </div>
                <div class="input-label">
                  <label>Phòng ban</label>
                  <br />
                  <custom-select
                    label="Phòng ban"
                    :options="departmentList"
                    v-model="employeeData.DepartmentId"
                  />
                </div>
              </div>
              <div class="form-line">
                <label-input
                  label="Mã số thuế cá nhân"
                  type="text"
                  v-model="employeeData.PersonalTaxCode"
                />
                <label-input
                  label="Mức lương cơ bản"
                  type="currency"
                  v-model="employeeData.Salary"
                />
              </div>
              <div class="form-line">
                <label-input
                  label="Ngày gia nhập công ty"
                  type="date"
                  v-model="employeeData.JoinDate"
                />
                <div class="input-label">
                  <label>Tình trạng công việc</label>
                  <br />
                  <custom-select
                    label="Tình trạng công viêc"
                    :options="workstatus"
                    v-model="employeeData.WorkStatus"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- các nút -->
        <div class="form-bottom">
          <button type="submit" class="button">
            <i style="margin-right: 8px" class="far fa-save"></i>Lưu
          </button>
          <button type="button" class="cancel-button btn-close-box close">
            Hủy
          </button>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import LabelInput from "../base/BaseInputLabel.vue";
import CustomSelect from "../base/BaseDropdown.vue";
import {
  employeeInstance,
  departmentInstance,
  positionInstance,
  formatCurrency,
  fillObj,
} from "../../common";
export default {
  name: "ModalFormEmployee",
  components: {
    LabelInput,
    CustomSelect,
  },
  props: {
    newId: {
      require: false,
    },
  },
  created() {
    // this.getDepartmentData();
    // this.getPositionData();
  },
  watch: {},
  mounted() {},
  computed: {},
  data() {
    return {
      show: false,
      mode: null,
      employeeData: {
        EmployeeId: null,
        EmployeeCode: null,
        FullName: null,
        Gender: null,
        DateOfBirth: null,
        PhoneNumber: null,
        Email: null,
        IdentityNumber: null,
        IdentityDate: null,
        IdentityPlace: null,
        JoinDate: null,
        DepartmentId: null,
        PositionId: null,
        WorkStatus: null,
        PersonalTaxCode: null,
        Salary: null,
      },
      departmentList: [],
      positionList: [],
      genderList: [
        { text: "Nam", value: "1" },
        { text: "Nữ", value: "0" },
        { text: "Không xác định", value: "2" },
      ],
      workstatus: [
        { text: "Đang làm việc", value: "1" },
        { text: "Chưa làm", value: "2" },
        { text: "Không xác định", value: "3" },
      ],
    };
  },
  methods: {
    /**
     * ? Lấy dữ liệu phòng ban
     */
    getDepartmentData() {
      this.departmentList = [];
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
     * ? Lấy dữ liệu vị trí
     */
    getPositionData() {
      this.positionList = [];
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
          alert("Có lỗi xảy ra khi lấy dữ liệu position!");
          console.log(err);
        });
    },
    /**
     * ? Lấy dữ liệu nhân viên theo id
     */
    async getNewEmployeeIdCode() {
      try {
        //debugger; // eslint-disable-line no-debugger
        const response = await employeeInstance.get("/NewEmployeeCode");
        console.log(response);
        this.employeeData.EmployeeCode = response.data;
      } catch (e) {
        console.log(e);
        alert("Có lỗi xảy ra khi lấy id!");
      }
    },
    /**
     * ? Tạo nhân viên mới
     */
    async createNewEmployee() {
      try {
        //debugger; // eslint-disable-line no-debugger
        let tmp = { ...this.employeeData };
        for (const key in tmp) {
          if (!tmp[key] || !tmp[key].length) {
            tmp[key] = "";
          }
        }
        tmp.Salary = formatCurrency(tmp.Salary, 0);
        delete tmp.EmployeeId;
        const response = await employeeInstance.post("/", tmp);
        console.log(response);
      } catch (e) {
        console.log(e);
        alert("Có lỗi xảy ra khi tạo nhân viên!");
      }
    },
    /**
     * ? Lấy dữ liệu nhân viên theo id
     */
    async getDetailEmployee(id) {
      try {
        const response = await employeeInstance.get(`/${id}`);
        console.log(response);
        this.employeeData = response.data;
      } catch (err) {
        console.log(err);
        alert("Có lỗi xảy ra khi lấy dữ liệu nhân viên!");
      }
    },
    /**
     * ? Thay đổi dữ liệu nhân viên
     */
    async changeEmployeeData() {
      try {
        const id = this.employeeData.EmployeeId;
        let tmp = { ...this.employeeData };
        tmp.Salary = formatCurrency(tmp.Salary, 0);
        const response = await employeeInstance.put(`/${id}`, tmp);
        console.log(response);
      } catch (err) {
        console.log(err);
        alert("Có lỗi xảy ra khi thay đổi dữ liệu nhân viên!");
      }
    },
    /**
     * ? Hiện modal form theo mode, mode được định nghĩa cả ở parent
     */
    onShow(mode, id) {
      this.show = true;
      this.getDepartmentData();
      this.getPositionData();
      if (mode == "add") {
        this.mode = mode;
        // debugger; // eslint-disable-line no-debugger
        this.getNewEmployeeIdCode();
        this.$refs.EmployeeCode.onFocus();
      }
      if (mode == "modify" && id) {
        this.mode = mode;
        this.getDetailEmployee(id);
        this.$refs.EmployeeCode.onFocus();
      }
    },

    onClose() {
      this.show = false;
      this.employeeData = fillObj(this.employeeData, null);
      this.$emit("complete-task");
    },

    onSubmitForm(event) {
      event.preventDefault();
      if (this.mode == "add") {
        this.createNewEmployee();
      }
      if (this.mode == "modify") {
        this.changeEmployeeData();
      }
      this.onClose();
    },
  },
};
</script>

<style>
@import "../../css/Components/ModalForm.css";
</style>