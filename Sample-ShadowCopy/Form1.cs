namespace Sample_ShadowCopy
{
    using System;
    using System.Reflection;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lb_Monitor.Text += "开始\r\n";
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            setup.ApplicationName = "ExtFuncs";
            setup.PrivateBinPath = "ExtDlls";
            setup.ShadowCopyFiles = "true";
            setup.CachePath = @"TempAss";
            AppDomain newDom = AppDomain.CreateDomain("Hello", null, setup);

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
                        this.lb_Monitor.Text += $"调用输出：{retval}" + "\r\n";

                        // 输出引用程序集的路径
                        var refAsses = AppDomain.CurrentDomain.GetAssemblies();
                        foreach (var ass in refAsses)
                        {
                            this.lb_Monitor.Text += "名称：" + ass.GetName().Name + "\r\n";
                            this.lb_Monitor.Text += "路径：" + ass.Location + "\r\n";
                            this.lb_Monitor.Text += "\r\n";
                        }
                    });
            AppDomain.Unload(newDom); // 卸载应用程序域
            this.lb_Monitor.Text += "结束\r\n";
        }
    }
}