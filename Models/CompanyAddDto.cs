using System.Collections.Generic;

namespace aspnetcore3_demo.Models {
    /// <summary>
    /// 新增公司DTO
    /// </summary>
    public class CompanyAddDto {
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// 公司介绍
        /// /// </summary>
        /// <value></value>
        public string Introduction { get; set; }
        /// <summary>
        /// Employees名称与Company实体类的Employees字段命名一致
        /// </summary>
        /// <params name="EmployeeAddDto">新增员工实体</params>
        /// <returns></returns>
        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto> ();
    }
}
