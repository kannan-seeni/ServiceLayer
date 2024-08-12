namespace TNCSC.Hulling.Domain.Gunnys
{
    /// <summary>
    /// Gunny
    /// </summary>
    public class Gunny
    {
        #region Properties
        public long Id { get; set; }
        public long UserRefId { get; set; }
        public long MillRefId { get; set; }
        public long GodwonRefId { get; set; }
        public DateTime Date { get; set; }
        public string KMS { get; set; }
        public string TruckMemoNo { get; set; }
        public string Godwon { get; set; }
        public int NoOfBags { get; set; }
        public string LorryNo { get; set; }
        public string ADNo { get; set; }
        public DateTime ADDate { get; set; }
        public int NoOfONBBags { get; set; }
        public int NoOfSSBags { get; set; }
        public int NoOfSWPBags { get; set; }
        public string Transport { get; set; }
        [SwaggerIgnore]
        public long CreatedBy { get; set; }
        [SwaggerIgnore]
        public long ModifiedBy { get; set; }
        public bool Status { get; set; }
        #endregion

    }
}

