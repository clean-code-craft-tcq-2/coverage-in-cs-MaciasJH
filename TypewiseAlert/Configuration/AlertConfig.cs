using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Models;

namespace TypewiseAlert.Configuration
{
    public class AlertConfig
    {       
        public enum AlertTarget
        {
            TO_CONTROLLER,
            TO_EMAIL
        };

        public static Dictionary<AlertTarget, IAlertSender> alertTo = new Dictionary<AlertTarget, IAlertSender>{

            {AlertTarget.TO_CONTROLLER, new AlertController () },
            {AlertTarget.TO_EMAIL, new AlertEmail() },

        };
    }
}
