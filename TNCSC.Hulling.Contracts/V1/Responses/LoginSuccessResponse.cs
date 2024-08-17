namespace TNCSC.Hulling.Contracts.V1.Responses
{

    /// <summary>
    /// LoginSuccessResponse
    /// </summary>
    public class LoginSuccessResponse
    {
        #region Properties

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string UserStatus { get; set; }

        public long ID { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string MillId { get; set; }

        public string Role { get; set; }

        public string Region { get; set; }
        #endregion


    }
}
