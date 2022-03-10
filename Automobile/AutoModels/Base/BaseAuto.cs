using Automobile.Utils;

namespace Automobile.AutoModels.Base
{
    public abstract class BaseAuto
    {
        /// <summary>
        /// Type of a car
        /// Sets only once during the initialization
        /// </summary>
        public abstract EnumType Type { get; }
        /// <summary>
        /// Average Liquid Consumption (km/l)
        /// Sets only once during the initialization
        /// Can be changed only inside the class
        /// </summary>
        public double AvgConsume { get; private set; }
        /// <summary>
        /// Maximum Liquid Amount of the Auto (l)
        /// Sets only once during the initialization
        /// Can be changed only inside the class
        /// </summary>
        public double MaxLiquidAmount { get; private set; }
        /// <summary>
        /// Current Speed of the Car (km/h)
        /// Can be changed any time
        /// </summary>
        public double Speed { get; set; }
        /// <summary>
        /// Current Liquid Amount of the Auto (l)
        /// Can be changed any time
        /// </summary>
        public double CurrentLiquidAmount { get; set; }
        /// <summary>
        /// Base Constructor of the Base class
        /// </summary>
        /// <param name="avgConsume">Average Liquid Consumption (km/l)</param>
        /// <param name="liquidAmount">Maximum Liquid Amount(l)</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected BaseAuto(double avgConsume, double liquidAmount)
        {
            if (avgConsume <= 0 | avgConsume > liquidAmount) throw new ArgumentException("Wrong arguments. avgConsume cant be low or equal zero.", nameof(avgConsume));
            AvgConsume = avgConsume;
            MaxLiquidAmount = CurrentLiquidAmount = liquidAmount;
        }
        /// <summary>
        /// Adds Liquid to the Current Liquid Amount
        /// </summary>
        /// <param name="liquidAmount"></param>
        /// <returns>How many liquid was added</returns>
        /// <exception cref="ArgumentException"></exception>
        public double AddLiquid(double liquidAmount)
        {
            if (liquidAmount < 0) throw new ArgumentException("Wrong argument. liquidAmount cant be low then zero.");
            double lastLiquid = this.CurrentLiquidAmount;
            this.CurrentLiquidAmount = Math.Min(this.MaxLiquidAmount, lastLiquid + liquidAmount);
            return this.CurrentLiquidAmount - lastLiquid;
        }
        /// <summary>
        /// Calculates the possible distance that can be reached with Maximum Liquid Amount
        /// </summary>
        /// <returns></returns>
        public double MaximumPossibleDistance()
        {
            return this.PossibleDistance(this.MaxLiquidAmount);
        }
        /// <summary>
        /// Calculates the possible distance that can be reached with Current Liquid Amount
        /// </summary>
        /// <returns></returns>
        public double CurrentPossibleDistance()
        {
            return this.PossibleDistance(this.CurrentLiquidAmount);
        }
        /// <summary>
        /// Calculates the possible distance that can be reached with Current Liquid Amount depends on Additional Weight or Passengers
        /// </summary>
        /// <returns></returns>
        public double CurrentPossibleDistanceWeightOn()
        {
            return this.PossibleDistanceWithAdditionalWeight(this.CurrentLiquidAmount);
        }
        /// <summary>
        /// Calculates the Drive Time needed to reach the given distance
        /// </summary>
        /// <param name="liquidAmount">Liquid Amount (l)</param>
        /// <param name="distance">Distance (km)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public double DriveTime(double liquidAmount, double distance)
        {
            if (distance <= 0) throw new ArgumentException("Distance to reach cant be less or equal zero.", nameof(distance));

            double maxDistance = this.PossibleDistanceWithAdditionalWeight(liquidAmount);
            if (maxDistance < distance) throw new Exception("There is not enough Liquid Amount to reach the distance.");

            if (Speed > 0)
            {
                return distance / Speed;
            }
            throw new Exception("To achive the distance Speed must be greater then zero.");
        }
        /// <summary>
        /// Calculates the the distance loss depends on Additional Weight or Amount of Passengers
        /// </summary>
        /// <param name="distance">Distance to reach without Weight or Passengers</param>
        /// <returns></returns>
        protected virtual double DistanceReduction(double distance)
        {
            return 0;
        }
        /// <summary>
        /// Distance that can be reached by the Auto depends on the Liquid Amount
        /// </summary>
        /// <param name="liquidAmount">Liquid Amount (l)</param>
        /// <returns>Returns Distance (km)</returns>
        private double PossibleDistance(double liquidAmount)
        {
            if (liquidAmount < 0) throw new ArgumentException("Liquid Amount cant be less then zero.", nameof(liquidAmount));
            return liquidAmount * AvgConsume;
        }
        /// <summary>
        /// Calculates the distance that can be reached depends on Additional Weight or Passengers and Liquid Amount
        /// </summary>
        /// <param name="liquidAmount">Liquid Amount (l)</param>
        /// <returns></returns>
        private double PossibleDistanceWithAdditionalWeight(double liquidAmount)
        {
            double distance = PossibleDistance(liquidAmount);
            double reduction = Math.Min(distance, this.DistanceReduction(distance));
            return distance - reduction;
        }
    }
}
