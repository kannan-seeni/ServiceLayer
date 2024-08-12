namespace TNCSC.Hulling.Domain
{
    /// <summary>
    /// APIResponse
    /// </summary>
    public class APIResponse
    {
        #region Properties

        /// <summary>
        /// version
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// responseCode
        /// </summary>
        public int responseCode { get; set; }

        /// <summary>
        /// error
        /// </summary>
        public ErrorModel error { get; set; }

        /// <summary>
        /// data
        /// </summary>
        public dynamic? data { get; set; }
        #endregion
    }
}
