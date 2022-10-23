using System;
using System.Collections.Generic;
using UrlShorter.Services;
using Xunit;

namespace UrlShorter.Test
{
    public class FirstEncodeServiceTest
    {
        private readonly FirstEncodeService _service = new FirstEncodeService();

        [Fact]
        public void Encode_ValidResult_ReturnString()
        {
            var resultService = _service.Encode(1);

            Assert.Equal("3", resultService);
        }

        [Fact]
        public void Encode_ValidCount_ReturnCountWithoutDuplicates()
        {
            HashSet<string> list = new HashSet<string>();
            int count = 100000;

            for (int i = 0; i < count; i++)
            {
                var resultService = _service.Encode(i);
                list.Add(resultService);
            }

            Assert.Equal(count, list.Count);
        }

        [Fact]
        public void Decode_ValidCount_ReturnCountWithoutDuplicates()
        {
            HashSet<string> listEncode = new HashSet<string>();
            HashSet<int> listDecode = new HashSet<int>();
            int count = 100000;

            for (int i = 0; i < count; i++)
            {
                var resultService = _service.Encode(i);
                listEncode.Add(resultService);
            }

            foreach(var x in listEncode)
            {
                listDecode.Add(_service.Decode(x));
            }

            Assert.Equal(listDecode.Count, listEncode.Count);
        }

        [Fact]
        public void Decode_ValidResult_ReturnId()
        {
            var resultService = _service.Decode("3");

            Assert.Equal(1, resultService);
        }
    }
}
