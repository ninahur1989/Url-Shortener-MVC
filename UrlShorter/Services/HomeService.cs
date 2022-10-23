using UrlShorter.Data;
using UrlShorter.Models;
using UrlShorter.Services.Interfaces;
using System.Linq;

namespace UrlShorter.Services
{
    public sealed class HomeService : IHomeService
    {
        private readonly AppDbContext _context;

        public HomeService(AppDbContext context)
        {
            _context = context;
        }

        public void UpdateAbout(AboutAlgoritm algoritm)
        {
            var item = _context.AboutAlgoritms.FirstOrDefault();
            item.Description = algoritm.Description;
            _context.SaveChanges();
        }

        public AboutAlgoritm GetAbout()
        {
            return _context.AboutAlgoritms.FirstOrDefault();
        }
    }
}
