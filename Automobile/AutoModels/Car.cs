using Automobile.AutoModels.Base;
using Automobile.Utils;

namespace Automobile.AutoModels
{
    public class Car : BaseAuto
    {
        /// <summary>
        /// The coeficcient that increases the Average Liquid Consumption per passenger
        /// </summary>
        private readonly double passengersCoef = 0.06;
        /// <summary>
        /// The Maximum Amount of Passengers for Car
        /// Sets only once during the initialization
        /// Can be changed only inside the class
        /// </summary>
        public int MaxPassengers { get; private set; }
        /// <summary>
        /// The Current Amount of Passengers in the Car
        /// Can be changed only inside the class
        /// </summary>
        public int CurrentPassengers { get; set; } = 0;
        /// <summary>
        /// Type of a car
        /// Sets only once during the initialization
        /// </summary>
        public override EnumType Type { get { return EnumType.Car; } }
        public Car(int maxPassengers, double avgConsume, double liquidAmount) : base(avgConsume, liquidAmount)
        {
            if (maxPassengers < 0 | maxPassengers > 5) throw new ArgumentException("Wrong Amount of Maximum Passengers Allowed.", nameof(maxPassengers));
            MaxPassengers = maxPassengers;
        }
        /// <summary>
        /// Adds passengers
        /// </summary>
        /// <param name="passengers"></param>
        /// <returns>How many passengers were added</returns>
        /// <exception cref="ArgumentException"></exception>
        public int AddPassengers(int passengers)
        {
            if (passengers < 0) throw new ArgumentException("Wrong argument. passengers cant be less then zero", nameof(passengers));
            int lastPassengers = this.CurrentPassengers;
            this.CurrentPassengers = Math.Min(this.MaxPassengers, lastPassengers + passengers);
            return this.CurrentPassengers - lastPassengers;
        }
        protected override double DistanceReduction(double distance)
        {
            if (distance < 0) throw new ArgumentException("The Distance can't be less then zero.", nameof(distance));
            return distance * CurrentPassengers * passengersCoef;
        }
    }
}
