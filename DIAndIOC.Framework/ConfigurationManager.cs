using Microsoft.Extensions.Configuration;
using System.IO;

namespace DIAndIOC.Framework
{
    public class ConfigurationManager
    {
        private static IConfigurationRoot _iConfiguration;

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _iConfiguration = builder.Build();
        }

        public static string GetNode(string nodeName)
        {
            return _iConfiguration[nodeName];
        }
    }
}
