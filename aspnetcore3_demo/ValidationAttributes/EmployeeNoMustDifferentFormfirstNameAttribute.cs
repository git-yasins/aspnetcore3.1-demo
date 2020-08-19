using System;
using System.ComponentModel.DataAnnotations;
using aspnetcore3_demo.Models;

namespace aspnetcore3_demo.ValidationAttributes {
    /// <summary>
    /// 验证员工信息 自定义Attribute
    /// 可作用于类级别,也可作用于Model属性级别
    /// </summary>
    public class EmployeeNoMustDifferentFormfirstNameAttribute : ValidationAttribute {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {
            var addDto = (EmployeeModifyBaseDto) validationContext.ObjectInstance;
            if (addDto.EmployeeNo == addDto.FirstName) {
                return new ValidationResult (ErrorMessage, new [] { nameof (EmployeeModifyBaseDto) });
            }
            return ValidationResult.Success;
        }
    }
}
