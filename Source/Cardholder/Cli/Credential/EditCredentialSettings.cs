using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Credential
{
    internal class EditCredentialSettings : CredentialSettings
    {
        private readonly IValidator<EditCredentialSettings> _validation;

        public EditCredentialSettings(IValidator<EditCredentialSettings> validation)
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

        /// <summary>
        /// The Date/Time a credential was activated
        /// </summary>
        [CommandOption($"--{nameof(Activation)}")]
        public virtual System.DateTime? Activation { get; set; }

        /// <summary>
        /// Is the credential active or not
        /// </summary>
        [CommandOption($"--{nameof(Active)}")]
        public virtual bool? Active { get; set; }

        /// <summary>
        /// The ada mode of the credential
        /// </summary>
        [CommandOption($"--{nameof(AdaMode)}")]
        public virtual bool? AdaMode { get; set; }

        /// <summary>
        /// Determine if this credential should always be downloaded
        /// </summary>
        [CommandOption($"--{nameof(AlwaysDownload)}")]
        public virtual bool? AlwaysDownload { get; set; }

        /// <summary>
        /// The apb location of the credential
        /// </summary>
        [CommandOption($"--{nameof(ApbLocation)}")]
        public virtual int? ApbLocation { get; set; }

        /// <summary>
        /// If ASSA credential format for the credential
        /// </summary>
        [CommandOption($"--{nameof(AssaCredentialFormat)}")]
        public virtual int? AssaCredentialFormat { get; set; }

        /// <summary>
        /// The Assa facility code for the credential
        /// </summary>
        [CommandOption($"--{nameof(AssaFacilityCode)}")]
        public virtual int? AssaFacilityCode { get; set; }

        /// <summary>
        /// Should this credential be automatically activated
        /// </summary>
        [CommandOption($"--{nameof(AutoActivate)}")]
        public virtual bool? AutoActivate { get; set; }

        /// <summary>
        /// Should this credential be automatically deactivated
        /// </summary>
        [CommandOption($"--{nameof(AutoDeactivate)}")]
        public virtual bool? AutoDeactivate { get; set; }

        /// <summary>
        /// Card number of the credential
        /// </summary>
        [CommandOption($"--{nameof(CardNumber)}")]
        public virtual ulong? CardNumber { get; set; }

        /// <summary>
        /// The credential card type
        /// </summary>
        [CommandOption($"--{nameof(CardType)}")]
        public virtual int? CardType { get; set; }

        /// <summary>
        /// Date/Time the credential was created
        /// </summary>
        [CommandOption($"--{nameof(Created)}")]
        public virtual DateTime? Created { get; set; }

        /// <summary>
        /// The format of the credential
        /// </summary>
        [CommandOption($"--{nameof(CredentialFormat)}")]
        public int? CredentialFormat { get; set; }

        /// <summary>
        /// The reason the credential was deactivated
        /// </summary>
        [CommandOption($"--{nameof(DisabledReason)}")]
        public virtual int? DisabledReason { get; set; }

        /// <summary>
        /// Determine if the ApbLocation can be changed
        /// </summary>
        [CommandOption($"--{nameof(DontChangeApbLocation)}")]
        public virtual bool? DontChangeApbLocation { get; set; }

        /// <summary>
        /// Determine if the Use Count can be changed manually
        /// </summary>
        [CommandOption($"--{nameof(DontChangeUseCount)}")]
        public virtual bool? DontChangeUseCount { get; set; }

        /// <summary>
        /// Date/Time the credential expires
        /// </summary>
        [CommandOption($"--{nameof(Expiration)}")]
        public virtual DateTime? Expiration { get; set; }

        /// <summary>
        /// The facility code of the credential
        /// </summary>
        [CommandOption($"--{nameof(FacilityCode)}")]
        public virtual int? FacilityCode { get; set; }

        /// <summary>
        /// The GSA credential for the credential
        /// </summary>
        [CommandOption($"--{nameof(GsaCredential)}")]
        public virtual string GsaCredential { get; set; }

        /// <summary>
        /// Host based macro for the credential
        /// </summary>
        [CommandOption($"--{nameof(HostBasedMacro)}")]
        public virtual int? HostBasedMacro { get; set; }

        /// <summary>
        /// HotStamp value of the credential
        /// </summary>
        [CommandOption($"--{nameof(HotStamp)}")]
        public virtual ulong? HotStamp { get; set; }

        /// <summary>
        /// Issuecode of the credential
        /// </summary>
        [CommandOption($"--{nameof(IssueCode)}")]
        public virtual int? IssueCode { get; set; }

        /// <summary>
        /// Determine if TimeAttendance active for the credential
        /// </summary>
        [CommandOption($"--{nameof(IsTimeAttendance)}")]
        public virtual bool? IsTimeAttendance { get; set; }

        /// <summary>
        /// Date/Time the credential was last accessed
        /// </summary>
        [CommandOption($"--{nameof(LastAccessDate)}")]
        public virtual DateTime? LastAccessDate { get; set; }

        /// <summary>
        /// The last access event for this credential
        /// </summary>
        [CommandOption($"--{nameof(LastAccessEvent)}")]
        public virtual int? LastAccessEvent { get; set; }

        /// <summary>
        /// The last access location for this credential
        /// </summary>
        [CommandOption($"--{nameof(LastAccessLocation)}")]
        public virtual string LastAccessLocation { get; set; }

        /// <summary>
        /// Current mode for this credential
        /// </summary>
        [CommandOption($"--{nameof(Mode)}")]
        public virtual int? Mode { get; set; }

        /// <summary>
        /// Determine if credential get one Apb pass
        /// </summary>
        [CommandOption($"--{nameof(OneFreeApbPass)}")]
        public virtual bool? OneFreeApbPass { get; set; }

        /// <summary>
        /// Pin code for this credential
        /// </summary>
        [CommandOption($"--{nameof(PinCode)}")]
        public virtual string PinCode { get; set; }

        /// <summary>
        /// Does this credential have a pin exception
        /// </summary>
        [CommandOption($"--{nameof(PinExempt)}")]
        public virtual bool? PinExempt { get; set; }

        /// <summary>
        /// Date/Time this credential was printed
        /// </summary>
        [CommandOption($"--{nameof(Printed)}")]
        public virtual DateTime? Printed { get; set; }

        /// <summary>
        /// Token for the credential (internal use)
        /// </summary>
        [CommandOption($"--{nameof(Token)}")]
        public virtual string Token { get; set; }

        /// <summary>
        /// Trigger code 1 for the credential
        /// </summary>
        [CommandOption($"--{nameof(TriggerCode1)}")]
        public virtual int? TriggerCode1 { get; set; }

        /// <summary>
        /// Trigger code 2 for the credential
        /// </summary>
        [CommandOption($"--{nameof(TriggerCode2)}")]
        public virtual int? TriggerCode2 { get; set; }

        /// <summary>
        /// Trigger code 3 for the credential
        /// </summary>
        [CommandOption($"--{nameof(TriggerCode3)}")]
        public virtual int? TriggerCode3 { get; set; }

        /// <summary>
        /// Trigger code 4 for the credential
        /// </summary>
        [CommandOption($"--{nameof(TriggerCode4)}")]
        public virtual int? TriggerCode4 { get; set; }

        /// <summary>
        /// Trigger code 5 for the credential
        /// </summary>
        [CommandOption($"--{nameof(TriggerCode5)}")]
        public virtual int? TriggerCode5 { get; set; }

        /// <summary>
        /// Trigger code 6 for the credential
        /// </summary>
        [CommandOption($"--{nameof(TriggerCode6)}")]
        public virtual int? TriggerCode6 { get; set; }

        /// <summary>
        /// Trigger code 7 for the credential
        /// </summary>
        [CommandOption($"--{nameof(TriggerCode7)}")]
        public virtual int? TriggerCode7 { get; set; }

        /// <summary>
        /// Date/Time credential was updated
        /// </summary>
        [CommandOption($"--{nameof(Updated)}")]
        public virtual DateTime? Updated { get; set; }

        /// <summary>
        /// A max number of times the credential can be used
        /// </summary>
        [CommandOption($"--{nameof(UseLimit)}")]
        public virtual int? UseLimit { get; set; }

        /// <summary>
        /// Date/Time for start of vacation and credential temporarily deactivated
        /// </summary>
        [CommandOption($"--{nameof(VacationDate)}")]
        public virtual DateTime? VacationDate { get; set; }

        /// <summary>
        /// Number of days for vacation deactivation
        /// </summary>
        [CommandOption($"--{nameof(VacationDuration)}")]
        public virtual int? VacationDuration { get; set; }

        /// <summary>
        /// Is this credential a VIP credential
        /// </summary>
        [CommandOption($"--{nameof(Vip)}")]
        public virtual bool? Vip { get; set; }

        /// <summary>
        /// A credential with this enabled can unlock a door that is in locked/lock down mode.
        /// </summary>
        [CommandOption($"--{nameof(Override)}")]
        public virtual bool? Override { get; set; }
    }
}
