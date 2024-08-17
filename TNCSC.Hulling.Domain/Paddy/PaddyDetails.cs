namespace TNCSC.Hulling.Domain.Paddy
{
    /// <summary>
    /// PaddyDetails
    /// </summary>
    public class PaddyDetails
    {
        #region Properties
        public long Id { get; set; }
        public string MillId { get; set; }
        public string MillName { get; set; }
        public string Godown { get; set; }
        public DateTime Date { get; set; }
        public string Month { get; set; }
        public string KMS { get; set; }
        public string IssueMemoNo { get; set; }
        public string Variety { get; set; }
        public Decimal MoitureContent { get; set; }
        public int NoOfBags { get; set; }
        public Decimal Weight { get; set; }
        public string LorryNo { get; set; }
        public int NoOfNBBags { get; set; }
        public int NoOfONBBags { get; set; }
        public int NoOfSSBags { get; set; }
        public int NoOfSWPBags { get; set; }
        public string Transport { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }

       // public ReportData reportData { get; set; }
        #endregion
    }
}
