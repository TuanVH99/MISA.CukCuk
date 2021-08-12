using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.API.Model;
using MySqlConnector;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách nhân viên
        /// Author: VHT(12/08/21)
        /// </summary>
        /// <returns>Respnose code và Danh sách nhân viên</returns>
        [HttpGet]
        public IActionResult GetEmployees(string employeeCode, string employeeName, int? page, int? limit)
        {
            try
            {
                // Khởi tạo 1 số biến:
                //phân trang
                string pageLimit = (limit == null) ? "" : $"LIMIT {limit}";
                string pageOffset = string.Empty;
                if (limit != null)
                {
                    pageOffset = (page == null && page < 2) ? "" : $"OFFSET {(page - 1) * limit}";
                }
                string pagination = pageLimit + " " + pageOffset;


                //filter theo Mã nhân viên
                string employeeCodeFilter = employeeCode == null ? "" : $"EmployeeCode={employeeCode}";
                // filter theo chứa tên nhân viên
                string employeeNameFilter = employeeName == null ? "" : $"FullName LIKE '%{employeeName}%'";
                // Gộp thành where clause
                string whereClause = string.Empty;
                if (employeeCode != null && employeeName != null)
                {
                    whereClause = $"WHERE {employeeCodeFilter} AND {employeeNameFilter}";
                }
                else if (employeeCode == null && employeeName != null)
                {
                    whereClause = $"WHERE {employeeNameFilter}";
                }
                else if (employeeCode != null && employeeName == null)
                {
                    whereClause = $"WHERE {employeeCodeFilter}";
                }


                //Truy cập vào db
                // 1. Khai báo thông tin kết nối db
                var connectionString = Properties.Resources.MISA_MANHNV_DB_DEMO;

                // 2. Khởi tạo đối tượng kết nối với db
                IDbConnection dbConnection = new MySqlConnection(connectionString);


                // 3.Lấy dữ liệu
                var sqlCommand = $"SELECT * FROM Employee {whereClause} {pagination}";

                var employees = dbConnection.Query<Employee>(sqlCommand);

                // 4. Trả về cho client
                if (employees.Count() > 0)
                {
                    var response = StatusCode(200, employees);
                    return response;
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = "Null List!",
                        userMsg = Properties.Resources.User_Msg
                    };
                    var response = StatusCode(204, employees);
                    return response;
                }
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.User_Msg
                };
                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Lấy nhân viên theo id
        /// Author: VHT(12/08/21)
        /// </summary>
        /// <param name="employeeId">Guid</param>
        /// <returns>Respnose code và Danh sách nhân viên</returns>
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeesById(Guid employeeId)
        {
            try
            {
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

            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.User_Msg
                };
                return StatusCode(500, errObj);
            }
        }

        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {

                //Truy cập vào db
                // 1. Khai báo thông tin kết nối db
                var connectionString = Properties.Resources.MISA_MANHNV_DB_DEMO;

                // 2. Khởi tạo đối tượng kết nối với db
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                // 3.Lấy dữ liệu
                var sqlCommand = $"SELECT EmployeeCode FROM Employee";

                var listCode = dbConnection.Query<string>(sqlCommand);

                //4 xử lí dữ liệu lần 1, tách số và string trong mã nhân viên
                var max = 0;
                foreach (var item in listCode)
                {
                    var tmp = Int32.Parse(item.Split("-")[1]);
                    if (tmp > max)
                    {
                        max = tmp;
                    }
                }
                string newEmployeeCode = "NV-" + (max + 1);

                // 4. Trả về cho client
                var response = StatusCode(200, newEmployeeCode);
                return response;
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.User_Msg
                };
                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Thêm mới nv
        /// Author: VHT(12/08/21)
        /// </summary>
        /// <param name="employee">Dữ liệu từ body</param>
        /// <returns>Số Hàng tác động // rowEffects</returns>
        [HttpPost]
        public IActionResult CreateNewEmployee(Employee employee)
        {
            try
            {
                //Kiểm tra thông tin của khách hàng
                if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
                {
                    var errObj = new
                    {

                        userMsg = Properties.Resources.Employee_Code_Null_Err,

                    };
                    return BadRequest(errObj);
                }
                // kiểm tra email
                var emailFormat = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                var isMatch = Regex.IsMatch(employee.Email, emailFormat, RegexOptions.IgnoreCase);
                if (isMatch == false)
                {
                    var errObj = new
                    {
                        userMsg = Properties.Resources.Email_Format_Err,
                    };
                    return BadRequest(errObj);
                }
                //kiểm tra trùng mã

                //Truy cập vào db
                // 1. Khai báo thông tin kết nối db
                var connectionString = Properties.Resources.MISA_MANHNV_DB_DEMO;

                // 2. Khởi tạo đối tượng kết nối với db
                IDbConnection dbConnection0 = new MySqlConnection(connectionString);

                // 3.Lấy dữ liệu
                var sqlCommand0 = $"SELECT * FROM Employee where EmployeeCode = @EmployeeCodeParam";
                var dynamicParam0 = new DynamicParameters();
                dynamicParam0.Add("@EmployeeCodeParam", employee.EmployeeCode);
                var employeeWithCode = dbConnection0.QueryFirstOrDefault<Employee>(sqlCommand0, param: dynamicParam0);
                if (employeeWithCode != null)
                {
                    var errObj = new
                    {
                        userMsg = Properties.Resources.Duplicate_Employee_Code_Err,
                    };
                    return BadRequest(errObj);
                }

                // tạo id mới
                employee.EmployeeId = Guid.NewGuid();
                //Truy cập vào db


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
                var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);
                // 4. Trả về cho client
                if (rowEffects == 1)
                {
                    var responseObj = new
                    {
                        userMsg = Properties.Resources.Action_Successful
                    };
                    var response = StatusCode(201, responseObj);
                    return response;
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = "",
                        userMsg = Properties.Resources.User_Msg,
                    };
                    return StatusCode(500, errObj);
                }
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.User_Msg
                };
                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// Chỉnh sửa dữ liệu nhân viên
        /// Author: VHT(12/08/21)
        /// </summary>
        /// <param name="employeeId">id nv cần chỉnh sửa</param>
        /// <param name="employeeNew">dữ liệu chỉnh sửa truyền vào từ body</param>
        /// <returns>Số Hàng tác động // rowEffects</returns>
        [HttpPut("{employeeId}")]
        public IActionResult ModifyEmployee(Guid employeeId, Employee employeeNew)
        {
            try
            {
                //Truy cập vào db
                // 1. Khai báo thông tin kết nối db
                var connectionString = Properties.Resources.MISA_MANHNV_DB_DEMO;

                // 2. Khởi tạo đối tượng kết nối với db
                IDbConnection dbConnection = new MySqlConnection(connectionString);
                var dynamicParam = new DynamicParameters();
                dynamicParam.Add("@EmployeeIdParam", employeeId);

                // 3.Khởi tạo sql thêm dữ liệu
                var columnsStatement = string.Empty;

                // Lấy ra các prop
                var properties = employeeNew.GetType().GetProperties();

                // duyệt từng prop
                foreach (var prop in properties)
                {
                    //lấy tên prop
                    var propName = prop.Name;

                    //lấy giá trị tương ứng của prop
                    var propValue = prop.GetValue(employeeNew);

                    //lấy kiểu dữ liệu của prop
                    var propType = prop.PropertyType;

                    // thêm param tương ứng với mỗi prop của đối tượng
                    dynamicParam.Add($"@{propName}", propValue);


                    columnsStatement += $"{propName} = @{propName},";
                }

                //loại bỏ dấu , ở cuối câu
                columnsStatement = columnsStatement.Remove(columnsStatement.Length - 1, 1);

                var sqlCommand = $"UPDATE Employee SET {columnsStatement} WHERE EmployeeId = @EmployeeIdParam";

                // thực hiện câu sql
                var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);

                // 4. Trả về cho client
                var response = StatusCode(200, rowEffects);
                return response;
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.User_Msg
                };
                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// Xóa nhân viên theo id
        /// Author: VHT(12/08/21)
        /// </summary>
        /// <param name="employeeId">Id nhân viên cần xóa</param>
        /// <returns>Số Hàng tác động // rowEffects</returns>
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(Guid employeeId)
        {
            try
            {
                //Truy cập vào db
                // 1. Khai báo thông tin kết nối db
                var connectionString = Properties.Resources.MISA_MANHNV_DB_DEMO;

                // 2. Khởi tạo đối tượng kết nối với db
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                // 3.Lấy dữ liệu
                var sqlCommand = $"DELETE FROM Employee WHERE EmployeeId = @EmployeeIdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeIdParam", employeeId);
                var rowEffects = dbConnection.Execute(sqlCommand, param: parameters);

                // 4. Trả về cho client
                var response = StatusCode(200, rowEffects);
                return response;
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.User_Msg
                };
                return StatusCode(500, errObj);
            }
        }
    }
}
