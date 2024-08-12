#region References
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Options;
using TNCSC.Hulling.Components.Services;
#endregion

namespace TNCSC.Hulling.Components
{
    /// <summary>
    /// ConfigureServices
    /// </summary>
    public static class ConfigureServices
    {
        #region AddConfigManager
        /// <summary>
        /// AddConfigManager
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddConfigManager(this IServiceCollection services, string connectionString, string tableName)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<ConfigManagerOptions>(options => { options.ConnectionString = connectionString; options.TableName = tableName; });
            services.TryAddSingleton<IConfigManager, ConfigManager>();
            return services;
        }
        #endregion

        #region AddAuditAttribute

        /// <summary>
        /// AddLogger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddAuditAttribute(this IServiceCollection services, string connectionString, bool isAuditEnabled, bool isPayloadRecorded)
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<AuditAttributeOptions>(options =>
            {
                options.ConnectionString = connectionString;
                options.IsAuditEnabled = isAuditEnabled;
                options.IsPayloadRecorded = isPayloadRecorded;
            });

            services.TryAddSingleton<AuditAttribute>();

            return services;
        }

        #endregion AddLogger

        #region AddAuditManager

        /// <summary>
        /// AddLogger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddAuditManager(this IServiceCollection services, string connectionString, bool isAuditEnabled, bool isPayloadRecorded)
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<AuditAttributeOptions>(options =>
            {
                options.ConnectionString = connectionString;
                options.IsAuditEnabled = isAuditEnabled;
                options.IsPayloadRecorded = isPayloadRecorded;
            });

            services.TryAddSingleton<IAuditManager, AuditManager>();

            return services;
        }

        #endregion AddLogger

        #region AddLogger

        /// <summary>
        /// AddLogger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddLogger(this IServiceCollection services, string errorType, string targetType, string folderPath, string configConnectionstring)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<LoggerOptions>(options =>
            {
                options.ErrorType = errorType;
                options.TargetType = targetType;
                options.FolderPath = folderPath;
                options.ConfigConnectionstring = configConnectionstring;
            });

            services.TryAddSingleton<ILogger, Logger>();

            return services;
        }

        #endregion AddLogger
    }
}
