// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    public interface IMatrix2d<T> : ICollection2d<T>, IMatrixXd<T>, IEnumerable<Intersection2d<T>>, IEnumerable<T>, IEnumerable
    {
        T this[int x, int y] { get; set; }

        T this[Index2d index] { get; set; }

        int IndexXOf(T item);

        int IndexYOf(T item);

        new Index2d IndexOf(T intem);

        void InsertX(int x, T item);

        void InsertY(int y, T item);

        void RemoveAtX(int x);

        void RemoveAtY(int y);
    }
}