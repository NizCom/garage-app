
namespace Ex03.GarageLogic
{
    internal abstract class Engine
    {
        protected const float k_MinimumEnergyInEngine = 0;
        protected const float k_MaxPercentage = 100.0f;

        protected readonly float r_MaxEnergyLevel;
        protected float          m_CurrentEnergyLevel;
        protected float          m_EnergyLeftInPercent;  

        protected Engine (float m_MaxEnergyLevel)
        {
            r_MaxEnergyLevel = m_MaxEnergyLevel;
        }

        public float MaxEnergyLevel
        {
            get
            {
                return r_MaxEnergyLevel;
            }
        }

        public float CurrentEnergyLevel
        {
            get
            {
                return m_CurrentEnergyLevel;
            }
            set
            {
                if (value < k_MinimumEnergyInEngine )
                {
                    throw new ValueOutOfRangeException($"Energy level can't be negative!", k_MinimumEnergyInEngine, r_MaxEnergyLevel);
                }
                else if(r_MaxEnergyLevel < value)
                {
                    throw new ValueOutOfRangeException($"Energy level can't exceed the {r_MaxEnergyLevel}!", k_MinimumEnergyInEngine, r_MaxEnergyLevel);
                }

                m_CurrentEnergyLevel = value;
                m_EnergyLeftInPercent = (m_CurrentEnergyLevel / MaxEnergyLevel) * k_MaxPercentage;
            }
        }

        protected float EnergyLeftInPercent
        {
            get
            {
                return m_EnergyLeftInPercent;
            }
            set
            {
                m_EnergyLeftInPercent = value;
            }
        }

        public void AddEnergy(float i_AmountToAdd)
        {
            float newAmountInEngine = CurrentEnergyLevel + i_AmountToAdd;
            CurrentEnergyLevel = newAmountInEngine;
            m_EnergyLeftInPercent = newAmountInEngine / MaxEnergyLevel;
        }

        public abstract bool IsElectric();

        public override string ToString()
        {
            return string.Format(@"Current Energy Level in engine: {0:F2}%
Max Capacity: {1}", m_EnergyLeftInPercent, r_MaxEnergyLevel);
        }
    }
}

