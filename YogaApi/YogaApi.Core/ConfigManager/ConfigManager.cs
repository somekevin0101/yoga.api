using System;
using System.Configuration;

namespace YogaApi.Core.ConfigManager
{
    public class ConfigManager : IConfigManager
    {
        public string GetConfigValue(string configName)
        {
            return ConfigurationManager.ConnectionStrings[configName].ConnectionString;
        }
    }
}
