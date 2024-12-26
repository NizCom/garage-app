using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private const int k_NumOfWheelsInTruck = 12;
        private const int k_MaxPsiInTruckWheel = 28;

        private bool  m_TransportsHazardousGoods;
        private float m_CargoVolume;

        public Truck (string io_LicenseNumber, Engine io_Engine) : base (io_LicenseNumber, io_Engine)
        {
            m_Wheels = new List<Wheel>(k_NumOfWheelsInTruck);

            for (int i = 0; i < k_NumOfWheelsInTruck; i++)
            {
                m_Wheels.Add(new Wheel(k_MaxPsiInTruckWheel));
            }
        }

        public override void SetUniqueAttributes(string[] i_Attributes)
        {
            m_TransportsHazardousGoods = bool.Parse(i_Attributes[0]); 
            m_CargoVolume = float.Parse(i_Attributes[1]);
        }

        public override float GetMaxPsiInWheels()
        {
            return k_MaxPsiInTruckWheel;
        }

        public override string ToString()
        {
            string stringToPrint = base.ToString();
            stringToPrint += string.Format($@"Number of wheels: {k_NumOfWheelsInTruck}
Truck transports hazardous goods: {m_TransportsHazardousGoods}
Cargo volume: {m_CargoVolume}");

            return stringToPrint;
        }
    }
}
