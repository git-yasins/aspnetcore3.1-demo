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
        /// <summary>
        /// 国家/地区
        /// </summary>
        /// <value></value>
        public string Country { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        /// <value></value>
        public string Industry { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        /// <value></value>
        public string Product { get; set; }
        /// <summary>
        /// 公司简介
        /// </summary>
        /// <value></value>
        public string Introduction { get; set; }
    }
}
