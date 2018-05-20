// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    public interface ICollection2d<T> : ICollectionXd<T>, IEnumerable<Intersection2d<T>>, IEnumerable<T>, IEnumerable
    {
        int CountX { get; }

        int CountY { get; }

        void AddX(IEnumerable<T> items);

        void AddY(IEnumerable<T> items);

        bool RemoveX(IEnumerable<T> items);

        bool RemoveY(IEnumerable<T> items);
    }
}