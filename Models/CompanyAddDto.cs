using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace aspnetcore3_demo.Models {
    /// <summary>
    /// 新增公司DTO
    /// </summary>
    public class CompanyAddDto {
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <value></value>
        [Required (ErrorMessage = "公司名称不能为空")]
        [MaxLength (100, ErrorMessage = "{0}的最大长度不能超过{1}个字符")]
        [Display (Name = "公司名称")]
        public string Name { get; set; }
        /// <summary>
        /// 公司介绍
        /// /// /// </summary>
        /// <value></value>
        [Required]
        [Display (Name = "公司简介")]
        [StringLength (500, MinimumLength = 10, ErrorMessage = "{0}的长度范围从{2}到{1}")]
        public string Introduction { get; set; }
        /// <summary>
        /// Employees名称与Company实体类的Employees字段命名一致
        /// </summary>
        /// <params name="EmployeeAddDto">新增员工实体</params>
        /// <returns></returns>
        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto> ();
    }
}
