using DataEntry.Cli.Cardholder.Settings;
using FluentValidation;

namespace DataEntry.Validation
{
    internal class EditCardholderValidation : AbstractValidator<EditCardholderSettings>
    {
        public EditCardholderValidation()
        {
            RuleFor(p => p.Address1).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.Address2).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.City).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.Company).GreaterThan(0).UnlessIsNullOrEmpty();
            RuleFor(p => p.Country).Length(0, 100).WhenNotNull();
            RuleFor(p => p.Department).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.DriversLicenseNumber).Length(0, 25).UnlessIsNullOrEmpty();
            RuleFor(p => p.Email).Length(0, 100).EmailAddress().UnlessIsNullOrEmpty();
            RuleFor(p => p.EmployeeId).Length(0, 15).UnlessIsNullOrEmpty();
            RuleFor(p => p.EmployeeNumber).GreaterThanOrEqualTo(0).WhenNotNull();
            RuleFor(p => p.FirstName).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.HomePhone).Length(0, 20).UnlessIsNullOrEmpty();
            RuleFor(p => p.LastName).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.Location).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.MiddleName).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.Other).AnsiCharacters().UnlessIsNullOrEmpty();
            //RuleFor(p => p.CardholderType).WhenNotNull();
            RuleFor(p => p.ReasonForVisit).Length(0, 512).UnlessIsNullOrEmpty();
            RuleFor(p => p.Site).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.Sponsor).GreaterThan(0).WhenNotNull();
            RuleFor(p => p.State).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.TenantId).GreaterThan(0).UnlessIsNullOrEmpty();
            RuleFor(p => p.Title).Length(0, 100).UnlessIsNullOrEmpty();
            RuleFor(p => p.Token).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.WorkPhone).Length(0, 40).UnlessIsNullOrEmpty();
            RuleFor(p => p.Zip).Length(0, 20).UnlessIsNullOrEmpty();

            //RuleFor(p => p.CustomNumber1).GreaterThanOrEqualTo(int.MinValue).LessThanOrEqualTo(int.MaxValue);
            //RuleFor(p => p.CustomNumber2).GreaterThanOrEqualTo(int.MinValue).LessThanOrEqualTo(int.MaxValue);
            //RuleFor(p => p.CustomNumber3).GreaterThanOrEqualTo(int.MinValue).LessThanOrEqualTo(int.MaxValue);

            RuleFor(p => p.CustomText1).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText2).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText3).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText4).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText5).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText6).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText7).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText8).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText9).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText10).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText11).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText12).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText13).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText14).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText15).Length(0, 255).UnlessIsNullOrEmpty();
            RuleFor(p => p.CustomText16).Length(0, 255).UnlessIsNullOrEmpty();
        }
    }
}
