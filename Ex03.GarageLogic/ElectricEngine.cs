using System;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        internal ElectricEngine(float io_MaxBatteryTime) : base(io_MaxBatteryTime)
        {
        }

        public override bool IsElectric()
        {
            return true;
        }

        public void Charge(float io_AmountOfMinutesToFill)
        {
            if (io_AmountOfMinutesToFill <= k_MinimumEnergyInEngine)
            {
                throw new ArgumentException($"Minutes for charging must be above {k_MinimumEnergyInEngine}!");
            }

            AddEnergy(io_AmountOfMinutesToFill);
        }

    }
}
