namespace InterfaceInheritance
{
    using System;
    using System.Reflection;

    public interface Tool
    {
        int Size { get; set; }
    }

    public interface SubTool : Tool
    {
        int Wight { get; set; }
    }

    public class Computer : SubTool
    {
        public int Size { get; set; }

        public string Type { get; set; }

        public int Wight { get; set; }
    }

    public class SubComputer : Computer
    {
        // public string SubType { get; set; }
    }

    class Program
    {
        public static void Print(Type type, object obj)
        {
            var properties = type.GetProperties();
            foreach (var item in properties)
            {
                Console.WriteLine("可访问到的Porperty为:" + item.Name + ",值为:" + item.GetValue(obj, null));
            }
        }

        static void Main(string[] args)
        {
            // Tool tool = new SubComputer() { Size = 1, SubType = "SubType", Type = "Type", Wight = 1 };
            // Print(tool.GetType(), tool);
            // Console.WriteLine("====================================================================");
            // var subTool = (SubTool)tool;
            // Print(subTool.GetType(), subTool);
            // Console.WriteLine("====================================================================");
            // var computer = (Computer)tool;
            // Print(computer.GetType(), computer);
            // Console.WriteLine("====================================================================");
            // var subComputer = (SubComputer)tool;
            // Print(subComputer.GetType(), subComputer);
            // Console.WriteLine("====================================================================");

            // Computer tool = new Computer() { };
            // Print(tool.GetType(), tool);
            // Console.WriteLine("====================================================================");
            // var subTool = (SubTool)tool;
            // Print(subTool.GetType(), subTool);
            // Console.WriteLine("====================================================================");
            // var computer = (Computer)tool;
            // Print(computer.GetType(), computer);
            // Console.WriteLine("====================================================================");
            // var subComputer = (SubComputer)tool;
            // Print(subComputer.GetType(), subComputer);
            // Console.WriteLine("====================================================================");
            Console.WriteLine(Assembly.GetExecutingAssembly());
        }
    }
}