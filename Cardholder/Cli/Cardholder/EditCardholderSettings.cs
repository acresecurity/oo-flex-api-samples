using System.Text;
using FluentValidation;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Cardholder
{
    public partial class EditCardholderSettings : CardholderSettings
    {
        private readonly IValidator<EditCardholderSettings> _validation;

        public EditCardholderSettings(IValidator<EditCardholderSettings> validation)
        {
            _validation = validation;
        }

        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }

        #region Overrides of CommandSettings

        public override ValidationResult Validate()
        {
            var result = _validation.Validate(this);
            if (result.IsValid)
                return ValidationResult.Success();

            var sb = new StringBuilder();
            foreach (var item in result.Errors)
                sb.AppendLine($"[yellow]{item.PropertyName}[/]: {item.ErrorMessage}");

            return ValidationResult.Error(sb.ToString());
        }

        #endregion

        [CommandOption($"--{nameof(Address1)}")]
        public virtual string Address1 { get; set; }

        [CommandOption($"--{nameof(Address2)}")]
        public virtual string Address2 { get; set; }

        [CommandOption($"--{nameof(City)}")]
        public virtual string City { get; set; }

        [CommandOption($"--{nameof(Company)}")]
        public virtual int? Company { get; set; }

        [CommandOption($"--{nameof(CompanyName)}")]
        public virtual string CompanyName { get; set; }

        [CommandOption($"--{nameof(Country)}")]
        public virtual string Country { get; set; }

        [CommandOption($"--{nameof(Created)}")]
        public virtual DateTime? Created { get; set; }

        [CommandOption($"--{nameof(CustomNumber1)}")]
        public virtual int? CustomNumber1 { get; set; }

        [CommandOption($"--{nameof(CustomNumber2)}")]
        public virtual int? CustomNumber2 { get; set; }

        [CommandOption($"--{nameof(CustomNumber3)}")]
        public virtual int? CustomNumber3 { get; set; }

        [CommandOption($"--{nameof(CustomText1)}")]
        public virtual string CustomText1 { get; set; }

        [CommandOption($"--{nameof(CustomText10)}")]
        public virtual string CustomText10 { get; set; }

        [CommandOption($"--{nameof(CustomText11)}")]
        public virtual string CustomText11 { get; set; }

        [CommandOption($"--{nameof(CustomText12)}")]
        public virtual string CustomText12 { get; set; }

        [CommandOption($"--{nameof(CustomText13)}")]
        public virtual string CustomText13 { get; set; }

        [CommandOption($"--{nameof(CustomText14)}")]
        public virtual string CustomText14 { get; set; }

        [CommandOption($"--{nameof(CustomText15)}")]
        public virtual string CustomText15 { get; set; }

        [CommandOption($"--{nameof(CustomText16)}")]
        public virtual string CustomText16 { get; set; }

        [CommandOption($"--{nameof(CustomText2)}")]
        public virtual string CustomText2 { get; set; }

        [CommandOption($"--{nameof(CustomText3)}")]
        public virtual string CustomText3 { get; set; }

        [CommandOption($"--{nameof(CustomText4)}")]
        public virtual string CustomText4 { get; set; }

        [CommandOption($"--{nameof(CustomText5)}")]
        public virtual string CustomText5 { get; set; }

        [CommandOption($"--{nameof(CustomText6)}")]
        public virtual string CustomText6 { get; set; }

        [CommandOption($"--{nameof(CustomText7)}")]
        public virtual string CustomText7 { get; set; }

        [CommandOption($"--{nameof(CustomText8)}")]
        public virtual string CustomText8 { get; set; }

        [CommandOption($"--{nameof(CustomText9)}")]
        public virtual string CustomText9 { get; set; }

        [CommandOption($"--{nameof(Department)}")]
        public virtual string Department { get; set; }

        [CommandOption($"--{nameof(DriversLicenseNumber)}")]
        public virtual string DriversLicenseNumber { get; set; }

        [CommandOption($"--{nameof(Email)}")]
        public virtual string Email { get; set; }

        [CommandOption($"--{nameof(EmployeeId)}")]
        public virtual string EmployeeId { get; set; }

        [CommandOption($"--{nameof(EmployeeNumber)}")]
        public virtual int? EmployeeNumber { get; set; }

        [CommandOption($"--{nameof(FirstName)}")]
        public virtual string FirstName { get; set; }

        [CommandOption($"--{nameof(HireDate)}")]
        public virtual DateTime? HireDate { get; set; }

        [CommandOption($"--{nameof(HomePhone)}")]
        public virtual string HomePhone { get; set; }

        [CommandOption($"--{nameof(LastName)}")]
        public virtual string LastName { get; set; }

        [CommandOption($"--{nameof(Location)}")]
        public virtual string Location { get; set; }

        [CommandOption($"--{nameof(MiddleName)}")]
        public virtual string MiddleName { get; set; }

        [CommandOption($"--{nameof(Other)}")]
        public virtual string Other { get; set; }

        [CommandOption($"--{nameof(PersonnelType)}")]
        public virtual int? PersonnelType { get; set; }

        [CommandOption($"--{nameof(ReasonForVisit)}")]
        public virtual string ReasonForVisit { get; set; }

        [CommandOption($"--{nameof(Site)}")]
        public virtual string Site { get; set; }

        [CommandOption($"--{nameof(Sponsor)}")]
        public virtual int? Sponsor { get; set; }

        [CommandOption($"--{nameof(State)}")]
        public virtual string State { get; set; }

        [CommandOption($"--{nameof(TenantId)}")]
        public virtual int? TenantId { get; set; }

        [CommandOption($"--{nameof(Title)}")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Optional field
        /// </summary>
        [CommandOption($"--{nameof(Token)}")]
        public virtual string Token { get; set; }

        [CommandOption($"--{nameof(Updated)}")]
        public virtual DateTime? Updated { get; set; }

        [CommandOption($"--{nameof(WorkPhone)}")]
        public virtual string WorkPhone { get; set; }

        [CommandOption($"--{nameof(Zip)}")]
        public virtual string Zip { get; set; }

        [CommandOption($"--{nameof(UserId)}")]
        public virtual int? UserId { get; set; }
    }
}