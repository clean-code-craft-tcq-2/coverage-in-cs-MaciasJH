using System;
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
            double temperatureInC = 20; 
            
            BreachType breachType = ClassifyTemperatureBreach(batteryChar.coolingType, temperatureInC);
            Alerter alerterController = new Alerter(targetController, breachType);
            alerterController.Setup();
            alerterController.SendAlert();
            Assert.NotNull(alerterController);

            breachType = ClassifyTemperatureBreach(batteryChar.coolingType, temperatureInC);
            Alerter alerterEmail = new Alerter(targetEmail, breachType);
            alerterEmail.Setup();
            alerterEmail.SendAlert();
            Assert.NotNull(alerterEmail);
        }
    }
}
          