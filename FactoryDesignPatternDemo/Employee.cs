namespace FactoryDesignPatternDemo
{
    public class Employee
    {
        public string Address { get; internal set; }

        public EmployeeAddress EmployeeAddress { get; set; }

        public string Name { get; internal set; }
    }

    public class EmployeeAddress
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }
    }
}