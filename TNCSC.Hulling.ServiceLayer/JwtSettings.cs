namespace TNCSC.Hulling.ServiceLayer
{
    /// <summary>
    /// JwtSettings
    /// </summary>
    public class JwtSettings
    {
        #region Properties
        public string Secret { get; set; }
        public TimeSpan TokenLifeTime { get; set; }

        #endregion
    }
}
