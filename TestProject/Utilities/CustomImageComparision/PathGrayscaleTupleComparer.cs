using System;
using System.Collections.Generic;

namespace TestProject.Utilities.CustomImageComparision
{
    /// <summary>
    /// Helperclass which compares tuples with imagepath and grayscale based on the values in their grayscale array
    /// </summary>
    class PathGrayscaleTupleComparer : IComparer<Tuple<string, byte[,]>>
    {
        private static ArrayComparer<byte> _comparer = new ArrayComparer<byte>();
        public int Compare(Tuple<string, byte[,]> x, Tuple<string, byte[,]> y)
        {
            return _comparer.Compare(x.Item2, y.Item2);
        }
    }

    class ArrayComparer<T> : IComparer<T[,]> where T : IComparable
    {
        public int Compare(T[,] array1, T[,] array2)
        {
            for (int x = 0; x < array1.GetLength(0); x++)
            {
                for (int y = 0; y < array2.GetLength(1); y++)
                {
                    int comparisonResult = array1[x, y].CompareTo(array2[x, y]);
                    if (comparisonResult != 0)
                    {
                        return comparisonResult;
                    }
                }
            }
            return 0;
        }
    }
}
