namespace TNCSC.Hulling.Domain
{
    /// <summary>
    /// AuthResult
    /// </summary>
    public class AuthResult
    {
        #region Properties
        /// <summary>
        /// status
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// user
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// errors
        /// </summary>
        public IEnumerable<string> errors { get; set; }
        #endregion
    }
}
