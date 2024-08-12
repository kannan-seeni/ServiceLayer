namespace TNCSC.Hulling.Domain.Godwon
{
    /// <summary>
    /// Godwon
    /// </summary>
    public class Godwon
    {
        #region Properties
        public long Id { get; set; }
        public string GodwonId { get; set; }
        public long MillRefId { get; set; }  
        public string GodwonName { get; set; }
        public Enums.GodwonType godwonType { get; set; }
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
        public long UserRefId { get; set; }
        #endregion

    }
}
