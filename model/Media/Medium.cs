namespace Library
{
    abstract class Medium : IMedium
    {
        public string? Title { get; set; }
        public int YearOfPublication { get; set; }
        public bool IsBorrowed { get; set; }
        protected static int biggestID = 0;
        public Medium(){}
        public Medium (string title, int yearOfPublication)
        {
            Title = title;
            YearOfPublication = yearOfPublication;
            IsBorrowed = false;
        }
        public abstract int GetID();
        public abstract void SetAuthor(string? author);
    }
}