namespace PartialClassAndMethodDemo
{
    partial class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        partial void PrintEmployeeInfo();
    }
}