using Castle.DynamicProxy;

namespace aspnetcore3_demo.Services {
    //测试Autofac AOP注册容器服务
    public class MyInterceptor : IInterceptor {
        public void Intercept (IInvocation invocation) {
            System.Console.WriteLine ($"Intercept before,Method:{invocation.Method.Name}");
            invocation.Proceed ();//执行被拦截的实例默认内容
            System.Console.WriteLine ($"Intercept after,Method:{invocation.Method.Name}");
        }
    }
}
