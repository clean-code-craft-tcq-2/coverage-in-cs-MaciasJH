using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Models;

namespace TypewiseAlert.Configuration
{
    public class BreachConfig
    {
        public enum BreachType
        {
            NORMAL,
            TOO_LOW,
            TOO_HIGH
        };

        public static Dictionary<BreachType, string> messageBody = new Dictionary<BreachType, string>
        {
            {BreachType.TOO_HIGH, "Hi, the temperature is too high\n" },
            {BreachType.TOO_LOW, "Hi, the temperature is too low\n" }
        };
    }
}
