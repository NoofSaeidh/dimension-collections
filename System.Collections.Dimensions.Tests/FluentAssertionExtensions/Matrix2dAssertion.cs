using FluentAssertions;
using FluentAssertions.Common;
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
    public class Matrix2dAssertion<T> : ReferenceTypeAssertions<IMatrix2d<T>, Matrix2dAssertion<T>>
    {
        public Matrix2dAssertion(IMatrix2d<T> instance)
        {
            Subject = instance;
        }

        protected override string Identifier => "matrix2d";

        public AndWhichConstraint<Matrix2dAssertion<T>, object> HaveElementAt(
            int x, int y, object element, string because = "", params object[] becauseArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:collection} to have element at index ({0}{1}){reason}, but found {2}.", x, y, Subject);
            }

            if (Subject.CountX < x || Subject.CountY < y)
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:collection} to have element at index ({0}{1}){reason}, but found counts ({2}, {3}).", x, y, Subject.CountX, Subject.CountY);
            }

            var actual = Subject[x, y];

            Execute.Assertion
               .ForCondition(actual.IsSameOrEqualTo(element))
               .BecauseOf(because, becauseArgs)
               .FailWith("Expected {0} at index ({1}, {2}){reason}, but found {3}.", element, x, y, actual);

            return new AndWhichConstraint<Matrix2dAssertion<T>, object>(this, element);
        }
    }
}