using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions
{
    public static class ArrayExtended
    {
        public static void CopySingleToTwoDimensionArray(Array source, Array target)
        {
            // todo: checking sizes
            // todo: methods with different sizes
            var sourceSize = source.Length;
            var targetSizeX = target.GetLength(0);
            var targetSizeY = target.GetLength(1);
            for (int i = 0, k = 0; i < targetSizeX; i++)
            {
                for (int j = 0; j < targetSizeY; j++, k++)
                {
                    target.SetValue(source.GetValue(k), i, j);
                }
            }
        }

        public static void CopyEnumerationToTwoDimensionArray(IEnumerable enumerable, Array target)
        {
            var targetSizeX = target.GetLength(0);
            var targetSizeY = target.GetLength(1);
            var enumerator = enumerable.GetEnumerator();
            for (int i = 0, k = 0; i < targetSizeX; i++)
            {
                for (int j = 0; j < targetSizeY; j++, k++)
                {
                    if (!enumerator.MoveNext())
                        return;
                    target.SetValue(enumerator.Current, i, j);
                }
            }
        }
    }
}