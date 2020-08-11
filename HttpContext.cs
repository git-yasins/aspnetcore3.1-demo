using System.Threading.Tasks;
public delegate Task RequestDelegate(HttpContext contex);
public class HttpContext
{
    public HttpRequest Request { get; }
    public HttpResponse Response { get; }

    public HttpContext(IFeatureCollection features)
    {
        Request = new HttpRequest(features);
        Response = new HttpResponse(features);
    }
}