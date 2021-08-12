using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.API.Model;
using MySqlConnector;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //GET POST PUT DELETE
        [HttpGet]
        public IActionResult GetCustomers()
        {
            //Truy cập vào db
            // 1. Khai báo thông tin kết nối db
            var connectionString = "User Id = dev; Host = 47.241.69.179;Password = 12345678 ;Database = MISA.CukCuk_Demo_NVMANH;";

            // 2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3.Lấy dữ liệu
            var sqlCommand = "SELECT * FROM Employee";
            var customers = dbConnection.Query<object>(sqlCommand);

            // 4. Trả về cho client
            var response = StatusCode(200, customers);
            return response;
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(string customerId)
        {
            //Truy cập vào db
            // 1. Khai báo thông tin kết nối db
            var connectionString = "User Id = dev; Host = 47.241.69.179;Password = 12345678 ;Database = MISA.CukCuk_Demo_NVMANH;";

            // 2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3.Lấy dữ liệu
            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeId = @EmployeeIdParam";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeIdParam", customerId);
            var customers = dbConnection.QueryFirstOrDefault<object>(sqlCommand, param: parameters);

            // 4. Trả về cho client
            var response = StatusCode(200, customers);
            return response;

        }


        //[HttpPost]
        //public IActionResult InsertCustomer(Customer customers)
        //{
        ////Truy cập vào db
        //// 1. Khai báo thông tin kết nối db
        //var connectionString = "User Id = dev; Host = 47.241.69.179;Password = 12345678 ;Database = MISA.CukCuk_Demo_NVMANH;";

        //// 2. Khởi tạo đối tượng kết nối với db
        //IDbConnection dbConnection = new MySqlConnection(connectionString);

        //// 3.Thêm dữ liệu vào db
        //var columnsName = string.Empty;
        //var columnsParam = string.Empty;

        //// Đọc từng property của object
        //var properties = customers

        //var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeId = @EmployeeIdParam";
        //DynamicParameters parameters = new DynamicParameters();
        //parameters.Add("@EmployeeIdParam", customerId);
        //var customers = dbConnection.QueryFirstOrDefault<object>(sqlCommand, param: parameters);

        //// 4. Trả về cho client
        //var response = StatusCode(200, customers);
        //return response;

        //}



    }
}
