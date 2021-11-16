using System;
using System.Collections.Specialized;
using System.Configuration;

namespace ButlerBot.Utilities
{
    public static class AppConfigHelper
    {
        public static string GetSetting(string sectionName, string configName, string defaultValue = "")
        {
            var sectionConfig = (NameValueCollection)ConfigurationManager.GetSection(sectionName);
            var result = sectionConfig?.Get(configName);
            return !string.IsNullOrWhiteSpace(result) ? result : defaultValue;
        }

        public static T GetSetting<T>(string sectionName, string configName)
        {
            var configValue = GetSetting(sectionName, configName);

            if (typeof(T) == typeof(bool))
            {
                return (T)(object)bool.Parse(configValue);
            }
            
            if (typeof(T) == typeof(int))
            {
                return (T)(object)int.Parse(configValue);
            }
            
            if (typeof(T) == typeof(string))
            {
                return (T)(object)configValue.ToString();
            }

            throw new ArgumentException($"Error getting config setting: parsing not implemented for type {typeof(T)}");
        }
    }
}
