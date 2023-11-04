using LibaryAux;
using LibaryAux.Entities;
using System;

namespace LibaryBookApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            ShowOptions();
            var command = Console.ReadLine();
            int commandInt = intCheck(command);

            SelectMethod(commandInt);

        }

        public static void ShowOptions()
        {
            Console.WriteLine("------------Welcome to Libary Book's Loan Project!--------------");
            Console.WriteLine("Please, chose a command:");
            Console.WriteLine("1. Register User");
            Console.WriteLine("2. Register Book");
            Console.WriteLine("3. Loan Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Remove Book");
            Console.WriteLine("6. List Book");
            Console.WriteLine("7. List All Books");
            Console.WriteLine("---------------------------------------------------------------");
        }

        public static void SelectMethod(int command)
        {
            switch (command)
            {
                case 1:
                    RegisterUser();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }
        }

        public static void RegisterUser()
        {
            Console.WriteLine("------------------- Let's Add a New User ---------------------");
            Console.WriteLine("Type Name");
            var userName = Console.ReadLine().ToString();
            Console.WriteLine("Type Email");
            var userEmail = Console.ReadLine().ToString();
            if (DbOps.UserExists(userEmail))
            {
                Console.WriteLine("User Already Exists!");
                RegisterUser();
            }
            else
            {
                User user = new User(userName, userEmail);
                bool success = DbOps.AddUser(user);
                if(success)
                {
                    Console.WriteLine("User Registered!");
                }
                else
                {
                    Console.WriteLine("Error While Registrating User!");
                    RegisterUser();
                }
            }
        }

        public static int intCheck(string intStr)
        {
            var intCheck = int.TryParse(intStr, out int intValue);
            while (!intCheck)
            {
                Console.WriteLine("Invalid Input, please digit a number!");
                intStr = Console.ReadLine();
                intCheck = int.TryParse(intStr, out intValue);
            }
            return intValue;
        }

        public static void RegisterBook()
        {
            Console.WriteLine("------------------- Let's Add a New Book ---------------------");
            Console.WriteLine("Type Book Title");
            var Title = Console.ReadLine().ToString();
            Console.WriteLine("Type Author");
            var Author = Console.ReadLine().ToString();
            Console.WriteLine("Type ISBN");
            var ISBN = Console.ReadLine().ToString(); 
            Console.WriteLine("Type Publish Year");
            var yearRaw = Console.ReadLine();
            int year = intCheck(yearRaw);

            if (DbOps.GetBook(2, ISBN) != null)
            {
                Console.WriteLine("Book Already Registered!");
                RegisterBook();
            }
            else
            {
                Book book = new Book(Title, Author, ISBN, year);
                bool success = DbOps.AddBook(book);
                if (success)
                {
                    Console.WriteLine("Book Registered!");
                }
                else
                {
                    Console.WriteLine("Error While Registrating Book!");
                    RegisterBook();
                }
            }
        }
    }
}
