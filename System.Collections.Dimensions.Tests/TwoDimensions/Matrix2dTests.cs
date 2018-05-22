using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Dimensions.Tests.Common;
using System.Collections.Dimensions.Tests.FluentAssertionExtensions;
using System.Collections.Dimensions.TwoDimensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Collections.Dimensions.Tests.TwoDimensions
{
    [Trait("Class", nameof(Matrix2d<object>))]
    public class Matrix2dTests
    {
        private const int _defaultCapacity = 4;

        private static readonly Intersection2d<string>[] _defaultStringIntersections;

        private static readonly string[] _defaultStringValues;

        static Matrix2dTests()
        {
            _defaultStringValues = new string[]
            {
                "a", "b", "c", "d",
                "e", "f", "g", "h",
                "i", "j", "k", "l",
                "m", "n", "o", "p"
            };

            _defaultStringIntersections = _defaultStringValues
                .Select((e, i) => new Intersection2d<string>(i / 4, i % 4, e)).ToArray();

            AssertionOptions.AssertEquivalencyUsing(o =>
                o.ComparingByValue<Intersection2d<object>>()
                 .ComparingByValue<Intersection2d<string>>()
                 .ComparingByValue<Intersection2d<int>>()
            );
        }

        private static Matrix2d<object> DefaultMatrix() => new Matrix2d<object>(_defaultCapacity, _defaultCapacity);

        private static Matrix2d<object> DefaultMatrix(int x, int y) => new Matrix2d<object>(x, y);

        private static Matrix2d<string> DefaultNotEmptyStringMatrix() => new Matrix2d<string>((4, 4), _defaultStringValues);

        #region Capacities

        [Trait("Member", "Property")]
        [Trait("Property", nameof(Matrix2d<object>.Capacities))]
        [Trait("Property", nameof(Matrix2d<object>.CapacityX))]
        [Trait("Property", nameof(Matrix2d<object>.CapacityY))]
        [Theory]
        [InlineData(1, 4)]
        [InlineData(16, 5)]
        [InlineData(6, 6, true)]
        [InlineData(1, 0, true)]
        public void Capacities_Get_DefinedInConstructorValues(int x, int y, bool useIndexClass = false)
        {
            var matrix = useIndexClass
               ? new Matrix2d<object>(new Index2d(x, y))
               : new Matrix2d<object>(x, y);

            matrix.Capacities.Should()
                .Be(x, y)
                .And
                .Be(matrix.CapacityX, matrix.CapacityY);

            matrix.CapacityTotal.Should().Be(x * y);
        }

        [Trait("Member", "Property")]
        [Trait("Property", nameof(Matrix2d<object>.Capacities))]
        [Trait("Property", nameof(Matrix2d<object>.CapacityX))]
        [Trait("Property", nameof(Matrix2d<object>.CapacityY))]
        [Theory]
        [InlineData(1, 3,
            "s", "t", "r")]
        [InlineData(4, 4,
            "s", "t", "r", "i",
            "n", 'g', 'a', 'n',
            'd', 'i', 'n', 't',
            1, 2, 3, 4)]
        [InlineData(0, 0)]
        public void Capacities_Set_CannotSetFewerThanItemsCounts(int x, int y, params object[] values)
        {
            var matrix = new Matrix2d<object>((x, y), values);
            using (new AssertionScope())
            {
                matrix.Invoking(m => m.Capacities = (x - 1, y - 1))
                    .Should()
                    .Throw<ArgumentOutOfRangeException>();
                matrix.Invoking(m => m.CapacityX = x - 1)
                    .Should()
                    .Throw<ArgumentOutOfRangeException>();
                matrix.Invoking(m => m.CapacityY = y - 1)
                    .Should()
                    .Throw<ArgumentOutOfRangeException>();
            }
        }

        [Trait("Member", "Property")]
        [Trait("Property", nameof(Matrix2d<object>.Capacities))]
        [Trait("Property", nameof(Matrix2d<object>.CapacityX))]
        [Trait("Property", nameof(Matrix2d<object>.CapacityY))]
        [Theory]
        [InlineData("x", 5)]
        [InlineData("y", 5)]
        [InlineData("", 5, 5)]
        public void Capacities_Set_Chained(string type, int value1, int value2 = 0)
        {
            var matrix = DefaultMatrix();
            int x, y;
            switch (type)
            {
                case "x":
                    matrix.CapacityX = x = value1;
                    y = _defaultCapacity;
                    break;

                case "y":
                    matrix.CapacityY = y = value1;
                    x = _defaultCapacity;
                    break;

                default:
                    matrix.Capacities = (x, y) = (value1, value2);
                    break;
            }

            matrix.Capacities.Should()
                .Be(x, y)
                .And
                .Be(matrix.CapacityX, matrix.CapacityY);

            matrix.CapacityTotal.Should().Be(x * y);
        }

        #endregion Capacities

        #region Indexer

        [Trait("Member", "Indexer")]
        [Theory]
        [InlineData(1, 3,
            "s", "t", "r")]
        [InlineData(4, 4,
            "s", "t", "r", "i",
            "n", 'g', 'a', 'n',
            'd', 'i', 'n', 't',
            1, 2, 3, 4)]
        [InlineData(5, 3,
            '\r', '\n', '\0', '\t', '\f',
            typeof(string), typeof(int), typeof(char), typeof(uint), typeof(Type),
            null, "", 0, -1.1, -1.2)]
        public void Indexer_Get_PredefinedValues(int x, int y, params object[] values)
        {
            if (values.Length != x * y)
                throw new TestInitializationException();

            var matrix = new Matrix2d<object>((x, y), values);

            for (int i = 0, k = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++, k++)
                {
                    matrix.Should().HaveElementAt(i, j, values[k]);
                }
            }
        }

        [Trait("Member", "Indexer")]
        [Theory]
        [InlineData(0, 0, "ha")]
        [InlineData(3, 3, "foo")]
        [InlineData(1, 2, "bar")]
        public void Indexer_Get_Set_WorkWithSameValues(int x, int y, string setValue)
        {
            var matrix = DefaultNotEmptyStringMatrix();
            matrix[x, y] = setValue;
            matrix.Should().HaveElementAt(x, y, setValue);
        }

        [Trait("Member", "Indexer")]
        [Fact]
        public void Indexer_Set_OutOfRange_Throws()
        {
            var matrix = DefaultNotEmptyStringMatrix();
            matrix.Invoking(m => m[-1, 0] = "")
                .Should()
                .Throw<ArgumentOutOfRangeException>();
            matrix.Invoking(m => m[1, -1] = "")
                .Should()
                .Throw<ArgumentOutOfRangeException>();
            matrix.Invoking(m => m[4, 4] = "")
                .Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        #endregion Indexer

        #region Enumerations

        [Trait("Member", "Enumerator")]
        [Trait("Method", nameof(Matrix2d<object>.GetEnumerator))]
        [Fact]
        public void Enumerator_Default_WorksRight()
        {
            var matrix = DefaultNotEmptyStringMatrix();

            var array = matrix.ToArray();

            array.Should().HaveCount(16);
            array.ToArray().Should().BeEquivalentTo(_defaultStringIntersections);
        }

        #endregion Enumerations
    }
}