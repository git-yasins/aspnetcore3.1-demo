namespace aspnetcore3_demo.Parameters {
    /// <summary>
    /// 员工分页参数
    /// </summary>
    public class EmployeeDtoParameters {
        /// <summary>
        /// 最大条数
        /// </summary>
        private const int MaxPageSize = 20;
        /// <summary>
        /// 性别
        /// </summary>
        /// <value></value>
        public string Gender { get; set; }
        /// <summary>
        /// 搜索条件
        /// </summary>
        /// <value></value>
        public string Q { get; set; }
        /// <summary>
        /// 当前第几页
        /// </summary>
        /// <value></value>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// 默认5条
        /// </summary>
        private int _pageSize = 5;
        /// <summary>
        /// 每页多少条
        /// </summary>
        /// <value></value>
        public int PageSize {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        /// <summary>
        /// 排序
        /// 默认按Name排序
        /// </summary>
        /// <value></value>
        public string OrderBy { get; set; } = "name";
    }
}
