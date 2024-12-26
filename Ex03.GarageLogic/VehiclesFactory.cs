

namespace Ex03.GarageLogic
{
    internal class VehiclesFactory
    {
        private const float     k_CarMaxFuelTank = 45;
        private const eFuelType k_CarFuelType = eFuelType.Octan95;
        private const float     k_CarMaxBatteryTime = 3.5f;

        private const float     k_MotorbikeMaxFuelTank = 5.5f;
        private const eFuelType k_MotorbikeFuelType = eFuelType.Octan98;
        private const float     k_MotorbikeMaxBatteryTime = 2.5f;

        private const float     k_TruckMaxFuelTank = 120f;
        private const eFuelType k_TruckFuelType = eFuelType.Soler;


        public static string[] GetVehiclesTypes()
        {
            string[] vehiclesTypes = {"Fuel Car", "Electric Car", "Fuel Motorbike", "Electric Motorbike", "Truck" };

            return vehiclesTypes;
        }

        public static Vehicle CreateVehicle(string io_LicenseNumber, int io_VehicleType)
        {
            Vehicle vehicle = null;
            Engine engine = null;

            switch (io_VehicleType)
            {
                case (int)eVehicleTypes.FuelCar:
                    engine = new FuelEngine(k_CarFuelType, k_CarMaxFuelTank);
                    vehicle = new Car(io_LicenseNumber, engine);
                    break;
                case (int)eVehicleTypes.ElectricCar:
                    engine = new ElectricEngine(k_CarMaxBatteryTime);
                    vehicle = new Car(io_LicenseNumber, engine);
                    break;
                case (int)eVehicleTypes.FuelMotorbike:
                    engine = new FuelEngine(k_MotorbikeFuelType, k_MotorbikeMaxFuelTank);
                    vehicle = new Motorbike(io_LicenseNumber, engine);
                    break;
                case (int)eVehicleTypes.ElectricMotorbike:
                    engine = new ElectricEngine(k_MotorbikeMaxBatteryTime);
                    vehicle = new Motorbike(io_LicenseNumber, engine);
                    break;
                case (int)eVehicleTypes.Truck:
                    engine = new FuelEngine(k_TruckFuelType, k_TruckMaxFuelTank);
                    vehicle = new Truck(io_LicenseNumber, engine);
                    break;
            }

            return vehicle;
        }
    }
}
