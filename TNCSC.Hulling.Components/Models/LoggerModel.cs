namespace TNCSC.Hulling.Components.Models
{
    /// <summary>
    /// LoggerModel
    /// </summary>
    public class LoggerModel
    {
        #region Properties
        public string UserID { get; set; }

        public LogType LogType { get; set; }

        public string AdditionalInfo { get; set; }

        public Exception ExecptionDetails { get; set; }

        public string StackTrace { get; set; }

        public int ResponseCode { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string RaisedBy { get; set; }

        public DateTime RaisedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        #endregion
    }
}
