using System;

namespace aspnetcore3_demo.Parameters {
    /// <summary>
    /// 公司信息查询
    /// </summary>
    public class CompanyDtoParameters {
        /// <summary>
        /// 最大页数
        /// </summary>
        const int MaxPageSize = 20;
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <value></value>
        public string CompanyName { get; set; }
        /// <summary>
        /// 搜索条件
        /// </summary>
        /// <value></value>
        public string Search { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        /// <value></value>
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;
        /// <summary>
        /// 显示条数
        /// </summary>
        /// <value></value>
        public int PageSize {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        /// <summary>
        /// 数据塑形指定的字段
        /// </summary>
        /// <value></value>
        public string Fields { get; set; }

        /// <summary>
        /// 排序
        /// 默认按Name排序
        /// </summary>
        /// <value></value>
        public string OrderBy { get; set; } = "companyName";
    }
}
