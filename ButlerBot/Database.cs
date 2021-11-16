using ArangoDBNetStandard;
using ArangoDBNetStandard.Transport.Http;
using ButlerBot.Utilities;
using System;

namespace ButlerBot
{
    public class Database
    {
        private static readonly string _hostname = AppConfigHelper.GetSetting<string>("DbConfig","hostname");
        private static readonly int _port = AppConfigHelper.GetSetting<int>("DbConfig","port");
        private static readonly string _user = AppConfigHelper.GetSetting<string>("DbConfig","user");
        private static readonly string _password = AppConfigHelper.GetSetting<string>("DbConfig","password");
        private static readonly bool _useSSL = AppConfigHelper.GetSetting<bool>("DbConfig","useSSL");
        private static readonly string dbName = AppConfigHelper.GetSetting<string>("DbConfig","dbName");

        public ArangoDBClient Client { get; }

        public Database()
        {
            Uri arangoDbRoot = new Uri(
                   (_useSSL ? "https" : "http") + $"://{_hostname}:{_port}/");

            var transport = HttpApiTransport.UsingBasicAuth(
                arangoDbRoot,
                dbName,
                _user,
                _password);

            Client = new ArangoDBClient(transport);
        }
    }
}
