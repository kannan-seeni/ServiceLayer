namespace TNCSC.Hulling.Domain.FRK
{
    /// <summary>
    /// FRK
    /// </summary>
    public class FRK
    {
        #region Properties
        public long Id { get; set; }
        public long UserRefId { get; set; }
        public long MillRefId { get; set; }
        public long GodwonRefId { get; set; }
        public DateTime Date { get; set; }
        public string KMS { get; set; }
        public string ElnvNo { get; set; }
        public string IssueMemoNo { get; set; }
        public string CompanyName { get; set; }
        public int NoOfBags { get; set; }
        public decimal WeightOfFRK { get; set; }
        public string LorryNo { get; set; }
        public string Transport { get; set; }
        [SwaggerIgnore]
        public long CreatedBy { get; set; }
        [SwaggerIgnore]
        public long ModifiedBy { get; set; }
        public bool Status { get; set; }
        #endregion

    }
}
