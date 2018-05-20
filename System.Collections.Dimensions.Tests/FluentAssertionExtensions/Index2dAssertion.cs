using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System;
using System.Collections.Dimensions.TwoDimensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.Tests.FluentAssertionExtensions
{
    public class Index2dAssertion : ReferenceTypeAssertions<Index2d, Index2dAssertion>
    {
        public Index2dAssertion(Index2d instance)
        {
            Subject = instance;
        }

        protected override string Identifier => "index2d";

        public AndConstraint<Index2dAssertion> Be(
            int x, int y, string because = "", params object[] becauseArgs)
        {
            using (new AssertionScope(nameof(Index2d)))
            {
                Subject.X.Should().Be(x, because, becauseArgs);
                Subject.Y.Should().Be(y, because, becauseArgs);
            }

            return new AndConstraint<Index2dAssertion>(this);
        }
    }
}