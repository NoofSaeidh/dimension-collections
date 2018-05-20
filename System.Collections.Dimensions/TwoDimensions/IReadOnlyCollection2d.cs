// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    public interface IReadOnlyCollection2d<T> : IReadOnlyCollectionXd<T>, IEnumerable<Intersection2d<T>>
    {
        int CountX { get; }
        int CountY { get; }
        new Index2d Counts { get; }
    }
}