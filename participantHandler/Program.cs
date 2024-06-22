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
        public static IList<Person> participants = manage.GetParticipants();
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
                VerifyUserInput();
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

        public static void VerifyUserInput()
        {
            string userInput = Console.ReadLine();
            if ((!ValidateUserInput(userInput, mainRegex)))
            {
                Console.WriteLine("Invalid Input. Please enter a valid numeric value from 1 to 4.");
                Console.WriteLine("");
                return;
            }
            Console.WriteLine("");
            HandleUserInput(userInput);
        }

        public static void HandleUserInput(string userInput)
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
        public static bool ValidateUserInput(string user, string regex)
        {
            return Regex.IsMatch(user, regex);
        }

        public static void AddUser()
        {
            Console.WriteLine("Please enter a valid name:");
            string name = Console.ReadLine();
            Console.WriteLine("");

            if (!ValidateUserInput(name, userRegex))
            {
                Console.WriteLine("Invalid entry. Please enter a valid name.");
                return;
            }

            Console.WriteLine("Please enter a valid lastName:");
            string lastName = Console.ReadLine();
            Console.WriteLine("");

            if (!ValidateUserInput(lastName, userRegex))
            {
                Console.WriteLine("Invalid entry. Please enter a valid lastName.");
                return;
            }

            manage.Add(name, lastName);
            Console.WriteLine($"Operation Succeeded: {name} {lastName} has been successfully enrolled to this program.");
        }

        public static void RemoveUser()
        {
            ShowUsers();
            if (participants.Count == 0)
            {
                Console.WriteLine("Nothing to be removed, program is empty.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Please enter a valid numeric index of a participant to be removed:");
            string input = Console.ReadLine();

            if (!ValidateUserInput(input, @"^\d+$"))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid entry. Please enter a valid index.");
                return;
            }

            Console.WriteLine();
            try
            {
                var removedParticipant = manage.GetParticipants()[int.Parse(input)];
                manage.Remove(int.Parse(input)); 
                Console.WriteLine($"Operation Succeeded: {removedParticipant.Name} {removedParticipant.LastName} has been successfully removed from this program.");
            }
            catch (ArgumentOutOfRangeException ex)
            {    
                Console.WriteLine(ex.Message);
            }
        }

        public static void ShowUsers()
        {
            var participants = manage.GetParticipants();
            Console.WriteLine("Enrolled participants:");
            for (int i = 0; i < participants.Count; i++)
            {
                Console.WriteLine($"{i}. {participants[i].Name} {participants[i].LastName}");
            }
            Console.WriteLine("----------------------------------------------");
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
