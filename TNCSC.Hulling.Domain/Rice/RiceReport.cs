namespace TNCSC.Hulling.Domain.Rice
{
    public class RiceReport
    {
        public List<RiceReportDetails> details { get; set; }
        public ReportData total {  get; set; }
        public List<GodownTotal> godownTotal { get; set; }

    }
}
