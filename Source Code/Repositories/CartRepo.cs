using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class CartRepo
    {
        //Private variable to manipulate database.
        private DataContext _db;

        //Constructor that initialiezes database.
        public CartRepo()
        {
            _db = new DataContext();
        }

        //Adds Book to Cart.
        public void AddCartItem(CartInputModel model)
        {
            //Test if Connection is made.
            var allConnections = (from c in _db.UserBookCartConnections
                                  where c.UserID == model.UserID
                                  select c);

            int amountInCart = 0;
            for(int i = 0; i < allConnections.Count(); i++)
            {
                amountInCart += allConnections.ToList().ElementAt(i).Amount;
            }

            if(amountInCart >= 64)
            {
                //Cant add if cart is more than 64
                return;
            }

            var connection = allConnections.Where(c => c.BookID == model.BookID).FirstOrDefault();

            //If connection is established, increase the amount.
            if(connection != null)
            {
                connection.Amount++;
                _db.Update(connection);
                _db.SaveChanges();
            }
            //If connection is not established, it will be formed.
            else
            {
                var newCartItem = new UserBookCartConnectionEntityModel()
                {
                    UserID = model.UserID,
                    BookID = model.BookID,
                    Amount = 1,
                };
                _db.Add(newCartItem);
                _db.SaveChanges();
            }
        }

        //Returns all book in the Cart.
        public List<BookViewModel> GetCartBooks(string userID)
        {
            var books = (from b in _db.Books
                         join c in _db.UserBookCartConnections on b.ID equals c.BookID
                         where c.UserID == userID
                         select new BookViewModel()
                         {
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            Description = b.Description,
                            Price = b.Price,
                            NumberOfPages = b.NumberOfPages,
                            NumberOfCopiesSold = b.NumberOfCopiesSold,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating,
                            Image = b.Image,
                            Amount = c.Amount,
                         }
                        ).ToList();

            return books;
        }

        //Updates Amount of Book in cart.
        public void UpdateConnection(CartInputModel model)
        {
            var allConnections = (from c in _db.UserBookCartConnections
                              where c.UserID == model.UserID
                              select c);

            int amountInCart = 0;
            for(int i = 0; i < allConnections.Count(); i++)
            {
                int bookAmount = allConnections.ToList().ElementAt(i).Amount;
                if(allConnections.ToList().ElementAt(i).BookID == model.BookID)
                {
                    bookAmount = model.Amount;
                }
                amountInCart += bookAmount;
            }

            if(amountInCart > 64)
            {
                //Cart amount cannot exceed 64;
                return;
            }

            var connection = allConnections.Where(c => c.BookID == model.BookID).FirstOrDefault();

            if(model.Amount > 0)
            {
                connection.Amount = model.Amount;
                _db.UserBookCartConnections.Update(connection);
                _db.SaveChanges();
            }
            else
            {
                //NOT ALLOWED TO PUT BOOKS BELOW ZERO
            }
        }

        //Removes Cart Item.
        public void RemoveItem(string userID, int bookID)
        {
            var connection = (from c in _db.UserBookCartConnections
                              where c.UserID == userID && c.BookID == bookID
                              select c).FirstOrDefault();
            
            _db.UserBookCartConnections.Remove(connection);
            _db.SaveChanges();
        }

        //When order is finalized, The cart is emptied to make space for the next order.
        public void DeleteCartFinalizeOrder(int orderID)
        {
            var cartBooks = (from ubc in _db.UserBookCartConnections
                             join obc in _db.OrderBookConnections on ubc.BookID equals obc.BookID
                             where obc.OrderID == orderID
                             select ubc).ToList();
            
            _db.RemoveRange(cartBooks);
            _db.SaveChanges();
        }
    }
}