using System;
using static TypewiseAlert.Configuration.BreachConfig;

namespace TypewiseAlert
{
    public interface IAlertSender
    {
        void TriggerAlert(BreachType breachType);
    }
}