 using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class GarageManager
    {
        private const int       k_MinimumChoiceInMenu = 1;
        private const int       k_MaximumChoiceInMenu = 8;
        private const string    k_EmptyGarageErrorMessage = "Garage is empty of vehicles!\n";
        private const int       k_PhoneNumberLength = 10;
        private const int       k_MinVehicleTypeChoice = 1;
        private const int       k_MaxVehicleTypeChoice = 5;
        private const int       k_MinDoors = 2;
        private const int       k_MaxDoors = 5;
        private const int       k_YesAnswer = 1;
        private const int       k_NoAnswer = 2;
        private const int       k_Empty = 0;
        private const char      k_Hyphen = '-';
        private const string    k_TrueString = "true";
        private const string    k_FalseString = "false";


        private Garage m_Garage = new Garage();

        public void RunProgram()
        {
            bool userQuit = false;
            printWelcomeMessage();
            
            while (!userQuit)
            {
                printMenu();
                try
                {
                    int userChoice = getUserIntChoice();
                    implementUserChoice(userChoice, ref userQuit);
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine(i_FormatException.Message + " Input must be an integer." + Environment.NewLine);
                }
                catch (ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message + Environment.NewLine);
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine(i_Exception.Message + Environment.NewLine);
                }
            }
            
            printGoogdbyeMessage();
        }
       
        private void printMenu()
        {
            StringBuilder menuFormattedString = new StringBuilder();
            menuFormattedString.AppendFormat(string.Format("Please choose one of the following options ({0} - {1}):"
                                                            , k_MinimumChoiceInMenu, k_MaximumChoiceInMenu));
            menuFormattedString.AppendFormat($@"
{(int)eMenuOptions.AddVehicleToGarage}) Add vehicle to garage
{(int)eMenuOptions.DisplayGarageVehicles}) Display vehicles licenses in garage
{(int)eMenuOptions.ChangeVehicleStatus}) Change vehicle status
{(int)eMenuOptions.InflateVehicleWheels}) Inflate wheels
{(int)eMenuOptions.RefuelVehicle}) Refuel vehicle
{(int)eMenuOptions.ChargeVehicleBattery}) Charge vehicle battery
{(int)eMenuOptions.DisplayVehicleDetails}) Display vehicle details
{(int)eMenuOptions.ExitProgram}) Exit
");

            Console.WriteLine(menuFormattedString);
        }
        
        private void printWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
**************************************
*                                    *
*      Welcome to the garage of      *
*           May and Nizan!           *
*                                    *
**************************************{0}", Environment.NewLine);
            Console.ResetColor();
        }
        
        private void printGoogdbyeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($@"
**************************************
*                                    *
*              Goodbye!              *
*                                    *
**************************************");
            Console.ResetColor();
        }
        
        private int getUserIntChoice()
        {
            return int.Parse(Console.ReadLine());
        }
        
        private void implementUserChoice(int io_UserInput, ref bool io_UserQuit)
        {
            switch (io_UserInput)
            {
                case (int)eMenuOptions.AddVehicleToGarage:
                    addVehicleToGarage();
                    break;
                case (int)eMenuOptions.DisplayGarageVehicles:
                    showListOfLicenseNumbers();
                    break;
                case (int)eMenuOptions.ChangeVehicleStatus:
                    changeVehicleStatusInGarage();
                    break;
                case (int)eMenuOptions.InflateVehicleWheels:
                    inflateVehicleWheels();
                    break;
                case (int)eMenuOptions.RefuelVehicle:
                    refuelVehicle();
                    break;
                case (int)eMenuOptions.ChargeVehicleBattery:
                    chargeVehicleBattery();
                    break;
                case (int)eMenuOptions.DisplayVehicleDetails:
                    displayVehicleDetails();
                    break;
                case (int)eMenuOptions.ExitProgram:
                    Console.WriteLine("Exiting the program...");
                    io_UserQuit = true;
                    break;
                default:
                    throw new ValueOutOfRangeException($"Invalid choice. Please enter a valid option.", k_MinimumChoiceInMenu, k_MaximumChoiceInMenu);
            }
        }

        // ===========   option 1   ===========
        private void addVehicleToGarage() 
        {
            int  vehicleType;

            printLine($"You chose to add a vehicle to the garage!");
            string licenseNumber = getString("Please enter the vehicle license number:");

            if (m_Garage.IsVehicleInSystem(licenseNumber))
            {
                Console.WriteLine("The vehicle with the license No. {0} is already in the system.{1}", licenseNumber);
                Console.WriteLine("Vehicle with the license No. {0} is now in Repair status.{1}", licenseNumber, Environment.NewLine);
                m_Garage.ChangeVehicleStatus(licenseNumber, eStatus.InRepair);
            }
            else
            {  
                createVehicleRecord(licenseNumber, out vehicleType);
                setVehicleGeneralAttributes(licenseNumber);

                if (vehicleType == (int)eVehicleTypes.FuelCar || vehicleType == (int)eVehicleTypes.ElectricCar) //car
                {
                    setCarAttributes(licenseNumber);
                }
                else if (vehicleType == (int)eVehicleTypes.ElectricMotorbike || vehicleType == (int)eVehicleTypes.FuelMotorbike) //motorbike
                {
                    setMotorbikeAttributes(licenseNumber);
                }
                else // [vehicleType == (int)eVehicleTypes.Truck] --> truck
                {
                    setTruckAttributes(licenseNumber);
                }

                Console.WriteLine(Environment.NewLine + "====================================================" + Environment.NewLine);
                Console.WriteLine($"The vehicle {licenseNumber} is added! Now in repair." + Environment.NewLine);
                Console.WriteLine("====================================================" + Environment.NewLine);
            }
        }

        private void createVehicleRecord(string io_LicenseNumberFromUser, out int io_VehicleType)
        {
            string ownerName = getNameFromUser();
            string ownerPhoneNumber = getPhoneNumberFromUser();
            io_VehicleType = getVehicleTypeFromUser();

            m_Garage.AddVehicleRecordToGarage(io_LicenseNumberFromUser, ownerName, ownerPhoneNumber, io_VehicleType);
        }

        private string getNameFromUser()
        {
            string name = getString("Please insert name of the owner:");

            return name;
        }

        private string getPhoneNumberFromUser()
        {
            bool isValid = false;
            string ownerPhoneNumberString = "";
            int phoneNumber;

            while (!isValid)
            {
                try
                {
                    Console.WriteLine("Please insert phone number of the owner ({0} digits) :", k_PhoneNumberLength);
                    ownerPhoneNumberString = Console.ReadLine();

                    if (ownerPhoneNumberString.Length == k_Empty)
                    {
                        throw new ArgumentException("Phone number can't be empty!");
                    }

                    isValid = int.TryParse(ownerPhoneNumberString, out phoneNumber);
                }
                catch (ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message + " Phone number must be digits only" + Environment.NewLine);
                }
                catch (FormatException i_FortmatException)
                {
                    Console.WriteLine(i_FortmatException.Message + " Phone number must be digits only" + Environment.NewLine);
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine(i_Exception.Message + Environment.NewLine);
                }
            }

            return ownerPhoneNumberString;
        }

        private int getVehicleTypeFromUser()
        {
            int      vehicleType = 0;
            bool     isValid = false;
            string   typeChoice;
            string[] types = m_Garage.GetVehicleTypesFromFactory();

            while (!isValid)
            {
                Console.WriteLine($@"
Please choose type of the vehicle:
{(int)eVehicleTypes.FuelCar}- {types[(int)eVehicleTypes.FuelCar - 1]} 
{(int)eVehicleTypes.ElectricCar}- {types[(int)eVehicleTypes.ElectricCar - 1]} 
{(int)eVehicleTypes.FuelMotorbike}- {types[(int)eVehicleTypes.FuelMotorbike - 1]} 
{(int)eVehicleTypes.ElectricMotorbike}- {types[(int)eVehicleTypes.ElectricMotorbike - 1]} 
{(int)eVehicleTypes.Truck}- {types[(int)eVehicleTypes.Truck - 1]} 
");

                try
                {
                    typeChoice = Console.ReadLine();
                    vehicleType = int.Parse(typeChoice);
                    checkIfIntegerInRange(vehicleType, k_MinVehicleTypeChoice, k_MaxVehicleTypeChoice);
                    isValid = true;
                }
                catch (FormatException i_ForamtException)
                {
                    Console.WriteLine(i_ForamtException.Message + " Input must be an integer." + Environment.NewLine);
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(i_ValueOutOfRangeException.Message + Environment.NewLine);
                }
            }

            return vehicleType;
        }

        private void setVehicleGeneralAttributes(string io_LicenseNumber) 
        {
            bool   isVehicleAdded = false;
            string vehicleModel;
            string currentEnergyLevelInPercentage;
            string currentPsiInWheels;
            string wheelsManufacturer;

            while (!isVehicleAdded)
            {
                try
                {
                    vehicleModel = getString("Please enter the vehicle's model:");
                    currentEnergyLevelInPercentage = getString($"Please enter the Energy amount in the engine (up to {m_Garage.GetMaxEnergyInEngine(io_LicenseNumber)}):"); 
                    currentPsiInWheels = getString($"Please enter the current Psi in wheels (up to {m_Garage.GetMaxPsiInWheelsByVehicle(io_LicenseNumber)}):");
                    wheelsManufacturer = getString("Please enter the manufacturer wheels:");
                    string[] generalAttributes = { vehicleModel, currentEnergyLevelInPercentage, currentPsiInWheels, wheelsManufacturer };

                    m_Garage.SetGeneralAttributesToVehicle(io_LicenseNumber, generalAttributes);
                    isVehicleAdded = true;
                }
                catch (ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message + Environment.NewLine);
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine(i_FormatException.Message + Environment.NewLine);
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(i_ValueOutOfRangeException.Message + Environment.NewLine);
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine(i_Exception.Message + Environment.NewLine);
                }
            }
        }

        private string getString(string i_Message)
        {
            bool   isValid = false;
            string stringInput = "";

            while (!isValid)
            {
                try
                {
                    Console.WriteLine(i_Message);
                    stringInput = Console.ReadLine();
                    if(stringInput.Length == k_Empty)
                    {
                        throw new ArgumentException("Input can't be empty!");
                    }
                    isValid = true;
                }
                catch(ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message + Environment.NewLine);
                }
            }

            return stringInput;
        }

        private void setCarAttributes(string io_LicenseNumber)
        {
            string   carColor = getCarColor();
            string   numOfDoors = getNumOfDoors();
            string[] attributes = { carColor, numOfDoors };

            m_Garage.SetUniqueAttributesToVehicle(io_LicenseNumber, attributes);
        }

        private string getCarColor()
        {
            string menuToChooseFrom = $@"
Please choose a color: 
{(int)eColor.Yellow}- Yellow 
{(int)eColor.White}- White 
{(int)eColor.Red}- Red 
{(int)eColor.Black}- Black";

            string colorChoice = getInputFromUser(menuToChooseFrom, (int)eColor.Yellow, (int)eColor.Black);

            return colorChoice;
        }

        private string getNumOfDoors()
        {
            string messageToUser = string.Format(@"Please enter number of doors ({0}-{1}): ", k_MinDoors, k_MaxDoors);
            string numOfDoors = getInputFromUser(messageToUser, k_MinDoors, k_MaxDoors);

            return numOfDoors;
        }

        private void setMotorbikeAttributes(string io_LicenseNumber)
        {
            string   licenseType = getMotorbikeLicenseType();
            string   engineVolume = getEngineVolume();
            string[] attributes = { licenseType, engineVolume };

            m_Garage.SetUniqueAttributesToVehicle(io_LicenseNumber, attributes);
        }

        private string getMotorbikeLicenseType()
        {
            string menuToChooseFrom = $@"Please enter the motorbike License Type: 
{(int)eMotorbikeLicenseType.A}- A 
{(int)eMotorbikeLicenseType.A1}- A1 
{(int)eMotorbikeLicenseType.AA}- AA 
{(int)eMotorbikeLicenseType.B1}- B1
";
            string licenseTypeChoice = getInputFromUser(menuToChooseFrom, (int)eMotorbikeLicenseType.A, (int)eMotorbikeLicenseType.B1);

            return licenseTypeChoice;
        }

        private string getEngineVolume()
        {
            int    engineVolume;
            string engineVolumeString = null;
            bool   isValid = false;

            while (!isValid)
            {
                Console.WriteLine("Please enter the motorbike's engine volume:");
                engineVolumeString = Console.ReadLine();
                try
                {
                    engineVolume = int.Parse(engineVolumeString);
                    isValid = true;
                }
                catch(FormatException i_FormatException)
                {
                    Console.WriteLine(i_FormatException.Message + Environment.NewLine);
                }
            }

            return engineVolumeString;
        }

        private void setTruckAttributes(string io_LicenseNumber)
        {
            string   transportsHazardousGoods = doesTruckTransportsHazardousGoodsFromUser();
            string   cargoVolume = getCargoVolumeFromUser();
            string[] attributes = { transportsHazardousGoods, cargoVolume };

            m_Garage.SetUniqueAttributesToVehicle(io_LicenseNumber, attributes);
        }

        private string doesTruckTransportsHazardousGoodsFromUser()
        {
            string res;
            string messageToPrint = string.Format($@"Does the truck transports hazardous goods : 
{k_YesAnswer} - Yes 
{k_NoAnswer} - No", Environment.NewLine);
            string answerFromUser = getInputFromUser(messageToPrint, k_YesAnswer, k_NoAnswer);

            if(int.Parse(answerFromUser) == k_YesAnswer)
            {
                res = k_TrueString;
            }
            else
            {
                res = k_FalseString;
            }

            return res;
        }

        private string getCargoVolumeFromUser()
        {
            string userInputString = "";
            float  userInputFloat;
            bool   isValid = false;

            while(!isValid)
            {
                try
                {
                    Console.WriteLine("Please enter the cargo volume: ");
                    userInputString = Console.ReadLine();
                    userInputFloat = float.Parse(userInputString);

                    if(userInputFloat <= 0)
                    {
                        throw new ArgumentException("Cargo volume must be above 0!");
                    }
                    isValid = true;
                }
                catch(FormatException i_FormatException)
                {
                    Console.WriteLine(i_FormatException.Message + Environment.NewLine);
                }
                catch (ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message + Environment.NewLine);
                }
            }

            return userInputString;
        }

        // ===========   option 2   ===========
        private void showListOfLicenseNumbers()
        {
            printLine("You choce to see the vehicles in the garage!");

            if (m_Garage.IsEmpty())
            {
                Console.WriteLine(k_EmptyGarageErrorMessage);
            }
            else
            {
                int statusOfVehicles = getVehicleStatusFromUser(); 
                List<string> vehiclesLicenseNumbersList = m_Garage.ShowListOfLicenseNumbersAccordingTheStatus(statusOfVehicles);

                if (vehiclesLicenseNumbersList.Count == 0)
                {
                    Console.WriteLine($"There are no vehicles in the garage in '{(eStatus)statusOfVehicles}' status.");
                }
                else
                {
                    Console.WriteLine($"The license numbers list of the vehicles in status {(eStatus)statusOfVehicles} are:");
                    foreach (string VehicleLicenseNumber in vehiclesLicenseNumbersList)
                    {
                        Console.WriteLine("- " + VehicleLicenseNumber);
                    }
                }
                Console.Write(Environment.NewLine);
            }
        }
        
        private int getVehicleStatusFromUser() 
        {
            string  licenseTypeChoice;
            int     vehicleStatusFromUser;
            string  menuToChooseFrom = $@"Choose the status of the vehicles:
{(int)eStatus.InRepair}- In repair
{(int)eStatus.Repaired}- Repaired
{(int)eStatus.Paid}- paid
";
            licenseTypeChoice = getInputFromUser(menuToChooseFrom, (int)eStatus.InRepair, (int)eStatus.Paid);
            vehicleStatusFromUser = int.Parse(licenseTypeChoice);

            return vehicleStatusFromUser;
        }

        // ===========   option 3   =========== 
        private void changeVehicleStatusInGarage()
        {
            printLine("You chose to change a vehicle's status!");

            if (m_Garage.IsEmpty())
            {
                Console.WriteLine(k_EmptyGarageErrorMessage);
            }
            else
            {
                string licenseNumber = getString("Please enter the vehicle license number:");
                int statusNumber = getVehicleStatusFromUser();
                m_Garage.ChangeVehicleStatus(licenseNumber, (eStatus)statusNumber);
                Console.WriteLine($"Vehicle '{licenseNumber}' status changed to {(eStatus)statusNumber}{Environment.NewLine}");
            }
        }

        // ===========   option 4   ===========
        private void inflateVehicleWheels()
        {
            printLine("You chose to inflate a vehicle's wheels!");

            if (m_Garage.IsEmpty())
            {
                Console.WriteLine(k_EmptyGarageErrorMessage);
            }
            else
            {
                string licenseNumber = getString("Please enter the vehicle license number:");
                m_Garage.InflatePsiInVehicle(licenseNumber);
                Console.WriteLine($"The wheels of vehicle '{licenseNumber}' are full! {Environment.NewLine}");
            }
        }

        // ===========   option 5   ===========
        private void refuelVehicle()
        {
            printLine("You chose to refule a vehicle!");

            string messageToUser = "Please enter fuel amount to fill:";

            if (m_Garage.IsEmpty())
            {
                Console.WriteLine(k_EmptyGarageErrorMessage);
            }
            else
            {
                string licenseNumber = getString("Please enter the vehicle license number:");
                int fuelType = int.Parse(getVehicleFuelTypeFromUser());
                float fuelAmountToFill = getAmountToFillFromUser(messageToUser);
                m_Garage.FillFuelByVehicle(licenseNumber, fuelAmountToFill, (eFuelType)fuelType);
                Console.WriteLine($"Vehicle No. {licenseNumber} got refueled!");
            }
        }

        private string getVehicleFuelTypeFromUser()
        {
            string menuToChooseFrom = $@"
Choose a fuel type:
{(int)eFuelType.Octan95}- Octan95 
{(int)eFuelType.Octan96}- Octan96 
{(int)eFuelType.Octan98}- Octan98 
{(int)eFuelType.Soler}- Soler";

            string fuelTypeChoice = getInputFromUser(menuToChooseFrom, (int)eFuelType.Octan95, (int)eFuelType.Soler);

            return fuelTypeChoice;
        }

        private float getAmountToFillFromUser(string i_MessageToUser)   
        {
            bool  isValid = false;
            float amountToFill = 0;

            while (!isValid)
            {
                Console.WriteLine(i_MessageToUser);
                amountToFill = float.Parse(Console.ReadLine()); 
                isValid = true;
                
            }
           
            return amountToFill; 
        }

        // ===========   option 6   ===========
        private void chargeVehicleBattery()
        {
            printLine("You chose to charge a vehicle's battery!");

            string messageToUser = "Please enter amount of minutes for charging:";

            if (m_Garage.IsEmpty())
            {
                Console.WriteLine(k_EmptyGarageErrorMessage);
            }
            else
            {
                string licenseNumber = getString("Please enter the vehicle license number:");
                float amountOfMinutesToFill = getAmountToFillFromUser(messageToUser);
                m_Garage.ChargeVehicleBattery(licenseNumber, amountOfMinutesToFill);
                Console.WriteLine($"Vehicle No. {licenseNumber} got recharged!");
            }
        }

        // ===========   option 7   ===========
        private void displayVehicleDetails()
        {
            printLine("You chose to see vehicle's details!");

            if (m_Garage.IsEmpty())
            {
                Console.WriteLine(k_EmptyGarageErrorMessage);
            }
            else
            {
                string licenseNumber = getString("Please enter the vehicle license number:");

                string stringToPrint = string.Format($@"
************************************
The details of vehicle:
{m_Garage.ShowVehicleDetails(licenseNumber)}
************************************
");
                Console.WriteLine(stringToPrint);
            }
        }

        private string getInputFromUser(string i_MessageToPrint, int i_MinValue, int i_MaxValue)
        {
            bool   isChoiceValid = false;
            int    userChoice;
            string userChoiceString = null;

            while (!isChoiceValid)
            {
                Console.WriteLine(i_MessageToPrint);
                userChoiceString = Console.ReadLine();

                try
                {
                    userChoice = int.Parse(userChoiceString);
                    checkIfIntegerInRange(userChoice, i_MinValue, i_MaxValue);
                    isChoiceValid = true;
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine(i_FormatException.Message + Environment.NewLine);
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(i_ValueOutOfRangeException.Message + Environment.NewLine);
                }
            }

            return userChoiceString;
        }

        private void printLine(string i_LineToPrint)
        {
            StringBuilder lineBuilder = new StringBuilder(Environment.NewLine + i_LineToPrint + Environment.NewLine);
            lineBuilder.Append(new string(k_Hyphen, i_LineToPrint.Length));
            lineBuilder.Append(Environment.NewLine);
            Console.WriteLine(lineBuilder);
        }

        private void checkIfIntegerInRange(int io_userChoice, int io_MinChoice, int io_MaxChoice)
        {
            if (io_MinChoice > io_userChoice || io_userChoice > io_MaxChoice)
            {
                throw new ValueOutOfRangeException($"Input must be between {io_MinChoice} to {io_MaxChoice}!", io_MinChoice, io_MaxChoice);
            }
        }
    }
}
