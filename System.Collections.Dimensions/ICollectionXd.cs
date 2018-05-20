// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions
{
    public interface ICollectionXd<T> : IEnumerable<IIntersectionXd<T>>, IEnumerable<T>, IEnumerable
    {
        int Count { get; }
        IIndexXd Counts { get; }
        int Dimensions { get; }
        bool IsReadOnly { get; }
        bool IsSynchronized { get; }
        object SyncRoot { get; }

        void Add(IEnumerable<T> items, int dimension);

        void Clear();

        bool Contains(T item);

        void CopyTo(Array array, int arrayIndex);

        bool Remove(IEnumerable<T> items, int dimension);
    }
}