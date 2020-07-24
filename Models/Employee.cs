using System.ComponentModel;
namespace aspnetcore3_demo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        [DisplayName("姓")]
        public string FirstName { get; set; }
        [DisplayName("名")]
        public string LastName { get; set; }
        [DisplayName("性别")]
        public Gender Gender { get; set; }
        [DisplayName("是否解雇")]
        public bool Fired { get; set; }
    }

    public enum Gender
    {
        女 = 0,
        男 = 1
    }
}