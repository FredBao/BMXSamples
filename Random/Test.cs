namespace Random
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using FlexibleAOPComponent;

    /// <summary>
    /// The test.
    /// </summary>
    [LogHandler]
    [Serializable]
    [ComVisible(true)]
    public class Test : MarshalByRefObject
    {
        /// <summary>
        /// The cleaning station signs.
        /// </summary>
        private readonly List<string> cleaningStationSigns = new List<string>() { "CIO6017", "CIO6016", "CIO6015" };

        public void TestMethod()
        {
            var itemName = "CIO6016";

            var workflowNodeName = this.cleaningStationSigns.Contains(itemName) ? $@"{itemName}" : $@"{itemName}.{1}";

            Console.WriteLine("你进入TestMethod方法了.");
        }

        public virtual void TestMethod2(string name)
        {
            Console.WriteLine($@"{name}:你进入TestMethod2方法了.");
        }
    }
}
