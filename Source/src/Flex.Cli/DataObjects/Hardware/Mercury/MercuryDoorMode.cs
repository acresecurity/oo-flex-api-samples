
namespace Flex.DataObjects.Hardware.Mercury
{
    public enum MercuryDoorMode
    {
        Disable = 1,
        Unlocked = 2,
        Locked = 3,
        FacilityCodeOnly = 4,
        CardOnly = 5,
        PinOnly = 6,
        CardAndPinRequired = 7,
        CardOrPinRequired = 8
    }
}