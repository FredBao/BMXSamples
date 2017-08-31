namespace Bmx.Abp.Aspect
{
    using System;
    using System.Runtime.Remoting.Proxies;
    using System.Text;

    public class LogHandlerAttribute : ProxyAttribute
    {
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            var proxy = new CustomProxy(serverType)
            {
                Filter = m => !m.Name.Contains("ServerIsRunning")
            };

            proxy.BeforeExecute += Proxy_BeforeExecute;
            proxy.AfterExecute += Proxy_AfterExecute;
            proxy.ErrorExecuting += Proxy_ErrorExecuting;

            return proxy.GetTransparentProxy() as MarshalByRefObject;
        }

        private static void Proxy_BeforeExecute(object sender, System.Runtime.Remoting.Messaging.IMethodCallMessage e)
        {
            var index = 0;

            var args = new StringBuilder();

            foreach (var arg in e.Args)
            {
                args.Append(e.GetInArgName(index++));
                args.Append(" : ");
                args.Append(arg + ",");
            }

            var executedMethod = $@"Method : {e.MethodName} {args} Is Ready To Be Call.";
        }

        private static void Proxy_AfterExecute(object sender, System.Runtime.Remoting.Messaging.IMethodCallMessage e, object returnValue)
        {
            var index = 0;

            var args = new StringBuilder();

            foreach (var arg in e.Args)
            {
                args.Append(e.GetInArgName(index++));
                args.Append(" : ");
                args.Append(arg + ",");
            }

            var executedMethod = $@"Method : {e.MethodName} {args} Is Callled.";
        }

        private static void Proxy_ErrorExecuting(object sender, System.Runtime.Remoting.Messaging.IMethodCallMessage e, object innerException)
        {
            var index = 0;

            var args = new StringBuilder();

            foreach (var arg in e.Args)
            {
                args.Append(e.GetInArgName(index++));
                args.Append(" : ");
                args.Append(arg + ",");
            }

            var executedMethod = $@"Method : {e.MethodName} {args} Occurs An Exception : {innerException}";
        }
    }
}
