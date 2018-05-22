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
    public struct Intersection2d<T> : IIntersectionXd<T>, IEquatable<Intersection2d<T>>, IEquatable<IIntersectionXd<T>>
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

        public bool Equals(Intersection2d<T> other)
        {
            return Index == other.Index
                && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public bool Equals(IIntersectionXd<T> other)
        {
            if (other == null)
                return false;

            if (other is Intersection2d<T> i)
                return Equals(i);

            return Index.Equals(other.Index)
                && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IIntersectionXd<T>);
        }

        public override int GetHashCode()
        {
            var hashCode = 995152453;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + EqualityComparer<Index2d>.Default.GetHashCode(Index);
            return hashCode;
        }

        public override string ToString() => $"({Index}), {Value}";

        public static bool operator ==(Intersection2d<T> i1, Intersection2d<T> i2) => i1.Equals(i2);

        public static bool operator !=(Intersection2d<T> i1, Intersection2d<T> i2) => !i1.Equals(i2);
    }
}