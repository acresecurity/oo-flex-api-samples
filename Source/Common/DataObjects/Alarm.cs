namespace Common.DataObjects
{
    public class Alarm
    {
        public virtual int UniqueId { get; set; }

        public virtual int CameraId { get; set; }

        public Guid? CardholderKey { get; set; }

        public virtual int Count { get; set; }

        public virtual int EventDescriptionId { get; set; }

        public virtual string HardwareAddress { get; set; }

        public virtual string HardwareDescription { get; set; }

        public virtual string HardwareType { get; set; }

        public virtual Guid? HardwareUniqueKey { get; set; }

        public virtual AlarmNotification Notification { get; set; }

        public virtual int Priority { get; set; }

        public virtual AlarmStatus Status { get; set; }

        public virtual DateTime Transaction { get; set; }

        public UserRights PriorityToRight(UserRights baseRight)
        {
            if (Priority < 1 || Priority > 15)
                return baseRight;

            return baseRight + (Priority - 1);
        }
    }
}