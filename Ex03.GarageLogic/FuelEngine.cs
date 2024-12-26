using System;

namespace Ex03.GarageLogic
{
    internal class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelType;

        public FuelEngine(eFuelType i_FuelType, float io_MaxFuelTank) : base(io_MaxFuelTank)
        {
            r_FuelType = i_FuelType;
        }

        public override bool IsElectric()
        {
            return false;
        }

        public void FillFuel(float io_AmountToAdd, eFuelType i_FuelTypeToAdd)
        {
            if (io_AmountToAdd <= k_MinimumEnergyInEngine)
            {
                throw new ArgumentException($"Fuel amount must be above {k_MinimumEnergyInEngine}!{Environment.NewLine}");
            }
            else if (r_FuelType != i_FuelTypeToAdd)
            {
                throw new ArgumentException($"Wrong Fuel Type! Fuel Type must be {r_FuelType.ToString()}! {Environment.NewLine}");
            }

            AddEnergy(io_AmountToAdd);
        }

        public override string ToString()
        {
            string stringToPrint = base.ToString();
            stringToPrint += string.Format($@"
Fuel type: {r_FuelType}");

            return stringToPrint;
        }
    }
}
