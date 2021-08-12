using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Model
{
    public class Employee : BasePersonEntity
    {
        #region Properties
        // Khóa chính
        public Guid EmployeeId { get; set; }

        // Mã nhân viên
        public string EmployeeCode { get; set; }

        // Số CMND/CCCD
        public string IdentityNumber { get; set; }

        // Ngày cấp
        public DateTime? IdentityDate { get; set; }

        // Nơi cấp
        public string IdentityPlace { get; set; }

        // Ngày gia nhập công ty
        public DateTime? JoinDate { get; set; }

        // Id phòng ban
        public Guid DepartmentId { get; set; }

        // Id vị trí
        public Guid PositionId { get; set; }

        // Tình trạng công việc
        public int? WorkStatus { get; set; }

        // Mức lương cơ bản
        public double? Salary { get; set; }
        #endregion

    }
}
