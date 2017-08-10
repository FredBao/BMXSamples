namespace PassivationPatternDemo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.Read().ToString();
            switch (input)
            {
                case "49":
                    Order order = new Order()
                                      {
                                          OrderId = "1",
                                          Customer =
                                              new Customer()
                                                  {
                                                      Name = "12345",
                                                      Phone = "12345678978",
                                                      Email = "xx@xx.com"
                                                  },
                                          OrderItems =
                                              new List<OrderItem>()
                                                  {
                                                      new OrderItem()
                                                          {
                                                              Number = 2,
                                                              Product =
                                                                  new Product()
                                                                      {
                                                                          Name
                                                                              = "1",
                                                                          Price
                                                                              = 10
                                                                      }
                                                          },
                                                      new OrderItem()
                                                          {
                                                              Number = 2,
                                                              Product =
                                                                  new Product()
                                                                      {
                                                                          Name
                                                                              = "2",
                                                                          Price
                                                                              = 10
                                                                      }
                                                          }
                                                  }
                                      };
                    OrderExamineApproveManager manager = OrderExamineApproveManager.CreateFlow();
                    manager.RunFlows(order);
                    Stream stream = File.Open("OrderCheck.xml", FileMode.Create);
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, manager);
                    stream.Close();
                    stream.Dispose();
                    break;
                case "50":
                    Order order2 = new Order()
                                       {
                                           OrderId = "1",
                                           Customer =
                                               new Customer()
                                                   {
                                                       Name = "12345",
                                                       Phone = "12345678978",
                                                       Email = "xx@xx.com"
                                                   },
                                           OrderItems =
                                               new List<OrderItem>()
                                                   {
                                                       new OrderItem()
                                                           {
                                                               Number = 2,
                                                               Product =
                                                                   new Product()
                                                                       {
                                                                           Name
                                                                               = "1",
                                                                           Price
                                                                               = 10
                                                                       }
                                                           },
                                                       new OrderItem()
                                                           {
                                                               Number = 2,
                                                               Product =
                                                                   new Product()
                                                                       {
                                                                           Name
                                                                               = "2",
                                                                           Price
                                                                               = 10
                                                                       }
                                                           }
                                                   }
                                       };
                    Stream stream2 = File.Open("OrderCheck.xml", FileMode.Open);
                    BinaryFormatter formatter2 = new BinaryFormatter();
                    OrderExamineApproveManager manager2 = (OrderExamineApproveManager)formatter2.Deserialize(stream2);
                    manager2.RunFlows(order2);
                    stream2.Close();
                    stream2.Dispose();

                    Stream stream3 = File.Open("OrderCheck.xml", FileMode.Create);
                    BinaryFormatter formatter3 = new BinaryFormatter();
                    formatter3.Serialize(stream3, manager2);
                    stream3.Close();
                    stream3.Dispose();
                    break;
                default: break;
            }
            Console.ReadKey();
        }
    }
}