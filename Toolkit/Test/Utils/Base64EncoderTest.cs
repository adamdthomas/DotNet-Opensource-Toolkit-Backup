using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orasi.Toolkit.Utils;

namespace Orasi.Toolkit.Test.Utils
{
    class Base64EncoderTest
    {
        [Test]
        public void encode()
        {
             Assert.True(Base64Encoder.encode("String to encode").Equals("U3RyaW5nIHRvIGVuY29kZQ=="));
        }

        [Test]
        public void decode()
        {
            Assert.True(Base64Encoder.decode("U3RyaW5nIHRvIGVuY29kZQ==").Equals("String to encode"));
        }
    }
}
