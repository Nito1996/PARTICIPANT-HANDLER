using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace participantHandler
{
    internal class Program
    {
        static readonly string mainRegex = @"^[1-4]$";
        static readonly string userRegex = @"^[a-zA-Z]{2,10}$";
        public static IManage manage = new Manage();
        public static bool exitRequested = false;

        public enum MenuOption
        {
            ADD = 1,
            REMOVE,
            HISTORY,
            EXIT
        }
        static void Main()
        {
            do
            {
                ShowMenu();
                OperationHandler();
            } while (!exitRequested);
        }

        public static void ShowMenu()
        {
            Console.WriteLine("Welcome to the PARTICIPANT HANDLER PROGRAM!");
            Console.WriteLine("Which action would you like to perform today?");
            Console.WriteLine("");
            Console.WriteLine($"1. {MenuOption.ADD}");
            Console.WriteLine($"2. {MenuOption.REMOVE}");
            Console.WriteLine($"3. {MenuOption.HISTORY}");
            Console.WriteLine($"4. {MenuOption.EXIT}");
            Console.WriteLine("");
        }

        public static void OperationHandler()
        {
            string userInput = Console.ReadLine();
            if (!Regex.IsMatch(userInput, mainRegex))
            {
                Console.WriteLine("Invalid Input. Please enter a valid numeric value from 1 to 4.");
                Console.WriteLine("");
                return;
            }
            Console.WriteLine("");
            OperationExcuter(userInput);
        }

        public static void OperationExcuter(string userInput)
        {
            MenuOption option = Enum.Parse<MenuOption>(userInput);

            switch (option)
            {
                case MenuOption.ADD:
                    AddUser();
                    break;
                case MenuOption.REMOVE:
                    RemoveUser();
                    break;
                case MenuOption.HISTORY:
                    ShowUsers();
                    break;
                case MenuOption.EXIT:
                    LogOutConfirm();
                    break;
            }
            Console.WriteLine("");
        }
        public static bool ValidateUser(string user)
        {
            if (!Regex.IsMatch(user, userRegex))
            {
                return false;
            }
            return true;
        }

        public static void AddUser()
        {
            Console.WriteLine("Please enter a valid name:");
            string name = Console.ReadLine();
            Console.WriteLine("");

            if (!ValidateUser(name))
            {
                Console.WriteLine("Invalid entry. Please enter a valid name.");   
                return;
            }

            Console.WriteLine("Please enter a valid lastName:");
            string lastName = Console.ReadLine();
            Console.WriteLine("");

            if (!ValidateUser(lastName))
            {
                Console.WriteLine("Invalid entry. Please enter a valid lastName.");
                return;
            }

            Manage.Add(name, lastName);
            Console.WriteLine($"Operation Succeeded: {name} {lastName} has been successfully enrolled to this program.");
        }

        public static void RemoveUser()
        {
            Console.WriteLine("Please enter a valid name:");
            string name = Console.ReadLine();
            Console.WriteLine("");

            if (!ValidateUser(name))
            {
                Console.WriteLine("Invalid entry. Please enter a valid name.");
                Console.WriteLine("");
                return;
            }

            Console.WriteLine("Please enter a valid lastName:");
            string lastName = Console.ReadLine();
            Console.WriteLine("");

            if (!ValidateUser(lastName))
            {
                Console.WriteLine("Invalid entry. Please enter a valid lastName.");
                Console.WriteLine("");
                return;
            }

            if (Manage.Remove(name, lastName))
            {
                Console.WriteLine($"Operation Succeeded: {name} {lastName} has been successfully removed from this program.");
                return;
            }

            Console.WriteLine("Invalid Entry. That person doesn't exist in the current Matrix");
        }

        public static void ShowUsers()
        {
            IList<Person> participants = manage.GetParticipants();
            Console.WriteLine("Enrolled participants:");
            foreach (var participant in participants)
            {
                Console.WriteLine($"{participant.Name} {participant.LastName}");
            }
            Console.WriteLine("");
        }

        public static void LogOutConfirm()
        {
            string input;
            do
            {
                Console.WriteLine("Are you sure you want to log out?");
                Console.WriteLine("1. YES / 2. NO");

                input = Console.ReadLine();
                Console.WriteLine("");
            }
            while (input != "1" && input != "2");

            if (input == "1")
            {
                Console.WriteLine("Logging out...");
                Console.WriteLine("Thank you for using our services. You have a great day!");
                Console.WriteLine("");
                exitRequested = true;
            }
            return;
        }
    }
}
