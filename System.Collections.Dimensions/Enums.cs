// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions
{
    [Flags]
    public enum DimensionSizeMismatchActions
    {
        Throw = 0b00,
        EnlargeInput = 0b01,
        EnlargeDimension = 0b10,
    }
}