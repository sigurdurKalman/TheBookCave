using Microsoft.AspNetCore.Mvc;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        private RatingService _ratingService;

        public IActionResult Index()
        {
            return RedirectToAction("Catalogue", "Home");
        }

        public IActionResult Details(int bookID)
        {
            //This function takes in a bookID parameter which is the ID of the book that should have details.
            if(bookID == 0)
            {
                //If a book is not specified, redirect to the catalogue
                RedirectToAction("Home/Catalogue");
            }
            //var book = getBook(bookID);
            //should return View with book as parameter
            return View();
        }

        public IActionResult Rating(int bookID)
        {
            //This function takes in a bookID parameter which is the ID of the book that should have ratings.
            if(bookID == 0)
            {
                //If a book is not specified, redirect to the catalogue
                RedirectToAction("Home/Catalogue");
            }
            //var book = getBook(bookID);
            //should return View with book as parameter
            return View();
        }

        public IActionResult Edit(int bookID)
        {
            //This function takes in a bookID parameter which is the ID of the book that should be edited.
            if(bookID == 0)
            {
                //If a book is not specified, redirect to the catalogue
                RedirectToAction("Home/Catalogue");
            }
            //var book = getBook(bookID);
            //should return View with book as parameter
            return View();
        }
    }
}