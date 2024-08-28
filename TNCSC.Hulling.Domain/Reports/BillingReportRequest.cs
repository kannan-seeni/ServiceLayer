using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCSC.Hulling.Domain.Reports
{
    public class BillingReportRequest
    {
        public string Reportmonth { get; set; }
        public string MonthFrom { get; set; }
        public string MonthTo { get; set; }
        public string Grade { get; set; }
        public long MillId { get; set; }
    }
}
