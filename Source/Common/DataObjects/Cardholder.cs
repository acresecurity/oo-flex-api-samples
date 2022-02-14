namespace Common.DataObjects
{
    /// <summary>
    /// An individual cardholder record
    /// </summary>
    public partial class Cardholder
    {
        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual string City { get; set; }

        public virtual int? Company { get; set; }

        public virtual string CompanyName { get; set; }

        public virtual string Country { get; set; }

        public virtual DateTime? Created { get; set; }

        public virtual int? CustomNumber1 { get; set; }

        public virtual int? CustomNumber2 { get; set; }

        public virtual int? CustomNumber3 { get; set; }

        public virtual string CustomText1 { get; set; }

        public virtual string CustomText10 { get; set; }

        public virtual string CustomText11 { get; set; }

        public virtual string CustomText12 { get; set; }

        public virtual string CustomText13 { get; set; }

        public virtual string CustomText14 { get; set; }

        public virtual string CustomText15 { get; set; }

        public virtual string CustomText16 { get; set; }

        public virtual string CustomText2 { get; set; }

        public virtual string CustomText3 { get; set; }

        public virtual string CustomText4 { get; set; }

        public virtual string CustomText5 { get; set; }

        public virtual string CustomText6 { get; set; }

        public virtual string CustomText7 { get; set; }

        public virtual string CustomText8 { get; set; }

        public virtual string CustomText9 { get; set; }

        public virtual string Department { get; set; }

        public virtual string DriversLicenseNumber { get; set; }

        public virtual string Email { get; set; }

        public virtual string EmployeeId { get; set; }

        public virtual int? EmployeeNumber { get; set; }

        public virtual string FirstName { get; set; }

        public virtual DateTime? HireDate { get; set; }

        public virtual string HomePhone { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Location { get; set; }

        public virtual string MiddleName { get; set; }

        public virtual string Other { get; set; }

        public virtual int? PersonnelType { get; set; }

        public virtual string ReasonForVisit { get; set; }

        public virtual string Site { get; set; }

        public virtual int? Sponsor { get; set; }

        public virtual string State { get; set; }

        public virtual int? TenantId { get; set; }

        public virtual string Title { get; set; }

        /// <summary>
        /// Optional field
        /// </summary>
        public virtual string Token { get; set; }

        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        public virtual Guid? UniqueKey { get; set; }

        public virtual DateTime? Updated { get; set; }

        public virtual string WorkPhone { get; set; }

        public virtual string Zip { get; set; }

        public virtual int? UserId { get; set; }
    }
}