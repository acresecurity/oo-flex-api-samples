using System.Net;
using Flex.Cli.DataEntry.Credential.Settings;
using Flex.DataObjects.Cardholder;
using Flex.Services.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.WebUtilities;

namespace Flex.DataObjects.Validation
{
    // ReSharper disable once UnusedMember.Global
    internal class AddCredentialValidation : AbstractValidator<AddCredentialSettings>
    {
        private readonly IFlexHttpClientFactory _factory;

        public AddCredentialValidation(IFlexHttpClientFactory factory, ICacheStore cacheStore)
        {
            _factory = factory;

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

            RuleFor(p => p.PinCode)
                .MustAsync(async (credential, pinCode, context, token) =>
                {
                    var settings = await cacheStore.Settings();
                    if (settings == null || settings.Cardholder.AllowDuplicatePins == true)
                        return true;

                    var uniqueKey = Guid.Empty;
                    return await DoesExists(uniqueKey, null, null, pinCode);
                })
                .WithMessage("Duplicate PIN codes are not allowed.")
                .WhenNotNull();

            RuleFor(p => p.CardNumber)
                .MustAsync(async (credential, cardNumber, context, token) =>
                {
                    var settings = await cacheStore.Settings();
                    if (settings == null || settings.Cardholder.AllowDuplicateCardNumbers == true || credential.Mode > 0)
                        return true;

                    var uniqueKey = Guid.Empty;
                    return await DoesExists(uniqueKey, cardNumber, credential.FacilityCode, null);
                })
                .WithMessage("Duplicate card numbers are not allowed")
                .WhenNotNull();

            RuleFor(p => p.HotStamp)
                .MustAsync(async (credential, hotStamp, context, token) =>
                {
                    var settings = await cacheStore.Settings();
                    if (settings == null || settings.Cardholder.AllowDuplicateCardNumbers == true || credential.Mode == 0)
                        return true;

                    var uniqueKey = Guid.Empty;
                    return await DoesExists(uniqueKey, hotStamp, credential.FacilityCode, null);
                })
                .WithMessage("Duplicate card numbers are not allowed")
                .WhenNotNull();
        }

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

            var client = await _factory.GetClient();
            var response = await client.GetJsendAsync(url);
            if (response.IsSuccess() && response.Data.Any())
            {
                var existing = response.Deserialize<Credential[]>();
                return !existing.Any();
            }

            return true;
        }
    }
}
