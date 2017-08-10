namespace FactoryDesignPatternDemo
{
    using System;

    /// <summary>
    /// 一般静态工厂方法
    /// </summary>
    public class EmployeeFactory
    {
        public static Employee CreateEmployee(string name, string address)
        {
            if (name.StartsWith("Vip"))
            {
                return new Employee() { Name = name.Replace("Vip", string.Empty), Address = address };
            }
            else
            {
                return new Employee() { Name = name, Address = address };
            }
        }
    }

    /// <summary>
    /// 工厂注入规则
    /// </summary>
    public class EmployeeFactory2
    {
        public static Employee CreateEmployee(
            string name,
            string address,
            Func<string, bool> logic,
            Func<string, string> filterName)
        {
            if (logic(name))
            {
                return new Employee() { Name = filterName(name), Address = address };
            }
            else
            {
                return new Employee() { Name = name, Address = address };
            }
        }
    }

    /// <summary>
    /// 委托工厂模式
    /// </summary>
    public class EmployeeFactory3
    {
        public static Employee CreateEmployee(
            string name,
            string addressString, // may be like "上海,苏州"
            Func<string, EmployeeAddress> addressFactory)
        {
            return new Employee() { Name = name, EmployeeAddress = addressFactory(addressString) };
        }
    }
}