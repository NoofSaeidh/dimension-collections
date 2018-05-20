using FluentAssertions;
using System;
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
        [Trait("Member", "Property")]
        [Trait("Property", nameof(Matrix2d<object>.Capacities))]
        [Trait("Property", nameof(Matrix2d<object>.CapacityX))]
        [Trait("Property", nameof(Matrix2d<object>.CapacityY))]
        [Theory]
        [InlineData(1, 4)]
        [InlineData(16, 5)]
        [InlineData(6, 6, true)]
        [InlineData(1, 0, true)]
        public virtual void Capacitities_Returns_DefinedInConstructorValues(int x, int y, bool useIndexClass = false)
        {
            Matrix2d<object> matrix;
            matrix = useIndexClass
               ? new Matrix2d<object>(new Index2d(x, y))
               : new Matrix2d<object>(x, y);
            matrix.Capacities.Should().Be(new Index2d(x, y));
            matrix.CapacityX.Should().Be(x);
            matrix.CapacityY.Should().Be(y);
            matrix.CapacityTotal.Should().Be(x * y);
        }
    }
}