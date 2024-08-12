namespace TNCSC.Hulling.Domain.Mill
{
    public class Mill
    {

        #region Properties
        public long ID { get; set; }
        public string OwnerName { get; set; }
        public string MillName { get; set; }
        public string MillCode { get; set; }
        public string MillId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string GSTNo { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        [SwaggerIgnore]
        public long CreatedBy { get; set; }
        [SwaggerIgnore]
        public long ModifiedBy { get; set; }
        public bool Status { get; set; }
        #endregion
    }
}
