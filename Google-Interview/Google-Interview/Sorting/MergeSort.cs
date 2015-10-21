using System;
using System.Collections.Generic;
using System.Linq;

namespace Google_Interview.Sorting
{
    public static class MergeSort<T> where T : IComparable
    {
        public static IList<T> Sort(IList<T> list)
        {
            int count = list.Count;
            if (count <= 1) return list;

            IList<T> left = Sort(list.Take(count/2).ToList());
            IList<T> right = Sort(list.Skip(count/2).ToList());

            int leftIndex = 0;
            int rightIndex = 0;
            IList<T> merged = new List<T>();
            while (leftIndex < left.Count || rightIndex < right.Count)
            {
                if (leftIndex >= left.Count)
                    merged.Add(right[rightIndex++]);
                else if (rightIndex >= right.Count)
                    merged.Add(left[leftIndex++]);
                else if (left[leftIndex].CompareTo(right[rightIndex]) <= 0)
                    merged.Add(left[leftIndex++]);
                else if (left[leftIndex].CompareTo(right[rightIndex]) > 0)
                    merged.Add(right[rightIndex++]);
                else
                    Console.WriteLine("YOU DONE MESSED UP");
            }

            return merged;
        }
    }
}
