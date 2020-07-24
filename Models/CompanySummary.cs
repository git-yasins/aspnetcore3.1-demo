namespace aspnetcore3_demo.Models
{
    public class CompanySummary
    {
        /// <summary>
        /// 员工数量
        /// </summary>
        /// <value></value>
        public int EmployeeCount { get; set; }
        /// <summary>
        /// 部门平均员工数量
        /// </summary>
        /// <value></value>
        public int AverageDepartmentEmployeeCount { get; set; }
    }
}