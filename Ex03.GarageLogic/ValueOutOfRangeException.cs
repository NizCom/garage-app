using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(string io_Message, float i_MinValue, float i_MaxValue)
            : base(string.Format(io_Message)) 
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }
        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }
    }
}
