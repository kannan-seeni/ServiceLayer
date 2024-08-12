namespace TNCSC.Hulling.Domain
{
    /// <summary>
    /// ErrorModel
    /// </summary>
    public class ErrorModel
    {
        #region Properties
        public string Message { get; set; }
 
        public int ErrorCode { get; set; }
        #endregion

        #region Constructor
        public ErrorModel(string message, int errorCode)
        {
            this.Message = message;
            this.ErrorCode = errorCode;

        }
        #endregion
    }
}
