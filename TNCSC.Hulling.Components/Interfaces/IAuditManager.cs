#region References
using TNCSC.Hulling.Components.Models;
#endregion

namespace TNCSC.Hulling.Components.Interfaces
{
    /// <summary>
    /// IAuditManager
    /// </summary>
    public interface IAuditManager
    {
        void LoadAuditConfigData(string connectionString);
        
        bool Log(AuditModel auditModel);

        void ReLoadAuditConfigData();
    }
}


