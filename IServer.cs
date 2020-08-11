using System.Threading.Tasks;
public interface IServer
{
    Task StartAsync(RequestDelegate handler);
}