// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    [DebuggerDisplay("{Value}", Name = "{Index}")]
    public struct Intersection2d<T> : IIntersectionXd<T>
    {
        public Intersection2d(Index2d index, T value)
        {
            Value = value;
            Index = index;
        }

        public Intersection2d(int x, int y, T value) : this(new Index2d(x, y), value)
        {
        }

        public T Value { get; }
        public Index2d Index { get; }
        public int X => Index.X;
        public int Y => Index.Y;
        IIndexXd IIntersectionXd<T>.Index => Index;

        public override string ToString() => $"({Index}), {Value}";
    }
}