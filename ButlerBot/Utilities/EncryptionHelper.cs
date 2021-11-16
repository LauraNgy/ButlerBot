using System;
using System.Configuration;

namespace ButlerBot.Utilities
{
    public static class EncryptionHelper
    {
        public static void ProtectCustomConfig(string sectionName)
        {
            string applicationName = Environment.GetCommandLineArgs()[0];

            string exePath = System.IO.Path.Combine(Environment.CurrentDirectory, applicationName);

            // Get the configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            ConfigurationSection section = config.GetSection(sectionName);

            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
            }
        }
    }
}
