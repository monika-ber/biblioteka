using System.Net.Http.Headers;

namespace Library
{
    class LibraryManagement : ILibraryManagement
    {
        Library library = new Library();

        /// <summary>
        /// Add Medium if it doesn't exist in media collection (library has a limit of one media)
        /// </summary>
        /// <param name="medium">Medium type object that user wants to add to collection</param>
        public void AddMedium(Medium medium)
        {
            try
            {
                //Library can have only one copy of a book
                if (!BuisnessLogicLayer.CheckIfMediumExistsInDatabase(library.Media, medium))
                {
                    library.Media.Add(medium);
                    Console.WriteLine("Medium was added.");
                } else 
                {
                    Console.WriteLine("The library already has copy of this element");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was thrown: " + e.Message);
            }

        }

        /// <summary>
        /// Deletes Medium type object if it exists in media collection
        /// </summary>
        /// <param name="idMedium">Id of Medium type object</param>
        public void DeleteMedium(int idMedium)
        {
            try
            {
                if (BuisnessLogicLayer.CheckIfMediumExistsInDatabase(library.Media, idMedium) &&
                    !BuisnessLogicLayer.IsMediumBorrowed(library.Users, idMedium))
                {
                    library.Media.RemoveAll(m => m.GetID() == idMedium);
                    Console.WriteLine("Medium was deleted.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Medium with the given id number was not found", e.Message);
            }
        }

        /// <summary>
        /// Adds user to user collection if the same object doesn't exist in library and if user is over 13
        /// </summary>
        /// <param name="user">User type object</param>
        public void AddUser(User user)
        {
            try
            {
                if (!BuisnessLogicLayer.CheckIfUserExists(library.Users, user) && 
                    BuisnessLogicLayer.IsUserOver13(user))
                {
                    library.Users.Add(user);
                    Console.WriteLine("User was added to the database.");
                } 
                else 
                {
                    Console.WriteLine("User was not created");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("User was not created " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was thrown. " + e.Message);
            }
            
        }

        /// <summary>
        /// Deletes user if it exists in user collection
        /// </summary>
        /// <param name="userID">ID of User type object</param>
        public void DeleteUser(int userID)
        {
            try
            {
                if (BuisnessLogicLayer.CheckIfUserExists(library.Users, userID))
                {
                    library.Users.Remove(library.Users.First(u => u.UserID == userID));
                    Console.WriteLine("User was removed from the database");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"User with this ID was not found userID = {userID} " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was thrown. " + e.Message);
            }
        }

        /// <summary>
        /// Displays information about Medium type object
        /// </summary>
        /// <param name="medium">Medium type object</param>
        public void DisplayMedium(Medium medium)
        {
            if (medium is Book book)
            {
                Console.WriteLine($"Book {book.ISBN} is titled {book.Title} and was written by "
                + $"{book.Author} in {book.YearOfPublication}. Its borrowed state: {book.IsBorrowed}");
            }
            else if (medium is Film film)
            {
                Console.WriteLine($"Film {film.filmID} is titled {film.Title} and was directed by "
                + $"{film.Director} in {film.YearOfPublication}. Its borrowed state: {film.IsBorrowed}");
            }
        }

        /// <summary>
        /// Displays information about every object in media collection
        /// </summary>
        public void DisplayAllMedia()
        {
            if (library.Media.Count > 0)
            {
                foreach (var item in library.Media)
                {
                    DisplayMedium(item);
                }
            } else 
            {
                Console.WriteLine("There is no media in database.");
            }
        }

        /// <summary>
        /// Displays information about every object in users collection
        /// </summary>
        public void DisplayAllUsers()
        {
            if (library.Users.Count > 0) 
            {
                foreach (User user in library.Users)
                {
                    Console.WriteLine($"User {user.UserID}: {user.FirstName} {user.LastName}. Born on "
                    + user.DateOfBirth.ToString("MM-dd-yyyy"));
                }
            } else 
            {
                Console.WriteLine("There are no users in database.");
            }
        }

        /// <summary>
        /// Displays media borrowed by specified user
        /// </summary>
        /// <param name="userID"></param>
        public void DisplayBorrowedBooks(int userID)
        {
            try
            {
                User? user = BuisnessLogicLayer.ReturnUserIfExists(library.Users, userID);
                if (user != null && user.borrowedMedia.Count != 0) {
                    foreach (Medium item in user.borrowedMedia)
                    {
                        DisplayMedium(item);
                    }
                } else {
                    Console.WriteLine("This user doesn't have any borrowed books.");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"User with this ID was not found userID = {userID} " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was thrown. " + e.Message);
            }
            
        }

        /// <summary>
        /// Allows to lent medium to user with specified ID
        /// </summary>
        /// <param name="userID">ID of user that wants to borrow medium</param>
        /// <param name="idMedium">ID of medium that user wants to borrow</param>
        public void BorrowMedium(int userID, int idMedium)
        {
            try
            {
                if (BuisnessLogicLayer.CheckIfMediumExistsInDatabase(library.Media, idMedium) && 
                    !BuisnessLogicLayer.IsMediumBorrowed(library.Users, idMedium) &&
                    BuisnessLogicLayer.CheckUserAtMaxMediaCount(library.Users, userID, 3))
                    {
                        User? user = BuisnessLogicLayer.ReturnUserIfExists(library.Users, userID);
                        Medium borrowedItem = library.Media.First(b => b.GetID() == idMedium);
                        borrowedItem.IsBorrowed = true;
                        user?.borrowedMedia.Add(borrowedItem);
                        Console.WriteLine($"Book with ID={idMedium} was lent to user {userID}");
                    }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"User with userID={userID} or media with mediaID={idMedium} was not found" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was thrown. " + e.Message);
            }
        }

        /// <summary>
        /// Allows user to return borrowed medium to the library
        /// </summary>
        /// <param name="userID">ID of user that returns a medium</param>
        /// <param name="idMedium">ID of returned medium</param>
        public void ReturnMedium(int userID, int idMedium)
        {
            try
            {
                if (BuisnessLogicLayer.CheckIfUserExists(library.Users, userID) &&
                    BuisnessLogicLayer.CheckIfMediumExistsInDatabase(library.Media, idMedium) && 
                    BuisnessLogicLayer.IsMediumBorrowed(library.Users, idMedium, userID))
                {
                    User? user = BuisnessLogicLayer.ReturnUserIfExists(library.Users, userID);
                    user?.borrowedMedia.RemoveAll(b => b.GetID() == idMedium);
                    Medium returnedItem = library.Media.First(b => b.GetID() == idMedium);
                    returnedItem.IsBorrowed = false;
                    Console.WriteLine("Medium has been returned to the library");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"User with userID={userID} or media with mediaID={idMedium} was not found" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was thrown. " + e.Message);
            }
        }

        /// <summary>
        /// Create a collection of Medium type objects
        /// </summary>
        public void CreateMedia()
        {
            library.Media.Add(new Book("Bell Jar", 1963, "Sylvia Plath"));
            library.Media.Add(new Book ("Lśnienie", 1983, "Stephen King"));
            library.Media.Add(new Book("Harry Potter", 1998, "J.K. Rowling"));
            library.Media.Add(new Film("Joker", 2023, "Todd Phillips"));
            library.Media.Add(new Film("Mad max", 1979, "George Miller"));
        }

        /// <summary>
        /// Create a collection of User type objects
        /// </summary>
        public void CreateUsers()
        {
            library.Users.Add(new User("Stefan", "Piernik", new DateTime(1989,12,3)));
            library.Users.Add(new User("Karolina", "Miarka", new DateTime(1965,02,15)));
            library.Users.Add(new User("Dariusz", "Nowak", new DateTime(1935,07,12)));
        }
    }
}