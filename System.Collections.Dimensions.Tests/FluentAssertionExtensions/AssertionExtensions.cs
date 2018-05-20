using System;
using System.Collections.Dimensions.TwoDimensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.Tests.FluentAssertionExtensions
{
    public static class AssertionExtensions
    {
        public static Index2dAssertion Should(this Index2d instance) => new Index2dAssertion(instance);

        public static Matrix2dAssertion<T> Should<T>(this IMatrix2d<T> instance) => new Matrix2dAssertion<T>(instance);
    }
}