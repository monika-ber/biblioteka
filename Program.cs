using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Library
{
    class Program
    {
        static void Main()
        {
            LibraryManagement library = new LibraryManagement();
            library.CreateMedia();
            library.CreateUsers();
            
            Console.WriteLine("Welcome to the library management system.");
            DisplayInstructions();
            ReadUserInput(out int userInput);

            while(true)
            {
                switch(userInput)
                {
                    case 1:
                        Console.WriteLine("Please input a type of medium you wish add to database " +
                        "1 - book, 2 - film");
                        string? type = ReadUserInput(Console.ReadLine(), "Type");
                        Medium? medium = CreateMediumObject(type);
                        Console.WriteLine("Please input a title of added medium");
                        medium.Title = ReadUserInput(Console.ReadLine(), "Title");
                        Console.WriteLine("Please input a production year of added medium");
                        ReadUserInput(out int year);
                        medium.YearOfPublication = year;
                        Console.WriteLine("Please input an author of added medium");
                        medium.setAuthor(ReadUserInput(Console.ReadLine(), "Surname"));
                        library.AddMedium(medium);
                        Console.WriteLine("Medium was added.");
                        break;
                    case 2:
                        Console.WriteLine("Please input an ID of medium to delete.");
                        ReadUserInput(out int mediumID);
                        library.DeleteMedium(mediumID);
                        break;
                    case 3:
                        Console.WriteLine("Please input First Name.");
                        string? firstName = ReadUserInput(Console.ReadLine(), "Name");
                        Console.WriteLine("Please input Last Name.");
                        string? lastName = ReadUserInput(Console.ReadLine(), "Name");
                        Console.WriteLine("Please input date of birth (in format: yyyy-mm-dd)");
                        string? dateOfBirth = ReadUserInput(Console.ReadLine(), "Date");
                        InputValidator.ValidateDate(dateOfBirth, out DateTime birthDate);
                        library.AddUser(new User(firstName, lastName, birthDate));
                        break;
                    case 4:
                        Console.WriteLine("Please input an ID of a user you wish to delete.");
                        ReadUserInput(out int userID);
                        library.DeleteUser(userID);
                        break;
                    case 5:
                        Console.WriteLine("List of all media in the library");
                        library.DisplayAllMedia();
                        break;
                    case 6:
                        Console.WriteLine("List of all users");
                        library.DisplayAllUsers();
                        break;
                    case 7:
                        Console.WriteLine("Please input ID of user you wish to find");
                        ReadUserInput(out userID);
                        library.DisplayBorrowedBooks(userID);
                        break;
                    case 8:
                        Console.WriteLine("Please input ID of a user.");
                        ReadUserInput(out userID);
                        Console.WriteLine("Please input ID of media to lent");
                        ReadUserInput(out mediumID);
                        library.BorrowMedium(userID, mediumID);
                        break;
                    case 9:
                        Console.WriteLine("Please input ID of a user.");
                        ReadUserInput(out userID);
                        Console.WriteLine("Please input ID of media to return");
                        ReadUserInput(out mediumID);
                        library.ReturnMedium(userID, mediumID);
                        break;
                    case 10:
                        return;
                    default:
                        Console.WriteLine("Invalid operation");
                        break;
                }

                DisplayInstructions();
                ReadUserInput(out userInput);
            }
        }

        /// <summary>
        /// Display instructions for user interface
        /// </summary>
        static void DisplayInstructions()
        {
            Console.WriteLine("Wybierz instrukcję: ");
            Console.WriteLine("1 - Add media to the system");
            Console.WriteLine("2 - Remove media from the system");
            Console.WriteLine("3 - Add a user");
            Console.WriteLine("4 - Remove user");
            Console.WriteLine("5 - Display all media in the system");
            Console.WriteLine("6 - Display all users");
            Console.WriteLine("7 - Display all media borrowed by the selected user");
            Console.WriteLine("8 - Borrow media");
            Console.WriteLine("9 - Return media");
            Console.WriteLine("10 - Close the application");
        }

        /// <summary>
        /// Create an object of Book type or Film type
        /// </summary>
        /// <param name="input">string with value 1 or 2</param>
        /// <returns>Book or Film object type</returns>
        static Medium? CreateMediumObject(string input)
        {
            Enum typ = (Type)Enum.Parse(typeof(Type), input);
            switch(typ)
            {
                case Type.Book:
                    Book book = new Book();
                    return book;
                case Type.Film:
                    Film film = new Film();
                    return film;
                default:
                    return default;
            }
        }

        /// <summary>
        /// Wait until user inputs correct intereger value
        /// </summary>
        /// <param name="input">User input</param>
        static void ReadUserInput(out int input)
        {
            while(!int.TryParse(Console.ReadLine(), out input) && input < 0)
            {
                Console.WriteLine("Incorrect format of input. Please input a integer number");
            }
        }

        /// <summary>
        /// Wait until user input correct string value
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="parameter">Type of input</param>
        /// <returns>User input when it's in correct form</returns>
        static string? ReadUserInput(string? input, string parameter)
        {
            while(!InputValidator.IsValidInput(input, parameter))
            {
                Console.WriteLine("Incorrect format of input.");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}