
using System;
using System.Collections.Specialized;
using System.IO;
public interface IHttpRequestFeature
{
    Uri Url { get; }
    NameValueCollection Headers { get; }
    Stream Body { get; }
}
