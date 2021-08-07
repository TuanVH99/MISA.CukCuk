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
                  <label>Phòng ban</label>
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
    this.getDepartmentData();
    this.getPositionData();
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
        { text: "Nữ", value: "2" },
        { text: "Không xác định", value: "3" },
      ],
      workstatus: [
        { text: "Đang làm việc", value: "1" },
        { text: "Chưa làm", value: "2" },
        { text: "Không xác định", value: "3" },
      ],
    };
  },
  methods: {
    getDepartmentData() {
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

    getPositionData() {
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
     * ? Lấy dữ liệu nhân viên theo id
     */
    async getNewEmployeeId() {
      try {
        //debugger; // eslint-disable-line no-debugger
        let tmp = this.employeeData;
        for (const key in tmp) {
          tmp[key] = null;
        }
        const response = await employeeInstance.get("/NewEmployeeCode");
        console.log(response);
        this.employeeData.EmployeeCode = response.data;
      } catch (e) {
        console.log(e);
        alert("Có lỗi xảy ra!");
      }
    },
    /**
     * ? Hiện modal form theo mode, mode được định nghĩa cả ở parent
     * params {String}
     */
    onShow(mode) {
      this.show = true;
      if (mode == "add") {
        this.mode = mode;
        // debugger; // eslint-disable-line no-debugger
        this.getNewEmployeeId();
        this.$refs.EmployeeCode.onFocus();
      }
      if (mode == "modify") {
        this.mode = mode;
        alert("under construction");
        // this.getEmployeeWithId();
      }
    },
    onClose() {
      this.show = false;
    },
  },
};
</script>

<style>
@import "../../css/Components/ModalForm.css";
</style>