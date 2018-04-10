using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract(Name = "CalculatorService", Namespace = "http://www.artech.com/")]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double x, double y);

        [OperationContract]
        double Subtract(double x, double y);

        [OperationContract]
        double Multiply(double x, double y);

        [OperationContract]
        double Divide(double x, double y);

        [OperationContract]
        string GetName(getResultNGRequest request);
    }

    [DataContract]
    public class getResultNGRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}