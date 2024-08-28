using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCSC.Hulling.Domain.Reports
{
    public class ReportBalance
    {
        public string ADNo { get; set; }
        public string ReportMonth { get; set; }
        public decimal ReportBalanceDue {  get; set; } 
        public bool IsToNextReport {  get; set; }
        public string IsReportedMonth{ get; set; }
    }
}
