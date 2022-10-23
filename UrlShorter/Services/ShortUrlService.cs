using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShorter.Data;
using UrlShorter.Models;
using UrlShorter.Services.Interfaces;

namespace UrlShorter.Services
{
    public sealed class ShortUrlService : IShortUrlService
    {
        private readonly AppDbContext _context;
        private readonly IEncodeService _encodeservice;

        public ShortUrlService(AppDbContext context, IEncodeService encodeservice)
        {
            _context = context;
            _encodeservice = encodeservice;
        }

        public ShortUrl GetById(int id)
        {
            var shortUrl = _context.ShortUrls.Include(c => c.User).FirstOrDefault(x => x.Id == id);
            return shortUrl;
        }

        public List<ShortUrl> GetAll()
        {
            return _context.ShortUrls.Include(c => c.User).ToList();
        }

        public ShortUrl GetByPath(string path)
        {
            return _context.ShortUrls.Find(_encodeservice.Decode((path)));
        }

        public ShortUrl GetByOriginalUrl(string originalUrl)
        {
            foreach (var shortUrl in _context.ShortUrls) 
            {
                if (shortUrl.OriginalUrl == originalUrl) 
                {
                    return shortUrl;
                }
            }

            return null;
        }

        public int Save(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);
            _context.SaveChanges();

            return shortUrl.Id;
        }

        public bool AlreadyExsist(string originUrl)
        {
            if (_context.ShortUrls.FirstOrDefault(x => x.OriginalUrl == originUrl) != null)
                return true;

            return false;
        }

        public bool DeleteUrl(ShortUrl url)
        {
            if (_context.ShortUrls.Remove(url) != null)
            {
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public void DeleteAllUrls()
        {
            var allUrls = GetAll();
            _context.ShortUrls.RemoveRange(allUrls);
            _context.SaveChanges();
        }
    }
}
