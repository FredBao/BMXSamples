namespace TestConsole
{
    using System;
    using System.Reflection;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(5000);
                Run();
            }
        }

        private static void Run()
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            setup.ApplicationName = "ExtFuncs";
            setup.PrivateBinPath = "ExtDlls";
            setup.ShadowCopyFiles = "true";
            AppDomain newDom = AppDomain.CreateDomain("hello", null, setup);

            newDom.DoCallBack(
                () =>
                    {
                        Type t = Type.GetType("ExtendedDLL.TestClass, ExtendedDLL");

                        // 获取公共无参构造函数
                        ConstructorInfo costr = t.GetConstructor(new Type[] { });

                        // 调用构造函数，创建类型实例
                        object instance = costr.Invoke(new object[] { });

                        // 找到要调用的方法
                        MethodInfo m = t.GetMethod("Print", BindingFlags.Public | BindingFlags.Instance);

                        // 调用方法，得到返回值
                        object retval = m.Invoke(instance, new object[] { });
                        Console.WriteLine($"调用输出：{retval}");
                        Console.WriteLine("\n===================================");

                        // 输出引用程序集的路径
                        var refAsses = AppDomain.CurrentDomain.GetAssemblies();
                        foreach (var ass in refAsses)
                        {
                            Console.WriteLine("名称：" + ass.GetName().Name);
                            Console.WriteLine("路径：" + ass.Location);
                            Console.WriteLine();
                        }
                    });
            AppDomain.Unload(newDom); // 卸载应用程序域
        }
    }
}