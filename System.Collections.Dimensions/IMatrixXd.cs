// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions
{
    public interface IMatrixXd<T> : ICollectionXd<T>, IEnumerable<IIntersectionXd<T>>, IEnumerable<T>, IEnumerable
    {
        T this[IIndexXd index] { get; set; }

        bool IsFixedSize { get; }

        IIndexXd IndexOf(T item);

        void Insert(IIndexXd index, T item);

        void RemoveAt(IIndexXd index, int dimension);
    }
}