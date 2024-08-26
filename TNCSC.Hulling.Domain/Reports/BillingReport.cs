namespace TNCSC.Hulling.Domain.Reports
{
    public class BillingReport
    {
        public int Id {  get; set; }
        public int RowNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal PaddyWeight { get; set; }
        public decimal TotalPaddyWeight { get; set; }
        public decimal OutTurn { get; set; }
        public DateTime DueDate { get; set; }
        public string ADNumber { get; set; }
        public DateTime ADDate { get; set; }
        public decimal RiceWeight { get; set; }
        public decimal TotalRiceWeight { get; set; }
         

    }
}
