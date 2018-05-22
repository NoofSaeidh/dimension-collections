// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    public struct Index2d : IIndexXd, IEquatable<Index2d>, IEquatable<IIndexXd>
    {
        internal const int Rank2d = 2;

        public Index2d(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Rank => Rank2d;
        public int X { get; }
        public int Y { get; }

        public int this[int dimension]
        {
            get
            {
                switch (dimension)
                {
                    case 0:
                        return X;

                    case 1:
                        return Y;

                    default:
                        throw new InvalidOperationException("Only 2 dimensions supported.");
                }
            }
        }

        public bool Equals(Index2d other)
        {
            return X == other.X && Y == other.Y;
        }

        public bool Equals(IIndexXd other)
        {
            if (other == null)
                return false;

            if (other is Index2d i)
                return Equals(i);

            return other.Rank == Rank2d
                && other[0] == X
                && other[1] == Y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IIndexXd);
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override string ToString() => $"{X}, {Y}";

        public static implicit operator Index2d((int x, int y) tuple) => new Index2d(tuple.x, tuple.y);

        public static bool operator ==(Index2d i1, Index2d i2) => i1.Equals(i2);

        public static bool operator !=(Index2d i1, Index2d i2) => !i1.Equals(i2);

        public static Index2d operator +(Index2d i1, Index2d i2) => new Index2d(i1.X + i2.X, i1.Y + i2.Y);
    }
}