namespace Library
{
    class BuisnessLogicLayer
    {
        //Methods for media collection

        /// <summary>
        /// Checks if provided Medium type object exists in collection
        /// </summary>
        /// <param name="media">Collection of Medium type objects</param>
        /// <param name="medium">Medium type object</param>
        /// <returns></returns>
        public static bool CheckIfMediumExistsInDatabase(List<Medium> media, Medium medium)
        {
            Medium? m = media.FirstOrDefault(m => m.Title == medium.Title &&
                                                  m.YearOfPublication == medium.YearOfPublication &&
                                                  m.GetID() == medium.GetID());
            if (m != default)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if medium with the provided ID exists in collection
        /// </summary>
        /// <param name="media">Collection of Medium type objects</param>
        /// <param name="mediumID">Provided medium ID</param>
        /// <returns></returns>
        public static bool CheckIfMediumExistsInDatabase(List<Medium> media, int mediumID)
        {
            Medium? m = media.First(m => m.GetID() == mediumID);
            if (m != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if in collection of Users there is any user with borrowed medium with idMedium
        /// </summary>
        /// <param name="users">Collection of User type objects</param>
        /// <param name="idMedium">ID of searched medium</param>
        /// <returns>True if medium is borrowed by any user</returns>
        public static bool IsMediumBorrowed(List<User> users, int idMedium)
        {
            User? o = users.FirstOrDefault(u => u.borrowedMedia.Any(b => b.GetID() == idMedium && b.IsBorrowed));
            if (o != default)
            {
                Console.WriteLine($"Medium with ID= {idMedium} is already lent to someone.");
                return true;
            } 
            return false;
        }

        /// <summary>
        /// Checks if in collection of Users exists User with User ID that has borrowed medium with
        /// mediumID=idMedium
        /// </summary>
        /// <param name="users">Collection of User type objects</param>
        /// <param name="idMedium">ID of searched medium</param>
        /// <param name="UserID">Id of searched user</param>
        /// <returns>True if medium is borrowed by user</returns>
        public static bool IsMediumBorrowed(List<User> users, int idMedium, int UserID)
        {
            User? o = users.FirstOrDefault(u => u.UserID == UserID &&
                            u.borrowedMedia.Any(b => b.GetID() == idMedium && b.IsBorrowed));
            if (o != default)
            {
                return true;
            } 
            Console.WriteLine($"Medium with ID= {idMedium} is already lent to other user " +
            "or it wasn't borrowed by anyone");
            return false;
        }
        //-----------------------------------------------------------------------------------------
        //Methods for users collection

        /// <summary>
        /// Checks if user with the same userID exists in searched collection
        /// </summary>
        /// <param name="users">Collection of User type objects</param>
        /// <param name="userID">ID property of User type object</param>
        /// <returns>True if User with the same data exists in collection</returns>
        public static bool CheckIfUserExists(List<User> users, int userID)
        {
            User? user = users.First(u => u.UserID == userID);
            if (user != null)
            { 
                return true;
            }
            Console.WriteLine($"User with userID={userID} does not exist in the database");
            return false;
        }

        /// <summary>
        /// Checks if user with the same data exists in searched collection
        /// </summary>
        /// <param name="users">Collection of User type objects</param>
        /// <param name="user">User type object</param>
        /// <returns>True if User with the same data exists in collection</returns>
        public static bool CheckIfUserExists(List<User> users, User user)
        {
            User? u = users.FirstOrDefault(u => u.FirstName == user.FirstName &&
                                                u.LastName == user.LastName &&
                                                u.DateOfBirth == user.DateOfBirth);
            if (u != default)
            {
                Console.WriteLine("User with the same data already exists");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if user exists in collection and returns User type object
        /// </summary>
        /// <param name="users">Collection of User type objects</param>
        /// <param name="userID">ID of searched user</param>
        /// <returns>Uset type object with provided ID</returns>
        public static User? ReturnUserIfExists(List<User> users, int userID)
        {
            if (CheckIfUserExists(users, userID))
            { 
                return users.First(u => u.UserID == userID);
            }
            Console.WriteLine("User was not found, returning default objet");
            return default;
        }

        /// <summary>
        /// Checks if user is younger than 13 years old
        /// </summary>
        /// <param name="user">User object with properties: FirstName, LastName, DateOfBirth</param>
        /// <returns>True if User is younger than 13 years old, False if not</returns>
        public static bool IsUserOver13(User user)
        {
            int years = DateTime.Now.Year - user.DateOfBirth.Year;
            if (DateTime.Now > user.DateOfBirth.AddYears(years)) 
            {
                years++;
            }
            if (years > 13)
            {
                return true;
            }
            Console.WriteLine("Cannot create user younger than 13 years old.");
            return false;
        }

        /// <summary>
        /// Checks if User is already at capacity of borrowed media
        /// </summary>
        /// <param name="users">Collection of User type objects</param>
        /// <param name="userID">Provided Uses ID</param>
        /// <param name="capacity">Limit of borrowed media</param>
        /// <returns></returns>
        public static bool CheckUserAtMaxMediaCount(List<User> users, int userID, int capacity)
        {
            User? user = ReturnUserIfExists(users, userID);
            if (user?.borrowedMedia.Count == capacity)
            {
                Console.WriteLine("Unable to lent more media. User already at capacity.");
                return false;
            }
            else 
            {
                return true;
            }
        }
    }
}