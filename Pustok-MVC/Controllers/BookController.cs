using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_MVC.Data;
using Pustok_MVC.Models;

namespace Pustok_MVC.Controllers
{
    public class BookController:Controller
    {
        private readonly AppDbContext _context;
        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult GetBookById(int id)
        {
            Book book = _context.Books.Include(x => x.Genre).Include(x => x.BookImages.Where(x => x.PosterStatus==true)).FirstOrDefault(x=>x.Id == id);
            return PartialView("_BookModalPartial", book);
        }
    }
}
