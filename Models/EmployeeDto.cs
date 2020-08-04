using System;

namespace aspnetcore3_demo.Models {
    /// <summary>
    /// 查询员工DTO
    /// </summary>
    public class EmployeeDto {
        /// <summary>
        /// 员工ID
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        /// <value></value>
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        /// <value></value>
        public string EmployeeNo { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// 显示性别
        /// </summary>
        /// <value></value>
        public string GenderDisplay { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        /// <value></value>
        public int Age { get; set; }
    }
}
