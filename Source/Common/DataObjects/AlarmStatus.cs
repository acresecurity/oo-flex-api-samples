namespace Common.DataObjects
{
    public enum AlarmStatus
    {
        PendingClear = 0,
        ReturnToNormal = 1,
        Acknowledge = 2,
        Alarm = 3,
        Unknown = 4,
        Normal = 5,
    }
}