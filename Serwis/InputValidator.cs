using System.Text.RegularExpressions;
using Microsoft.Win32.SafeHandles;
namespace Library
{
    class InputValidator
    {
        /// <summary>
        /// Validate elements classified as Name (FirstName and LastName)
        /// </summary>
        /// <param name="name">FirstName or LastName input</param>
        /// <returns>True if element matches Regex</returns>
        public static bool ValidateName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]{2,}$");
        }

        /// <summary>
        /// Validate elements classified as Surname
        /// </summary>
        /// <param name="name">Author or Director input</param>
        /// <returns>True if element matches Regex</returns>
        public static bool ValidateSurname(string surname)
        {
            return Regex.IsMatch(surname, @"^[a-zA-Z\s]{2,}$");
        }

        /// <summary>
        /// Validate elements classified as Title
        /// </summary>
        /// <param name="name">Title input</param>
        /// <returns>True if element matches Regex</returns>
        public static bool ValidateTitle(string title)
        {
            return Regex.IsMatch(title, @"^[a-zA-Z0-9\s!@#$%^&*()]+$");
        }

        /// <summary>
        /// Validate elements classified as Date
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="dateValue">DateTime value returned</param>
        /// <returns></returns>
        public static bool ValidateDate(string input, out DateTime dateValue)
        {
            bool isValid;
            dateValue = DateTime.Now;
            if (!input.Contains('-') || input.Length != 10)
            {
                return false;
            }
            isValid = DateTime.TryParse(input, out dateValue);
            return isValid;
        }

        /// <summary>
        /// Validated user input
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="parameter">Element classification</param>
        /// <returns></returns>
        public static bool IsValidInput(string? input, string parameter)
        {
            bool isValid;

            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            } 
            
            switch(parameter)
            {
                case "Name":
                    isValid = ValidateName(input);
                    return isValid;
                case "Type":
                    isValid = input == "1" || input == "2" ? true : false;
                    return isValid;
                case "Surname":
                    isValid = ValidateSurname(input);
                    return isValid;
                case "Title":
                    isValid = ValidateTitle(input);
                    return isValid;
                case "Date":
                    DateTime dt;
                    isValid = ValidateDate(input, out dt);
                    if (dt == DateTime.Now)
                    {
                        return false;
                    } 
                    else 
                    {
                        return isValid;
                    }
                default: 
                    Console.WriteLine("Incorrect parameter name");
                    break;
            }
            return true;
        }
    }
}