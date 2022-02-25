using System;
using System.IO;
using System.Text;
using TypewiseAlert.Models;
using Xunit;
using static TypewiseAlert.Configuration.AlertConfig;
using static TypewiseAlert.Configuration.BreachConfig;
using static TypewiseAlert.Configuration.CoolingConfig;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        [Fact]
        public void InfersBreachAsPerLimits()
        {
            Assert.True(TypewiseAlert.InferBreach(12, new Limits { minLimit = 20, maxLimit = 30 }) == BreachType.TOO_LOW);
            Assert.True(TypewiseAlert.InferBreach(45, new Limits { minLimit = 20, maxLimit = 30 }) == BreachType.TOO_HIGH);
            Assert.True(TypewiseAlert.InferBreach(25, new Limits { minLimit = 20, maxLimit = 30 }) == BreachType.NORMAL);
        }

        [Fact]
        public void ClassifyTemperatureBreachTest()
        {
            BreachType passiveBreach = ClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 20);
            Assert.IsType<BreachType>(passiveBreach);
            BreachType mediumBreach = ClassifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING, 20);
            Assert.IsType<BreachType>(mediumBreach);
            BreachType highBreach = ClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 20);
            Assert.IsType<BreachType>(highBreach);
        }

        [Fact]
        public void CheckAndAlertTest()
        {
            AlertTarget targetController = AlertTarget.TO_CONTROLLER;
            AlertTarget targetEmail = AlertTarget.TO_EMAIL;
            BatteryCharacter batteryChar = new BatteryCharacter { brand = "Test", coolingType = CoolingType.PASSIVE_COOLING };
            double temperatureOverMax = 55;
            double temperatureUnderMin = -21;
            double temperatureNormal = 20;

            BreachType breachType = ClassifyTemperatureBreach(batteryChar.coolingType, temperatureOverMax);
            Alerter alerterController = new Alerter(targetController, breachType);
            alerterController.Setup();
            alerterController.SendAlert();
            Assert.NotNull(alerterController);

            breachType = ClassifyTemperatureBreach(batteryChar.coolingType, temperatureOverMax);
            Alerter alerterEmail = new Alerter(targetEmail, breachType);
            alerterEmail.Setup();
            alerterEmail.SendAlert();
            Assert.NotNull(alerterEmail);

            // E2E Tests
            var output = new StringWriter();
            Console.SetOut(output);
            CheckAndAlert(targetEmail, batteryChar, temperatureUnderMin);
            var outputString = output.ToString();
            Assert.True(outputString.Length > 0);

            StringBuilder sb = output.GetStringBuilder();
            sb.Remove(0, sb.Length);
            CheckAndAlert(targetEmail, batteryChar, temperatureOverMax);
            outputString = output.ToString();
            Assert.True(outputString.Length > 0);

            sb.Remove(0, sb.Length);
            CheckAndAlert(targetEmail, batteryChar, temperatureNormal);
            outputString = output.ToString();
            Assert.True(outputString.Length == 0);

            sb.Remove(0, sb.Length);
            CheckAndAlert(targetController, batteryChar, temperatureOverMax);
            outputString = output.ToString();
            Assert.True(outputString.Length > 0);
        }
    }
}
          