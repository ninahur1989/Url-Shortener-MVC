using UrlShorter.Models;

namespace UrlShorter.Services.Interfaces
{
    public interface IHomeService
    {
        void UpdateAbout(AboutAlgoritm algoritm);
        AboutAlgoritm GetAbout();
    }
}
