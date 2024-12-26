
namespace Ex03.GarageLogic
{
    internal class VehicleRecord
    {
        private readonly Owner r_Owner;
        private Vehicle        m_Vehicle;
        private eStatus        m_VehicleStatus = eStatus.InRepair;

        public VehicleRecord(Owner i_Owner, Vehicle i_Vehicle)
        {
            r_Owner = i_Owner;
            m_Vehicle = i_Vehicle;
        }

        public eStatus Status
        {
            get
            {
                return m_VehicleStatus;
            }
            internal set
            {
                m_VehicleStatus = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public override string ToString()
        {
            string stringToPrint = string.Format($@"{r_Owner.ToString()}
{Vehicle.ToString()}");

            return stringToPrint;
        }

    }
}
