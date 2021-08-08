import axios from "axios";

/**
 * @param {Date} inputDateString
 * @param {String} typeOfFormat
 * @returns
 * TODO: Chuyển về format ngày tháng, 2 option
 */
export const formatDate = (inputDateString, typeOfFormat) => {
  const defaultFormat = new Date(inputDateString);
  if (typeOfFormat == "yyyy-mm-dd") {
    var date =
      defaultFormat.getDate() < 10
        ? "0" + defaultFormat.getDate()
        : defaultFormat.getDate();
    var month =
      defaultFormat.getMonth() < 9
        ? "0" + (defaultFormat.getMonth() + 1)
        : defaultFormat.getMonth() + 1;
    return `${defaultFormat.getFullYear()}-${month}-${date}`;
  }
  if (typeOfFormat == "dd/mm/yyyy") {
    return (
      defaultFormat.getDate() +
      "/" +
      (defaultFormat.getMonth() + 1) +
      "/" +
      defaultFormat.getFullYear()
    );
  }
  return defaultFormat;
};

/**
 *
 * @param {String} input
 * @param {String Formatted} type
 * @returns
 * TODO: Chuyển về format tiền tệ và ngược lại
 */
export const formatCurrency = (input, type = 1) => {
  if (input) {
    // return input.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    if (type == 0) {
      return Number(input.replaceAll(".", ""));
    } else {
      return new Intl.NumberFormat("de-DE").format(input);
    }
  }
  return "";
};

/**
 * TODO: Tạo các axios instace với base url
 */
export const employeeInstance = axios.create({
  baseURL: "http://cukcuk.manhnv.net/v1/Employees",
});

export const departmentInstance = axios.create({
  baseURL: "http://cukcuk.manhnv.net/api/Department",
});

export const positionInstance = axios.create({
  baseURL: "http://cukcuk.manhnv.net/v1/Positions",
});

/**
 * ? Chia Mảng thành các mảng có size như nhau...
 * @param {Array} arr
 * @param {Number} chunkSize
 * @returns Array
 */
export function sliceIntoChunks(arr, chunkSize) {
  const res = [];
  for (let i = 0; i < arr.length; i += chunkSize) {
    const chunk = arr.slice(i, i + chunkSize);
    res.push(chunk);
  }
  return res;
}

/**
 * ? Fill các prop object truyền vào với stirng hoặc null
 * @param {Object} obj
 * @returns Object
 */
export function fillObj(obj, option) {
  for (const key in obj) {
    if (Object.hasOwnProperty.call(obj, key)) {
      obj[key] = option;
    }
  }
  return obj;
}

/**
 * ? Xem xét email có đúng định dạng hay không
 * @param {String} emailString
 * @returns
 */
export function ValidateEmailAddress(emailString) {
  // phải có ký tự @
  var atSymbol = emailString.indexOf("@");
  if (atSymbol < 1) return false;

  var dot = emailString.indexOf(".");
  if (dot <= atSymbol + 2) return false;

  // Dấu chấm không được để cuối dòng
  if (dot === emailString.length - 1) return false;

  return true;
}
