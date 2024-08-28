using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNCSC.Hulling.Domain.Paddy;

namespace TNCSC.Hulling.Domain.Reports
{
    public class PaddyBillingReport
    {
        public int RowNumber { get; set; }
        public DateTime Date { get; set; }
        public string IssueMemoNo { get; set; }
        public decimal PaddyWeight { get; set; }
        public decimal TotalPaddyWeight { get; set; }
        public decimal OutTurn { get; set; }
        public DateTime DueDate { get; set; }
    }


    public class BillingPaddy
    {
        public int Id { get; set; }
        public List<PaddyReportForBill> Report { get; set; }
        public decimal TotalPaddyWeight { get; set; }
        public decimal OutTurn { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalRiceWeight { get; set; }
    }

    public class PaddyReportForBill
    {
        public int RowNumber { get; set; }
        public int RowId { get; set; }
        public DateTime Date { get; set; }
        public string IssueMemoNo { get; set; }
        public decimal PaddyWeight { get; set; }
        public string ADNumber { get; set; }
        public DateTime ADDate { get; set; }
        public decimal RiceWeight { get; set; }
        public decimal TotalWeight { get; set; }
        public bool IsToNextReport { get; set; }
        public string ReportMonth { get; set; }

    }
}
