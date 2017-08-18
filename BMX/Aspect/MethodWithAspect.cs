namespace BMX.Aspect
{
    using System;

    using Castle.DynamicProxy;

    [Serializable]
    public class MethodWithAspect : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
