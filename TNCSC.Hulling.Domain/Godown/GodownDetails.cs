namespace TNCSC.Hulling.Domain.Godown
{
    /// <summary>
    /// GodownDetails
    /// </summary>
    public class GodownDetails
    {
        #region Properties
        public long Id { get; set; }
        public string GodownId { get; set; }
        public string GodownName { get; set; } 
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
