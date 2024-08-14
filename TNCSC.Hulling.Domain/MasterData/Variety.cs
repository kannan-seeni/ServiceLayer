namespace TNCSC.Hulling.Domain.MasterData
{
    public class Variety
    {
        public int Id { get; set; }
        public string VarietyName { get; set; }
        public List<Grades> Grade {  get; set; }
        public bool Status { get; set; }
    }

    public class Grades
    {
        public int Id { get; set; }
        public string Grade { get; set; }
        public int VarietyId { get; set; }
        public bool Status { get; set; }
    }

}
