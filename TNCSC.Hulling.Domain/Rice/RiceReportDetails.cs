namespace TNCSC.Hulling.Domain.Rice
{
    public class RiceReportDetails
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Godown { get; set; }
        public string ADNo { get; set; }
        public string TruckNo { get; set; }
        public string LorryNo { get; set; } 
        public string Variety { get; set; }
        public int Bags { get; set; }
        public decimal Weight { get; set; }  
        public decimal MoitureContent { get; set; }
        public int QCNo { get; set; }
        public decimal DeHusked { get; set; }
        public decimal FRK { get; set; } 
        public int ONB { get; set; }
        public int SS { get; set; }
      
    }
}
