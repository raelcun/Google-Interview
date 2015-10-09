﻿using Google_Interview.sorting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Google_Interview
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TestInsertionSort());
            Console.ReadKey();
        }

        public static bool TestInsertionSort()
        {
            List<int> list = GenerateRandomList(10);
            InsertionSort<int>.Sort(list);
            return IsSorted(list);
        }

        #region Private Methods
        private static bool IsSorted<T>(IList<T> list) where T : IComparable
        {
            if (list.Count == 0) return true;
            for (int i = 1; i < list.Count; i++)
                if (list[i].CompareTo(list[i - 1]) < 0) return false;
            return true;
        }

        private static List<int> GenerateRandomList(int length, int min = -10000, int max = 10000)
        {
            List<int> list = new List<int>();
            Random r = new Random();
            for (int i = 0; i < length; i++) list.Add(r.Next(min, max));
            return list;
        }
        #endregion Private Methods
    }
}
