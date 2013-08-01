using System;
using System.Linq;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public enum HardwareTypes
    {
        SSP = 1,
        SSPTamper,
        Card,
        SIO,
        SIOCabTamper, //05
        SIOPwrTamper,
        MP,
        CP,
        ACM,
        DVR,          //10
        DSM,
        Direct,
        REX1,
        REX2,
        Timezone,     //15
        Macro,
        Trigger,
        TriggerVar,
        MPG,
        AccessArea,   //20
        AccessLevel,
        Strike,
        Relay,
        Reader,
        ThreatMgr,    //25
        Site,
        Floor,
        UserCmnd,
        Station,
        Channel,      //30
        Camera,
        Expanded
    }

    public class PackedAddress
    {
        static readonly HardwareTypes[] NoPointTypes = new HardwareTypes[] { 
            HardwareTypes.Timezone, 
            HardwareTypes.Macro, 
            HardwareTypes.Trigger, 
            HardwareTypes.TriggerVar, 
            HardwareTypes.MPG, 
            HardwareTypes.AccessArea, 
            HardwareTypes.UserCmnd 
        };

        static readonly HardwareTypes[] DoorTypes = new HardwareTypes[] { 
            HardwareTypes.ACM, 
            HardwareTypes.DSM, 
            HardwareTypes.REX1, 
            HardwareTypes.REX2, 
            HardwareTypes.Strike, 
            HardwareTypes.Relay 
        };

        /// <summary>
        /// Given a packed address for some piece of hardware this will convert it to the packed address for the associated SSP
        /// </summary>
        /// <param name="APacked"></param>
        public static int ToSSPAddress(int APacked)
        {
            return APacked & 0x7FFE0000 | 1;
        }

        public static bool IsExtended(int APacked)
        {
            return ((APacked & 0x80000000) >> 31) == 1;
        }

        /// <summary>
        /// Change just the type of a given Packed Address to a particular type
        /// </summary>
        /// <param name="APacked"></param>
        /// <param name="AType"></param>
        /// <returns></returns>
        public static long SetType(int APacked, HardwareTypes AType)
        {
            return (APacked & 0xFFFFFFE0) & (int)AType;
        }

        public static long Encode(HardwareTypes AType, int ASiteNo, int ASSPNo, int ASIONo, int APointNo)
        {
            if (NoPointTypes.Contains(AType))
                return (ASiteNo << 25) | (ASSPNo << 17) | (ASIONo << 5) | (int)AType;
            else
                return (ASiteNo << 25) | (ASSPNo << 17) | (ASIONo) << 10 | (APointNo << 5) | (int)AType;
        }

        public static void Decode(int APacked, ref HardwareTypes AType, ref int ASiteNo, ref int ASSPNo, ref int ASIONo, ref int APointNo)
        {
            AType = (HardwareTypes)(APacked & 0x1F);
            ASiteNo = (APacked >> 25) & 0x3F;
            ASSPNo = (APacked >> 17) & 0xFF;
            if (NoPointTypes.Contains(AType))
            {
                ASIONo = (APacked >> 5) & 0xFFF;
                APointNo = 0;
            }
            else
            {
                ASIONo = (APacked >> 10) & 0x7F;
                APointNo = (APacked >> 5) & 0x1F;
            }
        }

        public static string ToString(int APacked)
        {
            HardwareTypes type = HardwareTypes.SSP;
            int siteNo = 0;
            int sspNo = 0;
            int sioNo = 0;
            int pointNo = 0;
            Decode(APacked, ref type, ref siteNo, ref sspNo, ref sioNo, ref pointNo);

            string doorType = (DoorTypes.Contains(type) && pointNo == 1) ? "E" : "D";

            switch (type)
            {
                case HardwareTypes.Site:
                    return string.Format("Site {0}", siteNo);
                case HardwareTypes.SSP:
                case HardwareTypes.SSPTamper:
                case HardwareTypes.Card:
                    return string.Format("{0}.{1}", siteNo, sspNo);
                case HardwareTypes.SIO:
                case HardwareTypes.SIOCabTamper:
                case HardwareTypes.SIOPwrTamper:
                    return string.Format("{0}.{1}.{2}", siteNo, sspNo, sioNo);
                case HardwareTypes.MP:
                    return string.Format("{0}.{1}.{2}.I{3}", siteNo, sspNo, sioNo, pointNo);
                case HardwareTypes.Strike:
                case HardwareTypes.Relay:
                case HardwareTypes.CP:
                    return string.Format("{0}.{1}.{2}.O{3}", siteNo, sspNo, sioNo, pointNo);
                case HardwareTypes.ACM:
                case HardwareTypes.DSM:
                case HardwareTypes.REX1:
                case HardwareTypes.REX2:
                    return String.Format("{0}.{1}.{2}{3}", siteNo, sspNo, doorType, sioNo);
                case HardwareTypes.Timezone:
                    return string.Format("{0}.{1}.TS{2}", siteNo, sspNo, sioNo);
                case HardwareTypes.Macro:
                    return string.Format("{0}.{1}.M{2}", siteNo, sspNo, sioNo);
                case HardwareTypes.Trigger:
                    return string.Format("{0}.{1}.T{2}", siteNo, sspNo, sioNo);
                case HardwareTypes.TriggerVar:
                    return string.Format("{0}.{1}.TV{2}", siteNo, sspNo, sioNo);
                case HardwareTypes.MPG:
                    return string.Format("{0}.{1}.MPG{2}", siteNo, sspNo, sioNo);
                case HardwareTypes.AccessArea:
                    return string.Format("{0}.{1}.A{2}", siteNo, sspNo, sioNo);
                case HardwareTypes.Reader:
                    return string.Format("{0}.{1}.R{2}", siteNo, sspNo, sioNo);
                default:
                    return "0.0.0.0";
            }
        }

        public static HardwareTypes ToHardwareType(int APacked)
        {
            return (HardwareTypes)(APacked & 0x1F);
        }
    }
}
