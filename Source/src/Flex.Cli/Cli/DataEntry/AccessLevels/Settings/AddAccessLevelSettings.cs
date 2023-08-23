using Flex.DataObjects;
using FluentValidation;

namespace Flex.Cli.DataEntry.AccessLevels.Settings
{
    internal class AddAccessLevelSettings : DefaultCommandSettings
    {
        private readonly IValidator<AddAccessLevelSettings> _validation;

        public AddAccessLevelSettings(IValidator<AddAccessLevelSettings> validation) => _validation = validation;

        #region Overrides of DefaultCommandSettings

        protected override FluentValidation.Results.ValidationResult InternalValidate() => _validation.ValidateAsync(this).GetAwaiter().GetResult();

        #endregion

        public Guid UniqueKey { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? DisableActivationPeriod { get; set; }

        public DateTime? Activation { get; set; }

        public DateTime? Deactivation { get; set; }

        public Guid DefaultTimeSchedule { get; set; }

        public Escort Escort { get; set; }

        public SecurityLevel SecurityLevel { get; set; }
    }
}
