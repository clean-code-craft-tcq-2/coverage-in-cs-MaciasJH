using System;
using static TypewiseAlert.Configuration.BreachConfig;

namespace TypewiseAlert.Configuration
{
    internal class AlertEmail : IAlertSender
    {
        private string recepient = "a.b@c.com";
        public void TriggerAlert(BreachType breachType)
        {
            if (IsAlertTriggered(breachType))
            {
                string body = messageBody[breachType];
                Console.WriteLine("To: {0}\n", recepient);
                Console.WriteLine(body);
            }            
        }

        public bool IsAlertTriggered(BreachType breachType)
        {
            return breachType != 0 ? true : false;         
        }
    }
}