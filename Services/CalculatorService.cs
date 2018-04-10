using Contracts;
using System.Security.Permissions;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CalculatorService : ICalculator
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "Administrators")]
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Subtract(double x, double y)
        {
            return x - y;
        }

        public double Multiply(double x, double y)
        {
            return x * y;
        }

        public double Divide(double x, double y)
        {
            return x / y;
        }

        public string GetName(getResultNGRequest request)
        {
            return request.Name;
        }
    }
}
