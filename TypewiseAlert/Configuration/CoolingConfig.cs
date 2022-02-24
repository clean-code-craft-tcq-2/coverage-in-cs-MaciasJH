using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Models;

namespace TypewiseAlert.Configuration
{
    public class CoolingConfig
    {
        public enum CoolingType
        {
            PASSIVE_COOLING,
            HI_ACTIVE_COOLING,
            MED_ACTIVE_COOLING
        };

        public static Dictionary<CoolingType, Limits> CoolingCases = new Dictionary<CoolingType, Limits>{
            {CoolingType.HI_ACTIVE_COOLING, new Limits{minLimit = 0, maxLimit = 45 } },
            {CoolingType.MED_ACTIVE_COOLING, new Limits{minLimit = 0, maxLimit = 40 } },
            {CoolingType.PASSIVE_COOLING, new Limits{minLimit = 0, maxLimit = 35 } }
        };
    }
}
