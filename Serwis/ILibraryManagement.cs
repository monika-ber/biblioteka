namespace Library
{
    interface ILibraryManagement
    {
        public void CreateMedia();
        public void CreateUsers();
        public void AddMedium(Medium medium);
        public void DeleteMedium(int idMedium);
        public void AddUser(User user);
        public void DeleteUser(int idUser);
        public void DisplayAllMedia();
        public void DisplayAllUsers();
        public void DisplayBorrowedBooks(int userID);
        public void BorrowMedium(int userID, int idMedium);
        public void ReturnMedium(int userID, int idMedium);
    }
}