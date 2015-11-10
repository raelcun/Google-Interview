using System;
using System.Collections.Generic;

namespace Google_Interview.Sorting
{
    public static class InsertionSort<T> where T : IComparable
    {
        public static void Sort(IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;
                for (int j = i+1; j < list.Count; j++)
                    if (list[j].CompareTo(list[minIndex]) < 0) minIndex = j;
                if (minIndex != i) Swap(list, i, minIndex);
            }
        }

        private static void Swap(IList<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
