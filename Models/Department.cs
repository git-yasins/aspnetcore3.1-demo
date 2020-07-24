
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace aspnetcore3_demo.Models
{
    public class Department
    {
        public int Id { get; set; }
        [DisplayName("部门名称")]
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(50)]
        [DisplayName("地点")]
        public string Location { get; set; }
        [DisplayName("员工数量")]
        [Integer(ErrorMessage = "必须为整数")]
        public int EmployeeCount { get; set; }
    }
}
