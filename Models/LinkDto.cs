namespace aspnetcore3_demo.Models {
    /// <summary>
    /// HATEOAS 单个资源链接DTO
    /// </summary>
    public class LinkDto {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }

        public LinkDto (string href, string rel, string method) {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
