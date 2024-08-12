namespace TNCSC.Hulling.Domain.Godwon
{
    /// <summary>
    /// GodwonDetails
    /// </summary>
    public class GodwonDetails
    {
        #region Properties
        public long Id { get; set; }
        public string GodwonId { get; set; }
        public string GodwonName { get; set; }
        public string MillName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string GSTNo { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public int Distance { get; set; }
        public string AQName { get; set; }
        public string Superintendent { get; set; }
        public string UserName { get; set; }
        #endregion
    }
}
