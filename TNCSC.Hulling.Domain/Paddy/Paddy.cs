﻿namespace TNCSC.Hulling.Domain.Paddy
{
    /// <summary>
    /// Paddy
    /// </summary>
    public class Paddy
    {
        #region References
        public long Id { get; set; }
        public long UserRefId { get; set; }
        public long MillRefId { get; set; }
        public long GodwonRefId { get; set; }
        public DateTime PaddyReceivedDate { get; set; } 
        public string Month { get; set; }
        public string KMS { get; set; }
        public string IssueMemoNo { get; set; }
        public string Variety { get; set; }
        public Decimal MoitureContent { get; set; }
        public int NoOfBags { get; set; }
        public Decimal WeightOfPaddy { get; set; }
        public string LorryNo { get; set; }
        public int NoOfNBBags { get; set; }
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
