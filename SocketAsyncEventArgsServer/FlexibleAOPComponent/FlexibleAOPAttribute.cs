namespace FlexibleAOPComponent
{
    using System;
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting.Proxies;

    public class FlexibleAOPAttribute : ProxyAttribute
    {
        private DateTime _EndTime;

        private DateTime _StartTime;

        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            FlexibleDynamicProxy proxy = new FlexibleDynamicProxy(serverType);

            // proxy.Filter = m => true;// m.Name.StartsWith("Add");
            // proxy.BeforeExecute += Proxy_BeforeExecute;
            // proxy.AfterExecute += Proxy_AfterExecute;
            // proxy.ErrorExecuting += Proxy_ErrorExecuting;
            return proxy.GetTransparentProxy() as MarshalByRefObject;
        }

        private void Proxy_AfterExecute(
            object sender,
            IMethodCallMessage e,
            object returnValue)
        {
        }

        private void Proxy_BeforeExecute(object sender, IMethodCallMessage e)
        {
        }

        private void Proxy_ErrorExecuting(
            object sender,
            IMethodCallMessage e,
            object innerException)
        {
        }
    }
}