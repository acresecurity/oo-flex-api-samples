namespace Common.DataObjects
{
    /// <summary>
    /// Card credential belonging to a cardholder record
    /// </summary>
    public class Credential
    {
        /// <summary>
        /// The Date/Time a credential was activated
        /// </summary>
        public virtual DateTime? Activation { get; set; }

        /// <summary>
        /// Is the credential active or not
        /// </summary>
        public virtual bool? Active { get; set; }

        /// <summary>
        /// The ada mode of the credential
        /// </summary>
        public virtual bool? AdaMode { get; set; }

        /// <summary>
        /// Determine if this credential should always be downloaded
        /// </summary>
        public virtual bool? AlwaysDownload { get; set; }

        /// <summary>
        /// The apb location of the credential
        /// </summary>
        public virtual int? ApbLocation { get; set; }

        /// <summary>
        /// If ASSA credential format for the credential
        /// </summary>
        public virtual int? AssaCredentialFormat { get; set; }

        /// <summary>
        /// The Assa facility code for the credential
        /// </summary>
        public virtual int? AssaFacilityCode { get; set; }

        /// <summary>
        /// Should this credential be automatically activated
        /// </summary>
        public virtual bool? AutoActivate { get; set; }

        /// <summary>
        /// Should this credential be automatically deactivated
        /// </summary>
        public virtual bool? AutoDeactivate { get; set; }

        /// <summary>
        /// Card number of the credential
        /// </summary>
        public virtual ulong? CardNumber { get; set; }

        /// <summary>
        /// The credential card type
        /// </summary>
        public virtual int? CardType { get; set; }

        /// <summary>
        /// Date/Time the credential was created
        /// </summary>
        public virtual DateTime? Created { get; set; }

        /// <summary>
        /// The format of the credential
        /// </summary>
        public int? CredentialFormat { get; set; }

        /// <summary>
        /// The reason the credential was deactivated
        /// </summary>
        public virtual int? DisabledReason { get; set; }

        /// <summary>
        /// Determine if the ApbLocation can be changed
        /// </summary>
        public virtual bool? DontChangeApbLocation { get; set; }

        /// <summary>
        /// Determine if the Use Count can be changed manually
        /// </summary>
        public virtual bool? DontChangeUseCount { get; set; }

        /// <summary>
        /// Date/Time the credential expires
        /// </summary>
        public virtual DateTime? Expiration { get; set; }

        /// <summary>
        /// The facility code of the credential
        /// </summary>
        public virtual int? FacilityCode { get; set; }

        /// <summary>
        /// The GSA credential for the credential
        /// </summary>
        public virtual string GsaCredential { get; set; }

        /// <summary>
        /// Host based macro for the credential
        /// </summary>
        public virtual int? HostBasedMacro { get; set; }

        /// <summary>
        /// HotStamp value of the credential
        /// </summary>
        public virtual ulong? HotStamp { get; set; }

        /// <summary>
        /// Issuecode of the credential
        /// </summary>
        public virtual int? IssueCode { get; set; }

        /// <summary>
        /// Determine if TimeAttendance active for the credential
        /// </summary>
        public virtual bool? IsTimeAttendance { get; set; }

        /// <summary>
        /// Date/Time the credential was last accessed
        /// </summary>
        public virtual DateTime? LastAccessDate { get; set; }

        /// <summary>
        /// The last access event for this credential
        /// </summary>
        public virtual int? LastAccessEvent { get; set; }

        /// <summary>
        /// The last access location for this credential
        /// </summary>
        public virtual string LastAccessLocation { get; set; }

        /// <summary>
        /// Current mode for this credential
        /// </summary>
        public virtual int? Mode { get; set; }

        /// <summary>
        /// Determine if credential get one Apb pass
        /// </summary>
        public virtual bool? OneFreeApbPass { get; set; }

        /// <summary>
        /// Pin code for this credential
        /// </summary>
        public virtual string PinCode { get; set; }

        /// <summary>
        /// Does this credential have a pin exception
        /// </summary>
        public virtual bool? PinExempt { get; set; }

        /// <summary>
        /// Date/Time this credential was printed
        /// </summary>
        public virtual DateTime? Printed { get; set; }

        /// <summary>
        /// Token for the credential (internal use)
        /// </summary>
        public virtual string Token { get; set; }

        /// <summary>
        /// Trigger code 1 for the credential
        /// </summary>
        public virtual int? TriggerCode1 { get; set; }

        /// <summary>
        /// Trigger code 2 for the credential
        /// </summary>
        public virtual int? TriggerCode2 { get; set; }

        /// <summary>
        /// Trigger code 3 for the credential
        /// </summary>
        public virtual int? TriggerCode3 { get; set; }

        /// <summary>
        /// Trigger code 4 for the credential
        /// </summary>
        public virtual int? TriggerCode4 { get; set; }

        /// <summary>
        /// Trigger code 5 for the credential
        /// </summary>
        public virtual int? TriggerCode5 { get; set; }

        /// <summary>
        /// Trigger code 6 for the credential
        /// </summary>
        public virtual int? TriggerCode6 { get; set; }

        /// <summary>
        /// Trigger code 7 for the credential
        /// </summary>
        public virtual int? TriggerCode7 { get; set; }

        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        public virtual Guid? UniqueKey { get; set; }

        /// <summary>
        /// Date/Time credential was updated
        /// </summary>
        public virtual DateTime? Updated { get; set; }

        /// <summary>
        /// A max number of times the credential can be used
        /// </summary>
        public virtual int? UseLimit { get; set; }

        /// <summary>
        /// Unique identifier for the personnel record associated with the credential
        /// </summary>
        public virtual Guid? PersonnelKey { get; set; }

        /// <summary>
        /// Date/Time for start of vacation and credential temporarily deactivated
        /// </summary>
        public virtual DateTime? VacationDate { get; set; }

        /// <summary>
        /// Number of days for vacation deactivation
        /// </summary>
        public virtual int? VacationDuration { get; set; }

        /// <summary>
        /// Is this credential a VIP credential
        /// </summary>
        public virtual bool? Vip { get; set; }

        /// <summary>
        /// A credential with this enabled can unlock a door that is in locked/lock down mode.
        /// </summary>
        public virtual bool? Override { get; set; } // TODO
    }
}