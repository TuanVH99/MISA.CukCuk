using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.API.Model;
using MySqlConnector;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Lấy dánh sách nhân viên
        /// </summary>
        /// <returns>Respnose code và Danh sách nhân viên</returns>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            //Truy cập vào db
            // 1. Khai báo thông tin kết nối db
            var connectionString = Properties.Resources.MISA_MANHNV_DB_DEMO;

            // 2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3.Lấy dữ liệu
            var sqlCommand = "SELECT * FROM Employee";
            var employees = dbConnection.Query<object>(sqlCommand);

            // 4. Trả về cho client
            var response = StatusCode(200, employees);
            return response;
        }

        /// <summary>
        /// Lấy nhân viên theo id
        /// </summary>
        /// <param name="employeeId">Guid</param>
        /// <returns>Respnose code và Danh sách nhân viên</returns>
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeesById(Guid employeeId)
        {
            //try
            //{
            //Truy cập vào db
            // 1. Khai báo thông tin kết nối db
            var connectionString = Properties.Resources.MISA_MANHNV_DB_DEMO;

            // 2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3.Lấy dữ liệu
            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeId = @EmployeeIdParam";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeIdParam", employeeId);
            var employees = dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);

            // 4. Trả về cho client
            var response = StatusCode(200, employees);
            return response;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        //[HttpGet]
        //public IActionResult GetNewEmployeeCode()
        //{
        //    return null;
        //}

        [HttpPost]
        public IActionResult CreateNewEmployee(Employee employee)
        {
            // tạo id mới
            employee.EmployeeId = Guid.NewGuid();
            //Truy cập vào db
            // 1. Khai báo thông tin kết nối db
            var connectionString = Properties.Resources.MISA_MANHNV_DB_DEMO;

            // 2. Khởi tạo đối tượng kết nối với db
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            var dynamicParam = new DynamicParameters();

            // 3.Khởi tạo sql thêm dữ liệu
            var columnsName = string.Empty;
            var columnsParam = string.Empty;

            // Lấy ra các prop
            var properties = employee.GetType().GetProperties();

            // duyệt từng prop
            foreach (var prop in properties)
            {
                //lấy tên prop
                var propName = prop.Name;

                //lấy giá trị tương ứng của prop
                var propValue = prop.GetValue(employee);

                //lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                // thêm param tương ứng với mỗi prop của đối tượng
                dynamicParam.Add($"@{propName}", propValue);

                columnsName += $"{propName},";
                columnsParam += $"@{propName},";
            }

            //loại bỏ dấu , ở cuối câu
            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);

            var sqlCommand = $"INSERT INTO Employee({columnsName}) VALUE ({columnsParam})";

            // thực hiện câu sql
            var rowEffect = dbConnection.Execute(sqlCommand, param: dynamicParam);

            // 4. Trả về cho client
            var response = StatusCode(200, rowEffect);
            return response;

        }

        //[HttpPut("{employeeId}")]
        //public IActionResult ModifyEmployee(Guid employeeId)
        //{
        //    return null;
        //}

        //[HttpDelete("{employeeId}")]
        //public IActionResult DeleteEmployee(Guid employeeId)
        //{
        //    return null;
        //}
    }
}
