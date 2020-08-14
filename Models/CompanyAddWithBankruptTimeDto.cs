using System;

namespace aspnetcore3_demo.Models {
    /// <summary>
    /// 新增公司DTO属性
    /// </summary>
    public class CompanyAddWithBankruptTimeDto : CompanyAddDto {
        /// <summary>
        /// 破产时间
        /// </summary>
        /// <value></value>
        public DateTime? BankrupTime { get; set; }
    }
}
