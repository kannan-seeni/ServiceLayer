namespace TNCSC.Hulling.Components.Interfaces
{
    /// <summary>
    /// IConfigManager
    /// </summary>
    public interface IConfigManager
    {
        #region LoadConfigData
        /// <summary>
        /// LoadConfigData
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        void LoadConfigData(string connectionString, string tableName);
        #endregion

        #region GetConfig
        /// <summary>
        /// GetConfig
        /// </summary>
        /// <param name="configurationKey"></param>
        /// <returns></returns>
        string GetConfig(string configurationKey);
        #endregion
    }
}
