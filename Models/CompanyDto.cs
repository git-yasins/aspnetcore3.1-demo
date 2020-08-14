using System;

namespace aspnetcore3_demo.Models {
    /// <summary>
    /// 查询公司DTO
    /// </summary>
    public class CompanyDto {
        /// <summary>
        /// 公司ID
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <value></value>
        public string CompanyName { get; set; }
    }
}
