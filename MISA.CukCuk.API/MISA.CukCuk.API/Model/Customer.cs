using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Model
{
    public class Customer : BasePersonEntity
    {
        #region Property
        // Mã khách hàng
        public Guid CustomerId { get; set; }

        // Mã khách hàng
        public string CustomerCode { get; set; }

        // Nhóm khách hàng
        public Guid CustomerGroupId { get; set; }

        // Thẻ ghi nợ?
        public double? DebitAmount { get; set; }

        // Thẻ thành viên?
        public string MemberCardCode { get; set; }

        // Tên công ty
        public string CompanyName { get; set; }

        // Mã số thuế 
        public string CompanyTaxCode { get; set; }

        // ? tình trạng theo dõi
        public bool IsStopFollow { get; set; }
        #endregion
    }
}
