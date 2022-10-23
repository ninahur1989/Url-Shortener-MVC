using System.Collections.Generic;

namespace UrlShorter.Services.Interfaces
{
    public interface IEncodeService
    {
        string Encode(int num);

        int Decode(string str);
    }
}
