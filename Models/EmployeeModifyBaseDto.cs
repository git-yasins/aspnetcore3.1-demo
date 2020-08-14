using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.ValidationAttributes;
namespace aspnetcore3_demo.Models
{

    [EmployeeNoMustDifferentFormfirstName (ErrorMessage = "员工编号不可以等于名字")]
    public abstract class EmployeeModifyBaseDto : IValidatableObject {
        /// <summary>
        /// 员工编号
        /// </summary>
        /// <value></value>
        [Display (Name = "员工号")]
        [Required (ErrorMessage = "{0}是必填项")]
        //[StringLength (10, MinimumLength = 10, ErrorMessage = "{0}的长度是{1}")]
        [MaxLength (40, ErrorMessage = "员工名称不能超过40")]
        public string EmployeeNo { get; set; }
        /// <summary>
        /// /// 名
        /// </summary>
        /// <value></value>
        [Display (Name = "名")]
        [Required (ErrorMessage = "{0}是必填项")]
        [MaxLength (50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string FirstName { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        /// <value></value>
        [Display (Name = "姓"), Required (ErrorMessage = "{0}是必填项"), MaxLength (50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string LastName { get; set; }
        /// <summary>
        /// 性别 1男 2女
        /// </summary>
        /// <value></value>
        [Display (Name = "性别")]
        [Range (1, 2, ErrorMessage = "性别必须为整形数字1~2 1(男) 或 2(女)")]
        public Gender Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        /// <value></value>
        [Display (Name = "出生日期")]
        //[RegularExpression (@"^((([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29))$
//", ErrorMessage = "输入的必须为合法日期 例如:1999-01-01")]
        public DateTime DateOfbirth { get; set; }

        public IEnumerable<ValidationResult> Validate (ValidationContext validationContext) {
            if (FirstName == LastName) {
                yield return new ValidationResult ("姓和名不能相同", new [] { nameof (FirstName), nameof (LastName) });
            }
        }
    }
}
