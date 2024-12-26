using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private const string k_VehicleWasNotFoundMessage = "Vehicle was not found!!!";

        private Dictionary<string, VehicleRecord> m_VehiclesRecordsDictionary; // Dictionary license number --> vehicle record

        public Garage()
        {
            m_VehiclesRecordsDictionary = new Dictionary<string, VehicleRecord>();
        }

        public void AddVehicleRecordToGarage(string io_LicenseNumber, string io_OwnerName, string io_OwnerPhoneNumber, int io_VehicleType)
        {
            Owner ownerForVehicleRecord = new Owner(io_OwnerName, io_OwnerPhoneNumber);
            Vehicle vehicleForVehicleRecord = VehiclesFactory.CreateVehicle(io_LicenseNumber, io_VehicleType);
            VehicleRecord newVehicleRecord = new VehicleRecord(ownerForVehicleRecord, vehicleForVehicleRecord);
            m_VehiclesRecordsDictionary[io_LicenseNumber] = newVehicleRecord;
        }

        public void ChangeVehicleStatus(string io_LicenseNumber, eStatus i_NewStatus)
        {
            if (!IsVehicleInSystem(io_LicenseNumber))
            {
                throw new ArgumentException(k_VehicleWasNotFoundMessage);
            }

            m_VehiclesRecordsDictionary[io_LicenseNumber].Status = i_NewStatus;
        }

        public bool IsVehicleInSystem(string io_LicenseNumber)
        {
            return m_VehiclesRecordsDictionary.ContainsKey(io_LicenseNumber);
        }

        public bool IsEmpty()
        {
            return m_VehiclesRecordsDictionary.Count == 0;
        }

        public string[] GetVehicleTypesFromFactory()
        {
            return VehiclesFactory.GetVehiclesTypes();
        }

        public void SetGeneralAttributesToVehicle(string i_LicenseNumber, string[] io_GeneralAttributes)
        {
            m_VehiclesRecordsDictionary[i_LicenseNumber].Vehicle.SetGeneralAttributes(io_GeneralAttributes);
        }

        public void SetUniqueAttributesToVehicle(string io_LicenseNumber, string[] io_Attributes)
        {
            m_VehiclesRecordsDictionary[io_LicenseNumber].Vehicle.SetUniqueAttributes(io_Attributes);
        }

        public void InflatePsiInVehicle(string io_LicenseNumber)
        {
            if (!IsVehicleInSystem(io_LicenseNumber))
            {
                throw new ArgumentException(k_VehicleWasNotFoundMessage);
            }

            m_VehiclesRecordsDictionary[io_LicenseNumber].Vehicle.InflateInAllWheels();
        }
        
        public List<string> ShowListOfLicenseNumbersAccordingTheStatus(int i_statusOfVehicles) 
        {
            List<string> stackOfLicense = new List<string>();

            switch (i_statusOfVehicles)
            {
                case (int)eStatus.InRepair:

                    foreach (VehicleRecord vehicleRecord in m_VehiclesRecordsDictionary.Values)
                    {
                        if (vehicleRecord.Status == eStatus.InRepair)
                        {
                            stackOfLicense.Add(vehicleRecord.Vehicle.LicenseNumber);
                        }
                    }
                    break;
                case (int)eStatus.Repaired:
                    foreach (VehicleRecord vehicleRecord in m_VehiclesRecordsDictionary.Values)
                    {
                        if (vehicleRecord.Status == eStatus.Repaired)
                        {
                            stackOfLicense.Add(vehicleRecord.Vehicle.LicenseNumber);
                        }
                    }
                    break;
                case (int)eStatus.Paid:
                    foreach (VehicleRecord vehicle in m_VehiclesRecordsDictionary.Values)
                    {
                        if (vehicle.Status == eStatus.Paid)
                        {
                            stackOfLicense.Add(vehicle.Vehicle.LicenseNumber);
                        }
                    }
                    break;
            }
            return stackOfLicense;
        }

        public void FillFuelByVehicle(string io_LicenseNumber, float io_AmountToFill, eFuelType io_FuelType)
        {
            if(!IsVehicleInSystem(io_LicenseNumber))
            {
                throw new ArgumentException(k_VehicleWasNotFoundMessage);
            }
            
            m_VehiclesRecordsDictionary[io_LicenseNumber].Vehicle.AddFuelToEngine(io_AmountToFill, io_FuelType);
        }

        public void ChargeVehicleBattery(string io_LicenseNumber, float io_AmountOfMinutesToFill)
        {
            if (!IsVehicleInSystem(io_LicenseNumber))
            {
                throw new ArgumentException(k_VehicleWasNotFoundMessage);
            }

            m_VehiclesRecordsDictionary[io_LicenseNumber].Vehicle.ChargeBattery(io_AmountOfMinutesToFill);
        }

        public bool IsVehicleElectric(string i_LicenseNumber)
        {
            return m_VehiclesRecordsDictionary[i_LicenseNumber].Vehicle.IsEngineElectric();   
        }

        public string ShowVehicleDetails(string io_LicenseNumber)
        {
            if (!IsVehicleInSystem(io_LicenseNumber))
            {
                throw new ArgumentException(k_VehicleWasNotFoundMessage);
            }

            return m_VehiclesRecordsDictionary[io_LicenseNumber].ToString();
        }

        public float GetMaxEnergyInEngine(string i_LicenseNumber)
        {
            return m_VehiclesRecordsDictionary[i_LicenseNumber].Vehicle.Engine.MaxEnergyLevel;
        }

        public float GetMaxPsiInWheelsByVehicle(string i_LicenseNumber)
        {
            return m_VehiclesRecordsDictionary[i_LicenseNumber].Vehicle.GetMaxPsiInWheels();
        }

    }
}
