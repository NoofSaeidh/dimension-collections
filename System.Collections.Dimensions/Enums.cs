// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions
{
    public enum DimensionChangeBehavior
    {
        // throw exception if adding value has more (or less)
        // values in any dimension
        Throw,

        // ignore such values (doesn't add it)
        Ignore,

        // fill missing member with default values
        FillDefaults
    }
}