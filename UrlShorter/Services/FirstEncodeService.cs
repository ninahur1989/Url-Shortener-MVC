using System.Linq;
using System.Text;
using UrlShorter.Services.Interfaces;

namespace UrlShorter.Services
{
    public sealed class FirstEncodeService : IEncodeService
    {
        private const string Alphabet = "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_";
        private readonly int _base = Alphabet.Length;

        public string Encode(int num)
        {
            var sb = new StringBuilder();

            while (num > 0)
            {
                sb.Insert(0, Alphabet.ElementAt(num % _base));
                num = num / _base;
            }

            return sb.ToString();
        }

        public int Decode(string str)
        {
            var num = 0;

            for (var i = 0; i < str.Length; i++)
            {
                num = num * _base + Alphabet.IndexOf(str.ElementAt(i));
            }

            return num;
        }
    }
}
