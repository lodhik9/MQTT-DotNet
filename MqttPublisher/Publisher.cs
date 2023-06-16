using MQTTnet.Client;
using MQTTnet;
using System;
using System.Threading.Tasks;
using System.Text;

namespace MQTTSubscriber
{
    class Publisher
    {
        static async Task Main(string[] args)
        {
            /*
             * This sample creates a simple MQTT client and connects to a public broker with enabled TLS encryption.
             * 
             * This is a modified version of the sample _Connect_Client_! See other sample for more details.
             */

            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("test.mosquitto.org", 1883)
                    //.WithTls(
                    //    o =>
                    //    {
                    //        // The used public broker sometimes has invalid certificates. This sample accepts all
                    //        // certificates. This should not be used in live environments.
                    //        o.CertificateValidationHandler = _ => true;
                    //    })
                    .Build();

                // In MQTTv5 the response contains much more information.
                using (var timeout = new CancellationTokenSource(5000))
                {
                    // Establish connection using ConnectAsync
                    var response = await mqttClient.ConnectAsync(mqttClientOptions);

                    // Publishing this msg to the broker
                    Console.WriteLine("The MQTT client is connected.");

                    Console.ReadLine();

                    // Publish the message
                    await PublishMessageAsync(mqttClient);

                    // Disconnect the client
                    await mqttClient.DisconnectAsync();

                    Console.WriteLine("MQTT application message is published.");
                }
            }
        }
        public static async Task PublishMessageAsync(IMqttClient client)
        {
            string messagePayload = "hello";
            // Build the message
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("/Lodhi")
                .WithPayload(messagePayload)
                .Build();

            // Check for publishing
            if (client.IsConnected)
            {
                await client.PublishAsync(message);
            }
        }

    }





}