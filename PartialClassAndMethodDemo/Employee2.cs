namespace PartialClassAndMethodDemo
{
    using System;

    partial class Employee
    {
        public string Address { get; set; }

        public void Print()
        {
            this.PrintEmployeeInfo();
        }

        partial void PrintEmployeeInfo()
        {
            Console.WriteLine("Now In Employee2.cs.PrintEmployeeInfo");
        }
    }
}