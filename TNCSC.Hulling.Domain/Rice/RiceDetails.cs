namespace TNCSC.Hulling.Domain.Rice
{
    /// <summary>
    /// RiceDetails
    /// </summary>
    public class RiceDetails
    {
        #region Properties
        public long Id { get; set; }
        public string UserName { get; set; }
        public string MillId { get; set; }
        public string Godwon { get; set; }
        public DateTime Date { get; set; }
        public string KMS { get; set; }
        public string TruckMemoNo { get; set; }
        public string Variety { get; set; }
        public int NoOfBags { get; set; }
        public decimal WeightOfRice { get; set; }
        public decimal WeightOfRiceWithFRK { get; set; }
        public decimal WeightOfFRK { get; set; }
        public string ADNo { get; set; }
        public DateTime ADDate { get; set; }
        public decimal QCMoitureContent { get; set; }
        public int QCNo { get; set; }
        public decimal QCDeHUsted { get; set; }
        public decimal QCFRK { get; set; }
        public string LorryNo { get; set; }
        public int NoOfONBBags { get; set; }
        public int NoOfSSBags { get; set; }
        public int NoOfSWPBags { get; set; }
        public bool IsFRKAdded { get; set; }
        public decimal FRK { get; set; }
        public string DepositMonth { get; set; }
        public string Transport { get; set; }
        [SwaggerIgnore]
        public long CreatedBy { get; set; }
        [SwaggerIgnore]
        public long ModifiedBy { get; set; }
        public bool Status { get; set; }
        #endregion

    }
}
