<template>
  <div class="modal-form-bg" :class="{ hide: !show }">
    <div class="modal-form-content">
      <span class="close" @click="onCloseModal"
        ><i class="fas fa-times"></i
      ></span>
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
              <!-- title của part> -->
              <div class="form-part-title">A.Thông tin chung</div>
              <div class="title-line"></div>
              <!-- phần chính, chia 2 cột, chia từng hàng -->
              <div class="form-line">
                <LabelInput
                  label="Mã nhân viên"
                  name="EmployeeCode"
                  type="text"
                  v-model="newData.EmployeeCode"
                  focus
                  required
                /><LabelInput
                  label="Họ và tên"
                  name="FullName"
                  type="text"
                  v-model="newData.FullName"
                  required
                />
              </div>
              <div class="form-line">
                <LabelInput
                  label="Ngày sinh"
                  name="DateOfBirth"
                  type="date"
                  v-model="newData.DateOfBirth"
                />
                <LabelInput
                  label="Giới tính"
                  placeholder="Giới tính"
                  name="Gender"
                  type="select"
                  :options="genderList"
                  v-model="newData.Gender"
                />
              </div>
              <div class="form-line">
                <LabelInput
                  label="Số CMND/Căn Cước"
                  name="IdentityNumber"
                  type="text"
                  v-model="newData.IdentityNumber"
                  required
                />
                <LabelInput
                  label="Ngày cấp"
                  name="IdentityDate"
                  type="date"
                  v-model="newData.IdentityDate"
                />
              </div>
              <div class="form-line">
                <LabelInput
                  label="Nơi cấp"
                  name="IdentityPlace"
                  type="text"
                  v-model="newData.IdentityPlace"
                />
              </div>
              <div class="form-line">
                <LabelInput
                  label="Email"
                  name="Email"
                  type="text"
                  required
                  v-model="newData.Email"
                />
                <LabelInput
                  label="Số diện thoại"
                  name="PhoneNumber"
                  type="text"
                  required
                  v-model="newData.PhoneNumber"
                />
              </div>
            </div>
            <div class="form-part">
              <!-- title của part> -->
              <div class="form-part-title">B.Thông tin công việc</div>
              <div class="title-line"></div>
              <!-- phần chính, chia 2 cột, chia từng hàng -->
              <div class="form-line">
                <LabelInput
                  label="Vị trí"
                  placeholder="Tất cả vị trí"
                  name="DepartmentId"
                  type="select"
                  :options="positionList"
                  v-model="newData.DepartmentId"
                />
                <LabelInput
                  label="Phòng ban"
                  placeholder="Tất cả phòng ban"
                  name="DepartmentId"
                  type="select"
                  :options="departmentList"
                  v-model="newData.DepartmentId"
                />
              </div>
              <div class="form-line">
                <LabelInput
                  label="Mã số thuế cá nhân"
                  name="PersonalTaxCode"
                  type="text"
                  v-model="newData.PersonalTaxCode"
                />
                <LabelInput
                  label="Mức lương cơ bản"
                  name="Salary"
                  type="currency"
                  v-model="newData.Salary"
                />
              </div>
              <div class="form-line">
                <LabelInput
                  label="Ngày gia nhập công ty"
                  name="JoinDate"
                  type="date"
                  v-model="newData.JoinDate"
                />
                <LabelInput
                  label="Tình trạng công việc"
                  name="WorkStatus"
                  type="select"
                  v-model="newData.WorkStatus"
                  :options="workstatus"
                />
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
import {
  //   employeeInstance,
  departmentInstance,
  positionInstance,
} from "../../common";
export default {
  name: "ModalFormEmployee",
  components: {
    LabelInput,
  },
  props: {
    title: {
      type: String,
      require: false,
    },
    show: {
      require: true,
    },
    newId: {
      require: true,
    },
  },
  created() {
    this.getDepartmentData();
    this.getPositionData();
  },
  watch: {
    newId: function (val) {
      this.newData.EmployeeCode = val;
    },
  },
  mounted() {
    console.log("base modal mounted");
  },
  computed: {},
  data() {
    return {
      newData: {
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
    onCloseModal: function () {
      this.EmployeeId = null;
      this.EmployeeCode = null;
      this.FullName = null;
      this.Gender = null;
      this.DateOfBirth = null;
      this.PhoneNumber = null;
      this.Email = null;
      this.IdentityNumber = null;
      this.IdentityDate = null;
      this.IdentityPlace = null;
      this.JoinDate = null;
      this.DepartmentId = null;
      this.PositionId = null;
      this.WorkStatus = null;
      this.PersonalTaxCode = null;
      this.Salary = null;
      this.$emit("close-modal");
    },
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
    onSubmitForm: function (event) {
      event.preventDefault();
      this.$emit("submit-modal",this.newData);
    },
  },
};
</script>

<style>
@import "../../css/Components/ModalForm.css";
</style>