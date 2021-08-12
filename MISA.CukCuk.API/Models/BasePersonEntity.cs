using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Model
{
    public class BasePersonEntity : BaseEntity
    {
        #region Properties
        // 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        // Họ và tên
        public string FullName { get; set; }

        //Giới tính
        public int? Gender { get; set; }

        // Ngày sinh
        public DateTime? DateOfBirth { get; set; }

        //Số điện thoại
        public string PhoneNumber { get; set; }

        // Địa chỉ email
        public string Email { get; set; }

        #endregion
    }
}
