namespace Library
{
    class User
    {
        public int UserID { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        private static int biggestID = 0;
        public List<Medium> borrowedMedia = new List<Medium>();
        public User(string firstname, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstname;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            biggestID++;
            UserID = biggestID;
        }
    }
}