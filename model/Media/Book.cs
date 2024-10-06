namespace Library
{
    class Book : Medium
    {
        public int ISBN{ get; private set; }
        public string? Author { get; set; }
        public Book(){
            biggestID++;
            ISBN = biggestID;
        }
        public Book(string title, int yearOfPublication, string author) : base(title, yearOfPublication)
        {
            Author = author;
            biggestID++;
            ISBN = biggestID;
        }
        public override int GetID()
        {
            return ISBN;
        }

        public override void setAuthor(string? author)
        {
            Author = author;
        }
    }
}