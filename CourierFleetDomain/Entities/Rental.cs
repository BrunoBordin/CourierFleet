using CourierFleetDomain.Enuns;

namespace CourierFleetDomain.Entities
{
    public class Rental
    {
        public int Id { get; set; }

        public int CourierId { get; set; }

        public int MotorcycleId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ExpectedEndDate { get; set; }

        public ERentalPlan Plan { get; set; }

        public Motorcycle Motorcycle { get; set; }

        public Courier Courier { get; set; }

        public Rental(int courierId, int motorcycleId, DateTime startDate, DateTime endDate, DateTime expectedEndDate, ERentalPlan plan)
        {
            CourierId = courierId;
            MotorcycleId = motorcycleId;
            Plan = plan;
            StartDate = startDate;
            EndDate = endDate;
            ExpectedEndDate = expectedEndDate;
        }

        public decimal CalculateTotalCost(DateTime returnDate)
        {
            var plan = RentalPlans.GetPlan(Plan);

            if (returnDate < ExpectedEndDate)
            {
                var daysRented = (returnDate - StartDate).Days;
                var dailyCost = plan.DailyCost;
                var totalCost = daysRented * dailyCost;

                var daysNotEffected = (ExpectedEndDate - returnDate).Days;
                var penalty = daysNotEffected * dailyCost * plan.PenaltyPercentage;

                return totalCost + penalty;
            }
            else if (returnDate > ExpectedEndDate)
            {
                var daysRented = (ExpectedEndDate - StartDate).Days;
                var dailyCost = plan.DailyCost;
                var totalCost = daysRented * dailyCost;

                var additionalDays = (returnDate - ExpectedEndDate).Days;
                var additionalCost = additionalDays * 50.00m;

                return totalCost + additionalCost;
            }
            else
            {
                var daysRented = (ExpectedEndDate - StartDate).Days;
                var dailyCost = plan.DailyCost;
                return daysRented * dailyCost;
            }
        }
    }
}