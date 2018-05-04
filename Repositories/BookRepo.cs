using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Models;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;

        public BookRepo()
        {
            _db = new DataContext();
        }

        //getters and setters and yeeters

        public BookViewModel GetBook(int id)
        {
            var book = (from b in _db.Books
                        where b.ID == id
                        select new BookViewModel{
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            ISBN10 = b.ISBN10,
                            ISBN13 = b.ISBN13,
                            Description = b.Description,
                            NumberOfPages = b.NumberOfPages,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating
                        }).First();

            return book;
        }

        public List<BookViewModel> GetBooks(FilterModel filter)
        {
            var books = (from b in _db.Books
                         where b.Price >= filter.MinPrice && b.Price <= filter.MaxPrice
                         select new BookViewModel{
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            ISBN10 = b.ISBN10,
                            ISBN13 = b.ISBN13,
                            Description = b.Description,
                            NumberOfPages = b.NumberOfPages,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating
                         });

            if(filter.Genre != null)
            {
                books.Where(book => book.Genre.Equals(filter.Genre));
            }

            if(filter.SearchWord != null)
            {
                books.Where( (book => book.Title.ToLower().Contains(filter.SearchWord.ToLower())));
            }

            switch(filter.OrderBy) {
                case "AlphaUp":
                    books.OrderBy(book => book.Title);
                    break;
                case "AlphaDown":
                    books.OrderByDescending(book => book.Title);
                    break;
                case "PriceUp":
                    books.OrderBy(book => book.Price);
                    break;
                case "PriceDown":
                    books.OrderByDescending(book => book.Price);
                    break;
                case "RatingUp":
                    books.OrderBy(book => book.Rating);
                    break;
                case "RatingDown":
                    books.OrderByDescending(book => book.Rating);
                    break;
                default:
                    books.OrderBy(book => book.Title);
                    break;
            }

            if(filter.Amount != 0)
            {
                books.Take(filter.Amount);
            }

            var result = books.ToList();
            return result;
        }

        public List<BookViewModel> GetCartBooks(string userID)
        {
            var books = (from b in _db.Books
                         join c in _db.UserBookCartConnections on userID equals c.UserID
                         select new BookViewModel()
                         {
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            ISBN10 = b.ISBN10,
                            ISBN13 = b.ISBN13,
                            Description = b.Description,
                            NumberOfPages = b.NumberOfPages,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating
                         }
                        ).ToList();
            return books;
        }

        public List<BookViewModel> GetWishlistBooks(string userID)
        {
            var books = (from b in _db.Books
                         join c in _db.UserBookWishlistConnections on userID equals c.UserID
                         select new BookViewModel()
                         {
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            ISBN10 = b.ISBN10,
                            ISBN13 = b.ISBN13,
                            Description = b.Description,
                            NumberOfPages = b.NumberOfPages,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating
                         }
                        ).ToList();
            return books;
        }
    }
}