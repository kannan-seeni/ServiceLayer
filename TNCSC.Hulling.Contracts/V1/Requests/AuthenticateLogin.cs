namespace TNCSC.Hulling.Contracts.V1.Requests
{
    /// <summary>
    /// AuthenticateLogin
    /// </summary>
    public class AuthenticateLogin
    {
        #region Properties

        public string UserId { get; set; }

        public string Password { get; set; }
        #endregion
    }
}
