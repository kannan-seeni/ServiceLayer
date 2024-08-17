using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNCSC.Hulling.Domain.Paddy
{
    public class PaddyReportDetails
    {
        public long Id { get; set; }  
        public DateTime Date { get; set; }
        public string Godown { get; set; } 
        public string IssueMemoNo { get; set; }
        public string LorryNo { get; set; }
        public string Variety { get; set; }
        public int Bags { get; set; }
        public Decimal Weight { get; set; } 
        public int NB { get; set; }
        public int ONB { get; set; }
        public int SS { get; set; }
        
    }
}
