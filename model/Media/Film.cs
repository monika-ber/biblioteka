namespace Library
{
    class Film : Medium
    {
        public int filmID{ get; private set; }
        public string? Director { get; set; }
        public Film(){
            biggestID++;
            filmID = biggestID;
        }
        public Film(string title, int yearOfPublication, string director) : base(title, yearOfPublication)
        {
            Director = director;
            biggestID++;
            filmID = biggestID;
        }
        public override int GetID()
        {
            return filmID;
        }

        public override void SetAuthor(string? author)
        {
            Director = author;
        }
    }
}