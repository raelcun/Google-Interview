using Google_Interview.Sorting;
using Google_Interview.Map;
using System;
using System.Collections.Generic;
using Google_Interview.Data_Structures;

namespace Google_Interview
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine(TestQueue());
			//Console.WriteLine(TestLinkedList());
			//Console.WriteLine(TestMap());
            //Console.WriteLine(TestQuickSort());
            //Console.WriteLine(TestInsertionSort());
            //Console.WriteLine(TestMergeSort());
            Console.ReadKey();
        }

        #region Public Methods
		public static bool TestQueue()
		{
			List<int> list = GenerateRandomList(10);
			var queue = new Google_Interview.Data_Structures.Queue<int>();
			for (int i = 0; i < list.Count; i++) queue.Enqueue(list[i]);
			for (int i = 0; i < list.Count; i++) if (queue.Dequeue() != list[i]) return false;
			return true;
		}

		public static bool TestLinkedList()
		{
			List<int> list = GenerateRandomList(10000);
			var ll = new Google_Interview.LinkedList.LinkedList<int>();
			for (int i = 0; i < list.Count; i++) ll.Append(list[i]);
			for (int i = 0; i < list.Count; i++) if (!ll.Get(i).Equals(list[i])) return false;
			return true;
		}

		public static bool TestMap()
		{
			List<int> list = GenerateRandomList(10000);
			Map<int, int> map = new Map<int, int>(20000);
			for(int i = 0; i < list.Count; i++)
				map.put(i, list[i]);

			for (int i = 0; i < list.Count; i++)
			{
				if (map.get(i) != list[i])
					return false;
			}
			return true;
		}

        public static bool TestQuickSort()
        {
            List<int> list = GenerateRandomList(10000);
            QuickSort<int>.Sort(list);
            return IsSorted(list);
        }

        public static bool TestInsertionSort()
        {
            List<int> list = GenerateRandomList(10000);
            InsertionSort<int>.Sort(list);
            return IsSorted(list);
        }

        public static bool TestMergeSort()
        {
            List<int> list = GenerateRandomList(10000);
            IList<int> sorted = MergeSort<int>.Sort(list);
            return IsSorted(sorted);
        }
        #endregion Public Methods

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
