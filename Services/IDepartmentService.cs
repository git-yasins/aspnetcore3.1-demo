using System.Collections.Generic;
using System.Threading.Tasks;
using aspnetcore3_demo.Models;

namespace aspnetcore3_demo.Services
{
    public interface IDepartmentService
    {
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Department>> GetAll();
        /// <summary>
        /// 根据部门ID获取部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        Task<Department> GetById(int id);
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <returns></returns>
        Task<CompanySummary>GetCompanySummary();
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        Task Add(Department department);
    }
}