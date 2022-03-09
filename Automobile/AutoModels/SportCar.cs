using Automobile.AutoModels.Base;
using Automobile.Utils;

namespace Automobile.AutoModels
{
    public class SportCar : BaseAuto
    {
        public override EnumType Type { get { return EnumType.SportCar; } }

        public SportCar(double avgConsume, double liquidAmount) : base(avgConsume, liquidAmount)
        {
        }
        protected override double DistanceReduction(double distance)
        {
            return base.DistanceReduction(distance);
        }
    }
}
