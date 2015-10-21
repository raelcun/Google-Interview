using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Google_Interview.Sorting
{
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    public static class QuickSort<T> where T : IComparable
    {
        public static void Sort(IList<T> list) { InternalSort(list, 0, list.Count-1); }

        private static void InternalSort(IList<T> list, int left, int right)
        {
            int newPivot = Partition(list, left, right);
            if (left < newPivot - 1) InternalSort(list, left, newPivot-1);
            if (newPivot < right) InternalSort(list, newPivot, right);
        }

        private static int Partition(IList<T> list, int left, int right)
        {
			T pivot = list[(left + right) / 2];
			while(left <= right)
            {
                while (list[left].CompareTo(pivot) < 0) left++;
                while (list[right].CompareTo(pivot) > 0) right--;
				if (left <= right) {
					T temp = list [left];
					list [left] = list [right];
					list [right] = temp;
					left++;
					right--;
				}
            }
			return left;
        }
    }
}
