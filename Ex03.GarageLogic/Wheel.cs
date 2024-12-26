
namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        const int k_EmptyWheel = 0;

        private string         m_Manufacturer;
        private float          m_CurrentPsi;
        private readonly float r_MaxPsi;

        public Wheel(float i_MaxPsi)
        {
            r_MaxPsi = i_MaxPsi;
        }

        public string Manufacturer
        {
            set
            {
                m_Manufacturer = value;
            }
        }
        public float CurrentPsi
        {
            set
            {
                if (value < k_EmptyWheel || value > r_MaxPsi)
                {
                    throw new ValueOutOfRangeException($"Psi in wheels must be between {k_EmptyWheel} to {r_MaxPsi}!", k_EmptyWheel, r_MaxPsi);
                }

                m_CurrentPsi = value;
            }
        }
        
        public void InflateWheel(float i_PsiToAdd)
        {
            float newPsiInWheel = m_CurrentPsi + i_PsiToAdd;

            if (newPsiInWheel > r_MaxPsi)
            {
                throw new ValueOutOfRangeException($"Psi input is too high. The maximum psi in this vehicle's wheels is: {r_MaxPsi}", k_EmptyWheel, r_MaxPsi);
            }
            else
            {
                m_CurrentPsi = newPsiInWheel;
            }
        }

        public void InflateWheelToMax()
        {
            m_CurrentPsi = r_MaxPsi;
        }

        public override string ToString()
        {
            string stringToPrint = string.Format($@"Wheels manufacturer: {m_Manufacturer}
Wheels Psi is: {m_CurrentPsi}");

            return stringToPrint;
        }
    }
}
