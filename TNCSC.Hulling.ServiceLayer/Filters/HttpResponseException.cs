namespace TNCSC.Hulling.ServiceLayer.Filters
{
    /// <summary>
    /// HttpResponseException
    /// </summary>
    public class HttpResponseException : Exception
    {
        #region Properties
        public int Status { get; set; } = 500;
        public object Value { get; set; }

        #endregion
    }
}
