namespace CourierFleetDomain.Enuns;

public enum ERentalPlan
{
    SevenDays,
    FifteenDays,
    ThirtyDays,
    FortyFiveDays,
    FiftyDays
}

public class RentalPlanDetail
{
    public int Days { get; set; }
    public decimal DailyCost { get; set; }
    public decimal PenaltyPercentage { get; set; }
}

public static class RentalPlans
{
    public static RentalPlanDetail GetPlan(ERentalPlan plan)
    {
        return plan switch
        {
            ERentalPlan.SevenDays => new RentalPlanDetail { Days = 7, DailyCost = 30.00m, PenaltyPercentage = 0.20m },
            ERentalPlan.FifteenDays => new RentalPlanDetail { Days = 15, DailyCost = 28.00m, PenaltyPercentage = 0.40m },
            ERentalPlan.ThirtyDays => new RentalPlanDetail { Days = 30, DailyCost = 22.00m, PenaltyPercentage = 0.00m },
            ERentalPlan.FortyFiveDays => new RentalPlanDetail { Days = 45, DailyCost = 20.00m, PenaltyPercentage = 0.00m },
            ERentalPlan.FiftyDays => new RentalPlanDetail { Days = 50, DailyCost = 18.00m, PenaltyPercentage = 0.00m },
            _ => throw new ArgumentOutOfRangeException(nameof(plan), $"Not expected rental plan value: {plan}"),
        };
    }
}