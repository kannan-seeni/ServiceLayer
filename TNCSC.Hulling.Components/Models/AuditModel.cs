namespace TNCSC.Hulling.Components.Models
{
    /// <summary>
    /// AuditModel
    /// </summary>
    public class AuditModel
    {
        #region Properties
        public int AuditID { get; set; }

        public string ControllerName { get; set; }

        public string UserID { get; set; }

        public string ActionName { get; set; }

        public string IPAddress { get; set; }

        public string SessionID { get; set; }

        public string URLReferrer { get; set; }

        public DateTime TimeAccessed { get; set; }

        public string Action { get; set; }

        public string UserName { get; set; }

        public string MethodName { get; set; }

        public string LoggedInAt { get; set; }

        public string LoggedOutAt { get; set; }

        public string LoginStatus { get; set; }

        public string PayLoad { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        #endregion
    }
}
