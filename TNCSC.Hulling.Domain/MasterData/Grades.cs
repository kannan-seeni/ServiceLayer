namespace TNCSC.Hulling.Domain.MasterData
{
    public class Grades
    {

        public int Id { get; set; }
        public string Grade { get; set; }
        public List<Variety> Variety { get; set; }
        public bool Status { get; set; }

    }

    public class Variety
    {
        public int Id { get; set; }
        public string VarietyName { get; set; }
        public int GradeId { get; set; }
        public bool Status { get; set; }
    }

}
