using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Sample_WcfServiceLibrary
{
    using FanucToolManageDll;

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            string notice = string.Empty;

            try
            {
                var ftm = new FanucToolManageDll(50);

                uint cutterCompensationMax;

                if (ftm.Connect("10.10.100.39", 8193, 3000))
                {
                    notice += "Connected!";


                    if (ftm.GetThresholdValue(5013, out cutterCompensationMax))
                    {

                        notice += $"输入最大上限{ cutterCompensationMax}!";
                    }
                }
                else
                {
                    notice += "Connected Failed!";
                }

                ftm.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return string.Format("You entered: {0}", notice + value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
