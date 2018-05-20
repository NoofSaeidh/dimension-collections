// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    public interface IReadOnlyMatrix2d<T> : IReadOnlyCollection2d<T>
    {
        T this[int x, int y] { get; }
        T this[Index2d index] { get; }
    }
}