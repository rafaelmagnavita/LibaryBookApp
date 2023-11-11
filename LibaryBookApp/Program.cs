﻿using LibaryAux;
using LibaryAux.Entities;
using LibaryAux.Enums;
using LibaryAux.Repository;
using System;
using System.Linq;

namespace LibaryBookApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            StartApp();
        }
        public static void StartApp()
        {
            bool restart = true;

            while (restart)
            {
                Console.Clear();
                ShowOptions();
                var command = ReadInput();
                int commandInt = intCheck(command);

                SelectMethod(commandInt);

                restart = yesOrNoBox("Do you want to go back to main menu?") == "y";
            }
        }
        public static string ReadInput()
        {
            var input = Console.ReadLine();
            if (input.ToLower().Trim() == "menu")
            {
                Console.Clear();
                Console.WriteLine("Returning to main menu...");
                StartApp();
            }
            return input.ToString();
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
            Console.WriteLine("Type 'menu' anytime you want to go back to this menu!");
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
                    RegisterBook();
                    break;
                case 3:
                    LoanBook();
                    break;
                case 4:
                    ReturnBook();
                    break;
                case 5:
                    RemoveBook();
                    break;
                case 6:
                    ListBook();
                    break;
                case 7:
                    ListAllBooks();
                    break;
            }
        }

        public static void RegisterUser()
        {
            Console.WriteLine("------------------- Let's Add a New User ---------------------");
            Console.WriteLine("Type Name");
            var userName = ReadInput().ToString();
            Console.WriteLine("Type Email");
            var userEmail = ReadInput().ToString();
            if (DbOps.UserExists(userEmail))
            {
                Console.WriteLine("User Already Exists!");
                RegisterUser();
            }
            else
            {
                User user = new User(userName, userEmail);
                bool success = DbOps.AddUser(user);
                if (success)
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

        public static void ListAllBooks()
        {
            //var books = DbOps.GetAllBooks();
            BookRepository bookRepo = new BookRepository();
            var books = bookRepo.FindAll();
            Console.WriteLine($"List of All Libary Books:");
            foreach (var book in books)
            {
                Console.WriteLine($"---------------------------------");
                Console.WriteLine($"ID - {book.Id}");
                Console.WriteLine($"Title - {book.Title}");
                Console.WriteLine($"Author - {book.Author}");
                Console.WriteLine($"Publish Year - {book.PublishYear}");
                Console.WriteLine($"ISBN - {book.ISBN}");
                Console.WriteLine($"---------------------------------");
            }
            Console.WriteLine($"End of the List!");
        }

        public static void ListBook()
        {
            var book = FindBook();
            Console.WriteLine($"ID - {book.Id}");
            Console.WriteLine($"Title - {book.Title}");
            Console.WriteLine($"Author - {book.Author}");
            Console.WriteLine($"Publish Year - {book.PublishYear}");
            Console.WriteLine($"ISBN - {book.ISBN}");

        }

        public static int intCheck(string intStr)
        {
            var intCheck = int.TryParse(intStr, out int intValue);
            while (!intCheck)
            {
                Console.WriteLine("Invalid Input, please digit a number!");
                intStr = ReadInput();
                intCheck = int.TryParse(intStr, out intValue);
            }
            return intValue;
        }

        public static DateTime dateCheck(string dateStr)
        {
            var dateCheck = DateTime.TryParse(dateStr, out DateTime dateValue);
            while (!dateCheck)
            {
                Console.WriteLine("Invalid Input, please digit a valid date!");
                dateStr = ReadInput();
                dateCheck = DateTime.TryParse(dateStr, out dateValue);
            }
            return dateValue;
        }

        public static string yesOrNoBox(string content)
        {
            Console.WriteLine($"{content} (Y/N)?");
            var response = ReadInput();
            while (response.Trim().ToLower() != "y" && response.Trim().ToLower() != "n")
            {
                Console.WriteLine("Invalid answer!");
                response = ReadInput();
            }
            return response.Trim().ToLower();
        }

        public static void RegisterBook()
        {
            Console.WriteLine("------------------- Let's Add a New Book ---------------------");
            Console.WriteLine("Type Book Title");
            var Title = ReadInput().ToString();
            Console.WriteLine("Type Author");
            var Author = ReadInput().ToString();
            Console.WriteLine("Type ISBN");
            var ISBN = ReadInput().ToString();
            Console.WriteLine("Type Publish Year");
            var yearRaw = ReadInput();
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


        public static void LoanBook()
        {
            Console.WriteLine("------------------- Let's Register a Loan ---------------------");

            var user = FindUser();

            var book = FindBook();

            DateTime? loanDate = null;
            var customLoanDate = yesOrNoBox("Set Custom Loan Date");
            if (customLoanDate.Trim().ToLower() == "y")
            {
                loanDate = dateCheck(ReadInput());
            }

            int? loanPeriod = null;
            var customLoanPeriod = yesOrNoBox("Set Custom Loan Period, standart is 90 days");
            if (customLoanPeriod.Trim().ToLower() == "y")
            {
                loanPeriod = intCheck(ReadInput());
            }

            Loan loan = new Loan(user.Id, book.Id, loanDate, loanPeriod);

            bool success = DbOps.AddLoan(loan);
            if (success)
            {
                Console.WriteLine("Book Loaned!");
            }
            else
            {
                Console.WriteLine("Error While Loaning Book!");
                LoanBook();
            }
        }

        public static void ReturnBook()
        {
            Console.WriteLine("------------------- Let's Return a Book ---------------------");
            var user = FindUser();
            var activeLoans = DbOps.GetActiveLoansByUserId(user.Id);
            Console.WriteLine("------------------- List of User's Loaned Books ---------------------");
            foreach (var loan in activeLoans)
            {
                Console.WriteLine($"Loan Id: {loan.Id} - Book : {loan.Book.Title}");
            }
            Console.WriteLine("Type the Id one of the loans above");
            int id = intCheck(ReadInput());
            while (!activeLoans.Any(a => a.Id.Equals(id)))
            {
                Console.WriteLine("Please, choose a valid Id!");
                id = intCheck(ReadInput());
            }

            var selectedLoan = activeLoans.Where(a => a.Id.Equals(id)).FirstOrDefault();

            string confirmReturn = yesOrNoBox($"Confirm return of {selectedLoan.Book.Title} for {user.Name}");

            if (confirmReturn.Equals("y"))
            {
                selectedLoan.BookReturned = true;
                var success = DbOps.EditLoan(selectedLoan);
                if (success)
                {
                    Console.WriteLine("Book Returned!");
                }
                else
                {
                    Console.WriteLine("Error while returning!");
                }
            }

        }

        public static void RemoveBook()
        {
            var book = FindBook();
            string confirmRemove = yesOrNoBox($"Confirm removing book {book.Title}?");
            if (confirmRemove.Equals("y"))
            {
                var success = DbOps.RemoveBook(book);
                if (success)
                {
                    Console.WriteLine("Book Removed!");
                }
                else
                {
                    Console.WriteLine("Error while removing book!");
                }
            }
        }

        public static Book FindBook()
        {
            Console.WriteLine("Chose an Searcher for Book:");
            Console.WriteLine("1. By Book Title");
            Console.WriteLine("2. By ISBN");
            Console.WriteLine("3. By Id");
            int command = intCheck(ReadInput());
            var maxEnumLenght = Enum.GetValues(typeof(SearchEnums.BookSearchCommand)).Length;
            while (command < 1 || command > maxEnumLenght)
            {

                Console.WriteLine("Command should be between 1 and 3!");
                command = intCheck(ReadInput());
            }
            Console.WriteLine("Enter searcher book value:");
            object param = ReadInput();

            var book = DbOps.GetBook(command, param);
            if (book != null)
            {
                return book;
            }
            else
            {
                Console.WriteLine("Book Not Found!!!!");
                return FindBook();
            }
        }

        public static User FindUser()
        {
            Console.WriteLine("Type User Email:");
            var user = DbOps.GetUser(ReadInput());
            while (user == null)
            {
                Console.WriteLine("User Not Found!!!!");
                Console.WriteLine("Type User Email:");
                user = DbOps.GetUser(ReadInput());
            }
            return user;
        }
    }
}
