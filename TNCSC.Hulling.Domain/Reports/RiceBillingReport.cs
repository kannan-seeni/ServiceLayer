namespace TNCSC.Hulling.Domain.Reports
{
    public class RiceBillingReport
    {
        public int RowNumber { get; set; }
        public string ADNumber { get; set; }
        public DateTime ADDate { get; set; }
        public decimal RiceWeight { get; set; }
        public decimal TotalRiceWeight { get; set; }
        public bool IsToNextReport { get; set; }
        public string ReportMonth { get; set; }
    }
}
