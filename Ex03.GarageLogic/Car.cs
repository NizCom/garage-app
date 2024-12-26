using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private const int   k_NumOfWheelsInCar = 5;
        private const float k_MaxPsiInCarWheel = 31;

        private eColor m_CarColor;
        private int    m_NumOfDoors;

        public Car(string io_LicenseNumber, Engine io_Engine): base(io_LicenseNumber, io_Engine)
        {
            m_Wheels = new List<Wheel>(k_NumOfWheelsInCar);

            for (int i = 0; i < k_NumOfWheelsInCar; i++)
            {
                m_Wheels.Add(new Wheel(k_MaxPsiInCarWheel));
            }
        }

        public override void SetUniqueAttributes(string[] i_Attributes)
        {
            m_CarColor = (eColor)int.Parse(i_Attributes[0]);
            m_NumOfDoors = int.Parse(i_Attributes[1]);
        }

        public override float GetMaxPsiInWheels()
        {
            return k_MaxPsiInCarWheel;
        }

        public override string ToString()
        {
            string stringToPrint = base.ToString();
            stringToPrint += string.Format($@"Number of wheels: {k_NumOfWheelsInCar}
Car color: {m_CarColor}
Number of doors: {m_NumOfDoors}
");
            return stringToPrint;
        }

    }
}
