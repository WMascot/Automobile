using Automobile.AutoModels.Base;
using Automobile.Utils;

namespace Automobile.AutoModels
{
    public class Lorry : BaseAuto
    {
        /// <summary>
        /// The coeficcient that increases the Average Liquid Consumption per 200 kg of weight
        /// </summary>
        private readonly double weightCoef = 0.04;
        /// <summary>
        /// The Maximum Amount of Weight for Car (kg)
        /// Sets only once during the initialization
        /// Can be changed only inside the class
        /// </summary>
        public int MaxWeight { get; private set; }
        /// <summary>
        /// The Current Amount of Weight for Car (kg)
        /// Can be changed only inside the class
        /// </summary>
        public double Weight { get; set; } = 0;
        /// <summary>
        /// Type of a car
        /// Sets only once during the initialization
        /// </summary>
        public override EnumType Type { get { return EnumType.Lorry; } }

        public Lorry(int maxWeight, double avgConsume, double liquidAmount) : base(avgConsume, liquidAmount)
        {
            if (maxWeight < 0) throw new ArgumentOutOfRangeException("Maximum Amount of Weight cant be less then zero.", nameof(maxWeight));
            if (avgConsume * ((int)maxWeight / 200) * 0.04 >= liquidAmount * 0.8) throw new Exception("The Lorry is overweighted");
            MaxWeight = maxWeight;
        }
        protected override double DistanceReduction(double distance)
        {
            if (distance < 0) throw new ArgumentException("The Distance can't be less then zero.", nameof(distance));
            return distance * (int)Weight / 200 * weightCoef;
        }
    }
}
