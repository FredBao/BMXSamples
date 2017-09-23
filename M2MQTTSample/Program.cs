namespace M2MQTTSample
{
    using System;
    using System.Net;

    using uPLibrary.Networking.M2Mqtt;
    using uPLibrary.Networking.M2Mqtt.Messages;

    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var address = System.Configuration.ConfigurationManager.AppSettings["Address"];
                var port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"]);
                var username = System.Configuration.ConfigurationManager.AppSettings["Username"];
                var password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                Console.WriteLine("Hello, World!");

                // Create Client instance
                var client = new MqttClient(address, port, false, null);

                // Register to message received
                client.MqttMsgPublishReceived += MyClient_MqttMsgPublishReceived;

                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId, username, password);

                // Subscribe to topic
                var topics = new string[] { "M", "Map" };

                client.Subscribe(topics, new[] { (byte)3, (byte)3 });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }

        private static void MyClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // Handle message received
            var message = System.Text.Encoding.Default.GetString(e.Message);

            Console.WriteLine("Message received: " + message);
        }
    }
}
