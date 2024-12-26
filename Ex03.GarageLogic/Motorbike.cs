using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class Motorbike : Vehicle
    {
        private const int   k_NumOfWheelsInMotorbike = 2;
        private const float k_MaxPsiInMotorbikeWheel = 33;

        private eMotorbikeLicenseType m_MotorbikeLicenseType;
        private int m_EngineVolume;

        public Motorbike(string io_LicenseNumber, Engine io_Engine) : base(io_LicenseNumber, io_Engine)
        {
            m_Wheels = new List<Wheel>(k_NumOfWheelsInMotorbike);

            for (int i = 0; i < k_NumOfWheelsInMotorbike; i++)
            {
                m_Wheels.Add(new Wheel(k_MaxPsiInMotorbikeWheel));
            }
        }

        public override void SetUniqueAttributes(string[] i_Attributes)
        {
            m_MotorbikeLicenseType = (eMotorbikeLicenseType)int.Parse(i_Attributes[0]);
            m_EngineVolume = int.Parse(i_Attributes[1]);
        }

        public override float GetMaxPsiInWheels()
        {
            return k_MaxPsiInMotorbikeWheel;
        }

        public override string ToString()
        {
            string stringToPrint = base.ToString();
            stringToPrint += string.Format($@"Number of wheels: {k_NumOfWheelsInMotorbike}
License type: {m_MotorbikeLicenseType}
Engine volume is: {m_EngineVolume}
");
            return stringToPrint;
        }

    }

}
