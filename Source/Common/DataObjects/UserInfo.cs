
namespace Common.DataObjects
{
    public class UserInfo
    {
        private Dictionary<UserRights, OperatorRight> _lookup;

        public OperatorRight this[UserRights right]
        {
            get
            {
                _lookup ??= Rights.ToDictionary(p => p.Right, p => p);
                return _lookup[right];
            }
        }

        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Profile { get; set; }

        public bool AllowWebUse { get; set; }

        public string Email { get; set; }

        public IEnumerable<Claim> Claims { get; set; }

        public class Claim
        {
            public string Issuer { get; set; }

            public string Type { get; set; }

            public string Value { get; set; }

            public string ValueType { get; set; }
        }

        public OperatorRight[] Rights { get; set; }

        public enum RightType
        {
            Boolean,
            ReadWrite,
            Selection
        }

        public enum ReadWriteState
        {
            None,
            ReadOnly,
            ReadWrite
        }

        public class OperatorRight
        {
            public UserRights Right { get; set; }

            public RightType Type { get; set; }

            public object Value { get; set; }

            public bool AsBool() => bool.TryParse(Value.ToString(), out var result) && result;

            public ReadWriteState AsState() => Enum.TryParse(Value.ToString(), true, out ReadWriteState result) ? result : ReadWriteState.None;

            public static implicit operator bool(OperatorRight right) => right.AsBool();

            public static implicit operator ReadWriteState(OperatorRight right) => right.AsState();
        }

        public int AdminLevel { get; set; }
    }
}
