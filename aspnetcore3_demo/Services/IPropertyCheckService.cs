namespace aspnetcore3_demo.Services {
    /// <summary>
    /// 数据塑形字段检查
    /// </summary>
    public interface IPropertyCheckService {
        bool TypeHasProperties<T> (string fields);
    }
}
