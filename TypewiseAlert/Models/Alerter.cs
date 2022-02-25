using System;
using static TypewiseAlert.Configuration.AlertConfig;
using static TypewiseAlert.Configuration.BreachConfig;

namespace TypewiseAlert
{
    public class Alerter
    {
        private AlertTarget alertTarget;
        private BreachType breachType;
        private IAlertSender alertSender;

        public Alerter(AlertTarget alertTarget, BreachType breachType)
        {
            this.alertTarget = alertTarget;
            this.breachType = breachType;
        }

        public void Setup()
        {
            alertSender = alertTo[alertTarget];
        }

        public void SendAlert()
        {
            alertSender.TriggerAlert(breachType);
        }
    }
}