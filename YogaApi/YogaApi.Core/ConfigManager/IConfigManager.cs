using System;

namespace YogaApi.Core.ConfigManager
{
    public interface IConfigManager
    {
        string GetConfigValue(string configName);
    }
}
