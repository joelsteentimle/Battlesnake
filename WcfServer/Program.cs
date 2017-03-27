using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace WcfServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebServiceHost(typeof(SnakeService), new Uri("http://0.0.0.0:80/"));
            var ep = host.AddServiceEndpoint(typeof(ISnakeService), new WebHttpBinding(), "");            

            host.Open();
            Console.WriteLine("Service is running");
            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();
            host.Close();
        }
    }
}
