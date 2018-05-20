// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    public struct Index2d : IIndexXd
    {
        public Index2d(int x, int y)
        {
            X = x;
            Y = y;
        }

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

        public int Dimensions => 2;

        public override string ToString() => $"{X}, {Y}";
    }
}