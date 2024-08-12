#region References
using System.Runtime.CompilerServices;
using TNCSC.Hulling.Components.Models;
#endregion

namespace TNCSC.Hulling.Components.Interfaces
{
    /// <summary>
    /// ILogger
    /// </summary>
    public interface ILogger
    {
        void Log(LogType errorType, LoggerModel ErrorLogModel, [CallerMemberName] string methodName = "",
                                      [CallerFilePath] string fileName = "");
        void ReLoadLoggerConfigData();
    }
}
