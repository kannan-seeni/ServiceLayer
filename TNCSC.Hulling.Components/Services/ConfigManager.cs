#region References
using Dapper;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Options;
#endregion

namespace TNCSC.Hulling.Components.Services
{
    /// <summary>
    /// ConfigManager
    /// </summary>
    public class ConfigManager : IConfigManager
    {
        #region Declarations
        /// <summary>
        /// ConfigManagerOptions
        /// </summary>
        private ConfigManagerOptions configManagerOptions;
        /// <summary>
        /// ConfigurationData
        /// </summary>
        private Dictionary<string, string> ConfigurationData { get; set; }
        #endregion

        #region ConfigManager Constructor
        /// <summary>
        /// ConfigManager Constructor
        /// </summary>
        /// <param name="options"></param>
        public ConfigManager(IOptions<ConfigManagerOptions> options)
        {

            configManagerOptions = options.Value;
            LoadConfigData(configManagerOptions.ConnectionString, configManagerOptions.TableName);
        }
        #endregion

        #region LoadConfigData
        /// <summary>
        /// LoadConfigData
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        public void LoadConfigData(string connectionString, string tableName)
        {
            ConfigurationData = GetConfigData(connectionString, tableName);
        }
        #endregion

        #region GetConfigData
        /// <summary>
        /// GetConfigData
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetConfigData(string connectionString, string tableName)
        {
            string tableQuery = "SELECT [SectionName], [Key], [Value] FROM" + " " + tableName;
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    ConfigurationData = con.Query<(string SectionName, string Key, string Value)>(tableQuery).ToDictionary(t => string.Concat(t.SectionName, ".", t.Key), t => t.Value);
                    return ConfigurationData;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetConfig
        /// <summary>
        /// GetConfig
        /// </summary>
        /// <param name="configurationKey"></param>
        /// <returns></returns>
        public string GetConfig(string configurationKey)
        {
            string configValue;
            if (ConfigurationData.ContainsKey(configurationKey))
            {
                configValue = ConfigurationData[configurationKey];
            }
            else
            {
                configValue = string.Empty;
            }

            return configValue;
        }
        #endregion
    }
}
