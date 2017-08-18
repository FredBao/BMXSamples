namespace BMX.Aspect
{
    using System;
    using System.Reflection;
    using System.Runtime.Remoting.Activation;
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting.Proxies;

    #region 一般动态代理

    public delegate void CustomProxyEventHander<in TEventData>(object sender, TEventData eventData, object obj);

    public sealed class CustomProxy : RealProxy
    {
        private static readonly object threadSyncObject = new object();

        private Predicate<MethodInfo> filter;

        public CustomProxy(Type type) : base(type)
        {
            this.filter = m => true;
        }

        public event EventHandler<IMethodCallMessage> BeforeExecute;

        public event CustomProxyEventHander<IMethodCallMessage> AfterExecute;

        public event CustomProxyEventHander<IMethodCallMessage> ErrorExecuting;

        public Predicate<MethodInfo> Filter
        {
            get
            {
                return this.filter;
            }

            set
            {
                if (value == null)
                    this.filter = m => true;
                else
                    this.filter = value;
            }
        }

        /// <summary>
        /// 调用代理的类型的方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            object returnIMessage = null;

            lock (threadSyncObject)
            {
                var callMsg = msg as IConstructionCallMessage;

                if (callMsg != null)
                {
                    var constructionReturnMessage = this.InitializeServerObject(callMsg);

                    if (constructionReturnMessage == null)
                    {
                        return null;
                    }

                    CustomProxy.SetStubData(this, constructionReturnMessage.ReturnValue);

                    returnIMessage = constructionReturnMessage;
                }
                else
                {
                    var methodCall = msg as IMethodCallMessage;

                    if (methodCall == null)
                    {
                        return null;
                    }

                    var methodInfo = methodCall.MethodBase as MethodInfo;

                    this.OnBeforeExecute(methodCall);

                    try
                    {
                        if (methodInfo != null)
                        {
                            var result = methodInfo.Invoke(this.GetUnwrappedServer(), methodCall.InArgs);

                            IMessage message = new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);

                            this.OnAfterExecute(methodCall, message.Properties["__Return"]);

                            returnIMessage = message;
                        }
                    }
                    catch (Exception exception)
                    {
                        if (exception.InnerException != null)
                        {
                            this.OnErrorExecuting(methodCall, exception.InnerException.ToString());
                        }

                        returnIMessage = new ReturnMessage(exception, methodCall);
                    }
                }
            }

            return (IMessage)returnIMessage;
        }

        private void OnBeforeExecute(IMethodCallMessage methodCall)
        {
            if (this.BeforeExecute == null) return;

            var methodInfo = methodCall.MethodBase as MethodInfo;

            if (this.filter(methodInfo))
            {
                this.BeforeExecute?.Invoke(this, methodCall);
            }
        }

        private void OnAfterExecute(IMethodCallMessage methodCall, object returnValue = null)
        {
            if (this.AfterExecute == null) return;

            var methodInfo = methodCall.MethodBase as MethodInfo;

            if (this.filter(methodInfo))
            {
                this.AfterExecute?.Invoke(this, methodCall, returnValue);
            }
        }

        private void OnErrorExecuting(IMethodCallMessage methodCall, string innerException)
        {
            if (this.ErrorExecuting == null) return;

            var methodInfo = methodCall.MethodBase as MethodInfo;

            if (this.filter(methodInfo))
            {
                this.ErrorExecuting?.Invoke(this, methodCall, innerException);
            }
        }
    }
    #endregion
}
