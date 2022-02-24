using System;
using TypewiseAlert.Models;
using static TypewiseAlert.Configuration.AlertConfig;
using static TypewiseAlert.Configuration.BreachConfig;
using static TypewiseAlert.Configuration.CoolingConfig;

namespace TypewiseAlert
{
  public class TypewiseAlert
  {
    public static BreachType InferBreach(double value, Limits limits) {
      if(value < limits.minLimit) {
        return BreachType.TOO_LOW;
      }
      if(value > limits.maxLimit) {
        return BreachType.TOO_HIGH;
      }
      return BreachType.NORMAL;
    }

    public static BreachType ClassifyTemperatureBreach(
        CoolingType coolingType, double temperatureInC) {
            Limits limits = CoolingCases[coolingType];
            return InferBreach(temperatureInC, limits); ;
        }
    public struct BatteryCharacter {
      public CoolingType coolingType;
      public string brand;
    }
    public static void CheckAndAlert(
        AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC) {

        BreachType breachType = ClassifyTemperatureBreach(batteryChar.coolingType, temperatureInC);
        Alerter alerter = new Alerter(alertTarget, breachType);
        alerter.Setup();
        alerter.SendAlert();
    }    
  }
}
