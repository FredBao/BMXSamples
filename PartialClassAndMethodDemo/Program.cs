namespace PartialClassAndMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Employee employee = new Employee();
            employee.Id = 1;
            employee.Name = string.Empty;
            employee.Address = string.Empty;
            employee.Print();

            
        }
    }
}