﻿// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions
{
    public interface IReadOnlyMatrixXd<T> : IReadOnlyCollectionXd<T>, /*IEnumerable<T>,*/ IEnumerable
    {
        T this[IIndexXd index] { get; }
    }
}