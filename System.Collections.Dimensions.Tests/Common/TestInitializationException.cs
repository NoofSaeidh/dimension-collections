using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.Tests.Common
{
    [Serializable]
    public class TestInitializationException : Exception
    {
        public TestInitializationException()
        {
        }

        public TestInitializationException(string message) : base(message)
        {
        }

        public TestInitializationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TestInitializationException(
          Runtime.Serialization.SerializationInfo info,
          Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}