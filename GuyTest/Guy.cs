using System;

namespace GuyTest
{
    /// <summary>
    /// 一个伙计，拥有名称，年龄和装满现金的钱包
    /// </summary>
    class Guy
    {
        /// <summary>
        /// 为 Name 属性创建的支持只读的字段
        /// </summary>
        private readonly string name;

        /// <summary>
        /// 伙计的名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// 为 age 属性创建的支持只读的字段
        /// </summary>
        private readonly int age;

        /// <summary>
        /// 伙计的年龄
        /// </summary>
        public int Age
        {
            get
            {
                return age;
            }
        }

        /// <summary>
        /// 伙计拥有的现金
        /// </summary>
        public int Cash
        {
            get;
            private set;
        }

        /// <summary>
        /// 构造方法，用来设置名称，年龄和现金
        /// </summary>
        /// <param name="name">伙计的名称</param>
        /// <param name="age">伙计的年龄</param>
        /// <param name="cash">伙计最初拥有的现金</param>
        public Guy(string name, int age, int cash)
        {
            this.name = name;
            this.age = age;
            Cash = cash;
        }

        public override string ToString()
        {
            return String.Format("{0} 今年 {1} 岁，而且拥有 {2} 元现金", Name, Age, Cash);
        }

        /// <summary>
        /// 从伙计的钱包中捐钱
        /// </summary>
        /// <param name="amount">打算捐的现金数目</param>
        /// <returns>实际捐出的现金的数目，如果钱包中的钱不够就返回 0</returns>
        public int GiveCash(int amount)
        {
            if (amount <= Cash && amount > 0)
            {
                Cash -= amount;
                return amount;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 向钱包中存入一些现金
        /// </summary>
        /// <param name="amount">打算存入的现金的数目</param>
        /// <returns>实际存入的现金的数目，如果没有存入现金就返回 0</returns>
        public int ReceiveCash(int amount)
        {
            if (amount > 0)
            {
                if (amount > 0)
                {
                    Cash += amount;
                    return amount;
                }
                Console.WriteLine("{0} 说：{1} 不是我将拿走的数目", Name, amount);
            }
            return 0;
        }
    }
}
