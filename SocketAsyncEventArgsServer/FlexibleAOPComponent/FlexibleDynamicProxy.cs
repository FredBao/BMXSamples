namespace FlexibleAOPComponent
{
    using System;
    using System.Reflection;
    using System.Runtime.Remoting.Activation;
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting.Proxies;

    using SocketAsyncEventArgsServer.Helper;

    #region 泛型动态代理

    // public sealed class FlexibleDynamicProxy<T> : RealProxy
    // {
    // //设置真实对象引用
    // private readonly T _decorated;

    // public FlexibleDynamicProxy(T decorated) : base(typeof(T))
    // {
    // _decorated = decorated;
    // }
    // //记录方法调用的函数
    // private void Log(string msg, object arg = null)
    // {
    // Console.ForegroundColor = ConsoleColor.Red;
    // File.AppendAllLines("Logs/Log.txt", new string[] {
    // "Execute DateTime : "+DateTime.Now,
    // msg, arg.ToString() });
    // Console.ResetColor();
    // }
    // //调用代理的类型的方法
    // public override IMessage Invoke(IMessage msg)
    // {
    // object returnIMessage = null;
    // //如果调用方法是一般方法，进入If语句
    // if (msg is IMethodCallMessage)
    // {
    // var methodCall = msg as IMethodCallMessage;
    // var methodInfo = methodCall.MethodBase as MethodInfo;
    // Log("In Dynamic Proxy - Before executing '{0}'", methodCall.MethodName);
    // try
    // {
    // var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
    // Log("In Dynamic Proxy - After executing '{0}' ", methodCall.MethodName);
    // returnIMessage = new ReturnMessage(result, null, 0,
    // methodCall.LogicalCallContext, methodCall);
    // }
    // catch (Exception ex)
    // {
    // Log(string.Format(
    // "In Dynamic Proxy- Exception {0} executing '{1}'", ex),
    // methodCall.MethodName);
    // returnIMessage = new ReturnMessage(ex, methodCall);
    // }
    // }
    // //如果是调用了构造函数就进入使用下面的代码
    // else
    // {
    // //构造函数调用
    // IConstructionCallMessage constructCallMsg = msg as IConstructionCallMessage;
    // Log("In Dynamic Proxy - Before Call The Constructor Method '{0}'", constructCallMsg.MethodName);
    // IConstructionReturnMessage constructionReturnMessage = this.InitializeServerObject((IConstructionCallMessage)msg);
    // RealProxy.SetStubData(this, constructionReturnMessage.ReturnValue);
    // returnIMessage = constructionReturnMessage;
    // }
    // return returnIMessage as IMessage;
    // }
    // }
    #endregion

    #region 一般动态代理

    public delegate void CustomeProxyEventHander<TEventArgs>(object sender, TEventArgs e, object obj);

    public sealed class FlexibleDynamicProxy : RealProxy
    {
        private static object obj = new object();

        private Predicate<MethodInfo> _Filter;

        public FlexibleDynamicProxy(Type type)
            : base(type)
        {
            this._Filter = m => true;
        }

        public event CustomeProxyEventHander<IMethodCallMessage> AfterExecute;

        public event EventHandler<IMethodCallMessage> BeforeExecute;

        public event CustomeProxyEventHander<IMethodCallMessage> ErrorExecuting;

        public Predicate<MethodInfo> Filter
        {
            get
            {
                return this._Filter;
            }

            set
            {
                if (value == null) this._Filter = m => true;
                else this._Filter = value;
            }
        }

        // 调用代理的类型的方法
        public override IMessage Invoke(IMessage msg)
        {
            DateTime st = DateTime.Now;

            // 用lock来防止多线程在一个时间同时操作方法
            object returnIMessage = null;
            IMessage message;
            lock (obj)
            {
                // 如果是调用了构造函数就进入使用下面的代码 
                if (msg is IConstructionCallMessage)
                {
                    // 构造函数调用
                    IConstructionCallMessage constructCallMsg = msg as IConstructionCallMessage;
                    IConstructionReturnMessage constructionReturnMessage =
                        this.InitializeServerObject((IConstructionCallMessage)msg);
                    SetStubData(this, constructionReturnMessage.ReturnValue);
                    returnIMessage = constructionReturnMessage;
                }

                // 如果调用方法是一般方法，进入If语句
                else
                {
                    var methodCall = msg as IMethodCallMessage;
                    var methodInfo = methodCall.MethodBase as MethodInfo;

                    // OnBeforeExecute(methodCall);
                    try
                    {
                        var result = methodInfo.Invoke(this.GetUnwrappedServer(), methodCall.InArgs);
                        message = new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);

                        // OnAfterExecute(methodCall, message.Properties["__Return"]);
                        returnIMessage = message;
                    }
                    catch (Exception ex)
                    {
                        // OnErrorExecuting(methodCall, ex.InnerException.ToString());
                        LogHelper.Log.InfoFormat(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                        returnIMessage = new ReturnMessage(ex, methodCall);
                    }
                }
            }

            return returnIMessage as IMessage;
        }

        private void OnAfterExecute(IMethodCallMessage methodCall, object returnValue = null)
        {
            if (this.AfterExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (this._Filter(methodInfo)) this.AfterExecute(this, methodCall, returnValue);
            }
        }

        private void OnBeforeExecute(IMethodCallMessage methodCall)
        {
            if (this.BeforeExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (this._Filter(methodInfo)) this.BeforeExecute(this, methodCall);
            }
        }

        private void OnErrorExecuting(IMethodCallMessage methodCall, string innerException)
        {
            if (this.ErrorExecuting != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (this._Filter(methodInfo)) this.ErrorExecuting(this, methodCall, innerException);
            }
        }
    }

    #endregion
}