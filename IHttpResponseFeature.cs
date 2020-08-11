using System.Collections.Specialized;
using System.IO;
public interface IHttpResponseFeature
{
    int StatusCode { get; set; }
    NameValueCollection Headers { get; }
    Stream Body { get; }
}