using Pustok_MVC.Data;
using Pustok_MVC.Models;

namespace Pustok_MVC.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public Dictionary<String, String> GetSettings()
        {
            return _context.Settings.ToDictionary(x=>x.Key, x => x.Value);
        }
    }
}
