using TheBookCave.Models;

namespace TheBookCave.Data.EntityModels.BookEntityModel
{
    public class BookEntityModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public DateModel DatePublished { get; set; }
        public string Publisher { get; set; }
        public double Rating { get; set; }
        public int NumberOfRatings { get; set; }
        public int NumberOfCopiesSold { get; set; }
        public int InStock { get; set; }
    }
}
