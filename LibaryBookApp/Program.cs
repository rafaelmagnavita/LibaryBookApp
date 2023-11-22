using LibaryAux;
using LibaryDomain.Entities;
using LibaryDomain.Enums;
using LibaryAux.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using LibaryStructure.LogManager;

namespace LibaryBookApp
{
    public class Program
    {
        private static BookRepository _bookRepository;
        private static LoanRepository _loanRepository;
        private static UserLoginRepository _userRepository;
        private static ConsoleLogManager _console;
        public static void Initialize()
        {
            _bookRepository = new BookRepository();
            _loanRepository = new LoanRepository();
            _userRepository = new UserLoginRepository();
            _console = new ConsoleLogManager();
        }
        static async Task Main(string[] args)
        {
            await StartApp();
        }
        public static async Task StartApp()
        {
            bool restart = true;

            while (restart)
            {
                Console.Clear();
                Initialize();
                ShowOptions();
                int commandInt = _console.Read<int>();

                await SelectMethod (commandInt);

                restart = _console.yesOrNoBox("Do you want to go back to main menu?");
            }
        }

        public static void ShowOptions()
        {
            _console.Write("------------Welcome to Libary Book's Loan Project!--------------");
            _console.Write("Please, chose a command:");
            _console.Write("1. Register User");
            _console.Write("2. Register Book");
            _console.Write("3. Loan Book");
            _console.Write("4. Return Book");
            _console.Write("5. Remove Book");
            _console.Write("6. List Book");
            _console.Write("7. List All Books");
            _console.Write("Type 'menu' anytime you want to go back to this menu!");
            _console.Write("---------------------------------------------------------------");
        }

        public static async Task SelectMethod(int command)
        {
            switch (command)
            {
                case 1:
                    await RegisterUser();
                    break;
                case 2:
                    await RegisterBook();
                    break;
                case 3:
                    await LoanBook();
                    break;
                case 4:
                    await ReturnBook();
                    break;
                case 5:
                    await RemoveBook();
                    break;
                case 6:
                    await ListBook();
                    break;
                case 7:
                    await ListAllBooks();
                    break;
            }
        }

        public static async Task RegisterUser()
        {
            _console.Write("------------------- Let's Add a New User ---------------------");
            _console.Write("Type Name");
            var userName = _console.Read<string>();
            _console.Write("Type Email");
            var userEmail = _console.Read<string>();
            if (await _userRepository.Exists(userEmail))
            {
                _console.Write("User Already Exists!");
                await RegisterUser();
            }
            else
            {
                User user = new User(userName, userEmail);
                bool success = await _userRepository.Add(user);
                if (success)
                {
                    _console.Write("User Registered!");
                }
                else
                {
                    _console.Write("Error While Registrating User!");
                    await RegisterUser();
                }
            }
        }

        public static async Task ListAllBooks()
        {
            //var books = DbOps.GetAllBooks();
            var books = await _bookRepository.FindAll();
            _console.Write($"List of All Libary Books:");
            foreach (var book in books)
            {
                _console.Write($"---------------------------------");
                _console.Write($"ID - {book.Id}");
                _console.Write($"Title - {book.Title}");
                _console.Write($"Author - {book.Author}");
                _console.Write($"Publish Year - {book.PublishYear}");
                _console.Write($"ISBN - {book.ISBN}");
                _console.Write($"---------------------------------");
            }
            _console.Write($"End of the List!");
        }

        public static async Task ListBook()
        {
            var book = await FindBook();
            _console.Write($"ID - {book.Id}");
            _console.Write($"Title - {book.Title}");
            _console.Write($"Author - {book.Author}");
            _console.Write($"Publish Year - {book.PublishYear}");
            _console.Write($"ISBN - {book.ISBN}");

        }

        public static async Task RegisterBook()
        {
            _console.Write("------------------- Let's Add a New Book ---------------------");
            _console.Write("Type Book Title");
            var Title = _console.Read<string>();
            _console.Write("Type Author");
            var Author = _console.Read<string>();
            _console.Write("Type ISBN");
            var ISBN = _console.Read<string>();
            _console.Write("Type Publish Year");
            int year = _console.Read<int>();

            if (await _bookRepository.Find(2, ISBN) != null)
            {
                _console.Write("Book Already Registered!");
                await RegisterBook();
            }
            else
            {
                Book book = new Book(Title, Author, ISBN, year);
                bool success = await _bookRepository.Add(book);
                if (success)
                {
                    _console.Write("Book Registered!");
                }
                else
                {
                    _console.Write("Error While Registrating Book!");
                    await RegisterBook();
                }
            }
        }


        public static async Task LoanBook()
        {
            _console.Write("------------------- Let's Register a Loan ---------------------");

            var user = await FindUser();

            var book = await FindBook();

            DateTime? loanDate = null;
            var customLoanDate = _console.yesOrNoBox("Set Custom Loan Date");
            if (customLoanDate)
            {
                loanDate = _console.Read<DateTime>();
            }

            int? loanPeriod = null;
            var customLoanPeriod = _console.yesOrNoBox("Set Custom Loan Period, standart is 90 days");
            if (customLoanPeriod)
            {
                loanPeriod = _console.Read<int>();
            }

            Loan loan = new Loan(user.Id, book.Id, loanDate, loanPeriod);

            bool success = await _loanRepository.Add(loan);
            if (success)
            {
                _console.Write("Book Loaned!");
            }
            else
            {
                _console.Write("Error While Loaning Book!");
                await LoanBook();
            }
        }

        public static async Task ReturnBook()
        {
            _console.Write("------------------- Let's Return a Book ---------------------");
            var user = await FindUser();
            var activeLoans = await _loanRepository.GetActiveLoansByUserId(user.Id);
            _console.Write("------------------- List of User's Loaned Books ---------------------");
            foreach (var loan in activeLoans)
            {
                _console.Write($"Loan Id: {loan.Id} - Book : {loan.Book.Title}");
            }
            _console.Write("Type the Id one of the loans above");
            int id = _console.Read<int>();
            while (!activeLoans.Any(a => a.Id.Equals(id)))
            {
                _console.Write("Please, choose a valid Id!");
                id = _console.Read<int>();
            }

            var selectedLoan = activeLoans.Where(a => a.Id.Equals(id)).FirstOrDefault();

            bool confirmReturn = _console.yesOrNoBox($"Confirm return of {selectedLoan.Book.Title} for {user.Name}");

            if (confirmReturn)
            {
                selectedLoan.BookReturned = true;
                var success = await _loanRepository.Alter(selectedLoan);
                if (success)
                {
                    _console.Write("Book Returned!");
                }
                else
                {
                    _console.Write("Error while returning!");
                }
            }

        }

        public static async Task RemoveBook()
        {
            var book = await FindBook();
            bool confirmRemove = _console.yesOrNoBox($"Confirm removing book {book.Title}?");
            if (confirmRemove)
            {
                var success = await _bookRepository.Remove(book);
                if (success)
                {
                    _console.Write("Book Removed!");
                }
                else
                {
                    _console.Write("Error while removing book!");
                }
            }
        }

        public static async Task<Book> FindBook()
        {
            _console.Write("Chose an Searcher for Book:");
            _console.Write("1. By Book Title");
            _console.Write("2. By ISBN");
            _console.Write("3. By Id");
            int command = _console.Read<int>();
            var maxEnumLenght = Enum.GetValues(typeof(SearchEnums.BookSearchCommand)).Length;
            while (command < 1 || command > maxEnumLenght)
            {

                _console.Write("Command should be between 1 and 3!");
                command = _console.Read<int>();
            }
            _console.Write("Enter searcher book value:");
            object param = _console.Read<object>();

            var book = await _bookRepository.Find(command, param);
            if (book != null)
            {
                return book;
            }
            else
            {
                _console.Write("Book Not Found!!!!");
                return await FindBook();
            }
        }

        public static async Task<User> FindUser()
        {
            _console.Write("Type User Email:");
            var user = await _userRepository.GetUserbyEmail(_console.Read<string>());
            while (user == null)
            {
                _console.Write("User Not Found!!!!");
                _console.Write("Type User Email:");
                user = await _userRepository.GetUserbyEmail(_console.Read<string>());
            }
            return user;
        }
    }
}
