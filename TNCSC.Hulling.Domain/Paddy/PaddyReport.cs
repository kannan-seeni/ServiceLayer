using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCSC.Hulling.Domain.Paddy
{
    public class PaddyReport
    {
        public List<PaddyReportDetails> Details { get; set; }
        public ReportData Total { get; set; }
    }
}
