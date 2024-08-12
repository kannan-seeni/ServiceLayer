namespace TNCSC.Hulling.Domain.Paddy
{
    public class PaddyFinalReport
    {
       public paddyMonthReport ADTMonthReport { get; set; }
       public paddyMonthReport CRMonthReport { get; set; }
    }


    public class paddyMonthReport
    {
        public List<PaddyReport> ReportPerDay { get; set; }
        public ReportData GrandTotal { get; set; }
    }
}
