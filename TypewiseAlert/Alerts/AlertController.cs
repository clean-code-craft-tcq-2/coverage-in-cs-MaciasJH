using System;

namespace TypewiseAlert.Configuration
{
    internal class AlertController : IAlertSender
    {
        public void TriggerAlert(BreachConfig.BreachType breachType)
        {
            const ushort header = 0xfeed;
            Console.WriteLine("{} : {}\n", header, breachType);
        }
    }
}