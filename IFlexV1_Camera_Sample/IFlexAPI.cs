using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFlexV1_Camera_Sample
{
    interface IFlexAPI
    {
        string ApiKey
        {
            get;
            set;
        }

        string ServiceUrl
        {
            get;
            set;
        }
    }
}
