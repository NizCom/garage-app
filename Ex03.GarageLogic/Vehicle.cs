using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        protected string          m_Model;
        protected readonly string r_LicenseNumber;
        protected List<Wheel>     m_Wheels;
        protected Engine          m_Engine;

        protected Vehicle(string i_LicenseNumber, Engine i_Engine)
        {
            r_LicenseNumber = i_LicenseNumber;
            m_Engine = i_Engine;
        }

        public string Model
        {
            get
            {
                return m_Model;
            }
            private set
            {
                m_Model = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
        }

        public void SetGeneralAttributes(string[] i_GeneralAttributes)
        { 
            m_Model = i_GeneralAttributes[0];
            m_Engine.CurrentEnergyLevel = float.Parse(i_GeneralAttributes[1]);
            setWheels(i_GeneralAttributes[2], i_GeneralAttributes[3]);
        }

        private void setWheels(string i_currentPsiInWheels, string i_WheelsManufacturer)
        {
            float floatCurrentPsiInWheels = float.Parse(i_currentPsiInWheels);

            foreach (Wheel wheel in m_Wheels)
            {
                wheel.Manufacturer = i_WheelsManufacturer;
                wheel.CurrentPsi = floatCurrentPsiInWheels;
            }
        }

        public void InflateInAllWheels()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.InflateWheelToMax();
            }
        }

        public void AddFuelToEngine(float io_AmountFuelToAdd, eFuelType io_FuelTypeToAdd)
        {
            if (m_Engine is FuelEngine)
            {
                (m_Engine as FuelEngine).FillFuel(io_AmountFuelToAdd, io_FuelTypeToAdd);
            }
            else
            {
                throw new ArgumentException("Cannot add fuel to an electric engine!" + Environment.NewLine);
            }
        }

        public void ChargeBattery(float io_AmountOfMinutesToFill)
        {
            if (m_Engine is ElectricEngine)
            {
                (m_Engine as ElectricEngine).Charge(io_AmountOfMinutesToFill);
            }
            else
            {
                throw new ArgumentException("Cannot charge a fuel engine!" + Environment.NewLine);
            }
        }

        public bool IsEngineElectric()
        {
            return m_Engine.IsElectric();
        }

        public override string ToString()
        {
            string stringToPrint = string.Format($@"Vehicle license number: {r_LicenseNumber}
Model: {m_Model}
{m_Wheels[0].ToString()}
{m_Engine.ToString()}
");
            return stringToPrint;
        }

        public abstract void SetUniqueAttributes(string[] i_Attributes);

        public abstract float GetMaxPsiInWheels();
    }


}
