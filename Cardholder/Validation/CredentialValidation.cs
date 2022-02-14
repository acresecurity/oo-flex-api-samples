using Cardholder.Cli.Credential;
using FluentValidation;

namespace Cardholder.Validation
{
    public class CredentialValidation : AbstractValidator<CredentialSettings>
    {
        public CredentialValidation()
        {
            RuleFor(p => p.Activation)
                .LessThan(p => p.Expiration)
                .When(p => p.Activation != null && p.Expiration != null)
                .WithMessage("Activation cannot be greater than expiration date.");

            RuleFor(p => p.FacilityCode)
                .GreaterThan(-1)
                .NotEmpty()
                .When(p => p.Mode != null && p.Mode.Value > 0)
                .WithMessage("Facility code is required when mode is not set to Auto");

            RuleFor(p => p.IssueCode)
                .GreaterThanOrEqualTo(-1)
                .LessThanOrEqualTo(255)
                .WhenNotNull();

            RuleFor(p => p.ApbLocation)
                .GreaterThanOrEqualTo(-1)
                .LessThanOrEqualTo(255)
                .WhenNotNull();

            RuleFor(p => p.VacationDuration)
                .GreaterThanOrEqualTo(-1)
                .WhenNotNull();

            RuleFor(p => p.UseLimit)
                .GreaterThanOrEqualTo(-1)
                .LessThanOrEqualTo(255)
                .WhenNotNull();

            RuleFor(p => p.TriggerCode1).GreaterThanOrEqualTo(0).WhenNotNull();
            RuleFor(p => p.TriggerCode2).GreaterThanOrEqualTo(0).WhenNotNull();
            RuleFor(p => p.TriggerCode3).GreaterThanOrEqualTo(0).WhenNotNull();
            RuleFor(p => p.TriggerCode4).GreaterThanOrEqualTo(0).WhenNotNull();
            RuleFor(p => p.TriggerCode5).GreaterThanOrEqualTo(0).WhenNotNull();
            RuleFor(p => p.TriggerCode6).GreaterThanOrEqualTo(0).WhenNotNull();
            RuleFor(p => p.TriggerCode7).GreaterThanOrEqualTo(0).WhenNotNull();

            /*
            RuleFor(p => p.PinCode)
                .MustAsync(async (credential, pinCode, context, token) =>
                {
                    if (settings == null || settings.Cardholder.AllowDuplicatePins == true)
                        return true;

                    var uniqueKey = InsertMode ? Guid.Empty : credential.UniqueKey.Value;
                    return await DoesExists(uniqueKey, null, null, pinCode);
                })
                .WithMessage("Duplicate PIN codes are not allowed.")
                .WhenNotNull();

            RuleFor(p => p.CardNumber)
                .MustAsync(async (credential, cardNumber, context, token) =>
                {
                    if (settings == null || settings.Cardholder.AllowDuplicateCardNumbers == true || credential.Mode > 0)
                        return true;

                    var uniqueKey = InsertMode ? Guid.Empty : credential.UniqueKey.Value;
                    return await DoesExists(uniqueKey, cardNumber, credential.FacilityCode, null);
                })
                .WithMessage("Duplicate card numbers are not allowed")
                .WhenNotNull();

            RuleFor(p => p.HotStamp)
                .MustAsync(async (credential, hotStamp, context, token) =>
                {
                    if (settings == null || settings.Cardholder.AllowDuplicateCardNumbers == true || credential.Mode == 0)
                        return true;

                    var uniqueKey = InsertMode ? Guid.Empty : credential.UniqueKey.Value;
                    return await DoesExists(uniqueKey, hotStamp, credential.FacilityCode, null);
                })
                .WithMessage("Duplicate card numbers are not allowed")
                .WhenNotNull();
            */
        }
        /*
        private async Task<bool> DoesExists(Guid uniqueKey, decimal? cardNumber, int? facilityCode, string pinCode)
        {
            var query = new Dictionary<string, string>();
            if (cardNumber.HasValue)
                query.Add(nameof(cardNumber), cardNumber.ToString());

            if (facilityCode.HasValue)
                query.Add(nameof(facilityCode), facilityCode.ToString());

            if (!string.IsNullOrEmpty(pinCode))
                query.Add(nameof(pinCode), pinCode);

            var url = QueryHelpers.AddQueryString("/api/v2/credentials/exist", query);
            var response = await HttpClient.GetJsendAsync(url);
            if (response.IsSuccess() && response.Data.Any())
            {
                var existing = response.Deserialize<Credential[]>();
                if (EditMode)
                    return existing.All(p => uniqueKey == p.UniqueKey);
                if (InsertMode)
                    return !existing.Any();
            }

            return true;
        }
        */
    }
}
