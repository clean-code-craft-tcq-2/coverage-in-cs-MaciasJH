using System;
using TypewiseAlert.Models;
using Xunit;

namespace TypewiseAlert.Test
{
  public class TypewiseAlertTest
  {
    [Fact]
    public void InfersBreachAsPerLimits()
    {
      Assert.True(TypewiseAlert.InferBreach(12, new Limits{minLimit = 20, maxLimit = 30}) ==
        Configuration.BreachConfig.BreachType.TOO_LOW);
    }
  }
}
