using System.Collections.Generic;
using UrlShorter.Models;

namespace UrlShorter.Services.Interfaces
{
    public interface IShortUrlService
    {
        ShortUrl GetById(int id);

        List<ShortUrl> GetAll();

        ShortUrl GetByPath(string path);

        ShortUrl GetByOriginalUrl(string originalUrl);

        int Save(ShortUrl shortUrl);

        bool AlreadyExsist(string originUrl);

        bool DeleteUrl(ShortUrl url);

        void DeleteAllUrls();
    }
}
