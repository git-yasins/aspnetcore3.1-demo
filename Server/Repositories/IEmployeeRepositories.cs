using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspnetcore3_demo.Repositories {
    public interface IEmployeeRepositories {
        /// <summary>
        /// 添加员工
        /// /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<Employee> Add (Employee employee);
        /// <summary>
        /// 根据部门查询员工
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>员工集合</returns>
        Task<IEnumerable<Employee>> GetByDepartmentId (int departmentId);
        /// <summary>
        /// 解雇员工
        /// </summary>
        /// <param name="id">员工ID</param>
        Task<Employee> Fire (int id);
    }
}
