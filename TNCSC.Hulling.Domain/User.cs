namespace TNCSC.Hulling.Domain
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        #region Properties
        public long ID { get; set; }
        public string UserId { get; set; }
        [SwaggerIgnore]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long MillRefId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int Role { get; set; }
        public string Region { get; set; }
        [SwaggerIgnore]
        public long CreatedBy { get; set; }
        [SwaggerIgnore]
        public long  ModifiedBy { get; set; }
        public bool Status { get; set; }
        #endregion

    }


}
