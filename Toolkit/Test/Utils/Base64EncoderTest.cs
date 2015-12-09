using NUnit.Framework;
using Orasi.Toolkit.Utils;

namespace Orasi.Toolkit.Test.Utils
{
    class Base64EncoderTest
    {
        [Test]
        public void Encode()
        {
             Assert.True(Base64Encoder.Encode("String to encode").Equals("U3RyaW5nIHRvIGVuY29kZQ=="));
        }

        [Test]
        public void Decode()
        {
            Assert.True(Base64Encoder.Decode("U3RyaW5nIHRvIGVuY29kZQ==").Equals("String to encode"));
        }
    }
}
