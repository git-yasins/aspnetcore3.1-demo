using System;
using aspnetcore3_demo.Entities;

namespace aspnetcore3_demo.Models {
    /// <summary>
    /// 新增员工DTO
    /// </summary>
    public class EmployeeAddDto {
        /// <summary>
        /// 员工编号
        /// </summary>
        /// <value></value>
        public string EmployeeNo { get; set; }
        /// <summary>
        /// 姓氏
        /// </summary>
        /// <value></value>
        public string FirstName { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        /// <value></value>
        public string LastName { get; set; }
        /// <summary>
        /// 性别 1男 2女
        /// </summary>
        /// <value></value>
        public Gender Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        /// <value></value>
        public DateTime DateOfbirth { get; set; }
    }
}
