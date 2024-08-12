namespace TNCSC.Hulling.Components
{
    #region AuditType
    public enum AuditType
    {
        Entry = 0,
        Exit = 1,
        Get = 2,
        Update = 3,
        Delete = 4,
        Others = 5
    }
    #endregion

    #region LogType
    public enum LogType
    {
        Success,
        Failed,
        Error,
        Info,
        Warning,
        IPSettings,
    }
    #endregion

}
