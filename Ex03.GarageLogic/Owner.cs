
namespace Ex03.GarageLogic
{
    internal class Owner
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;

        public Owner(string i_Name, string i_PhoneNumber)
        {
            r_OwnerName = i_Name;
            r_OwnerPhoneNumber = i_PhoneNumber;
        }

        public string Name
        {
            get
            {
                return r_OwnerName;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return PhoneNumber;
            }
        }

        public override string ToString()
        {
            string stringToPrint = string.Format($@"
Owner name: {r_OwnerName}
Phone number: {r_OwnerPhoneNumber}");
            return stringToPrint;
        }
    }
}
