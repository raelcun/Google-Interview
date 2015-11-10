using Google_Interview.Sorting;
using System;
using System.Collections.Generic;
using Google_Interview.Data_Structures;
using Google_Interview.Map;
using System.Linq;

namespace Google_Interview
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine(TestBTree());
			Console.WriteLine(TestStack());
			Console.WriteLine(TestQueue());
			Console.WriteLine(TestLinkedList());
			Console.WriteLine(TestMap());
            Console.WriteLine(TestQuickSort());
            Console.WriteLine(TestInsertionSort());
            Console.WriteLine(TestMergeSort());

			Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        #region Public Methods
		public static bool TestBTree()
		{
			List<int> list = GenerateRandomList(10000, -10000, 10000, false);
			List<int> added = new List<int>();

			var btree = new BST<int>();

			Random r = new Random();
			for (int i = 0; i < 10000; i++)
			{
				if (r.Next(0, 2) == 0) // test add
				{
					int rand = r.Next(0, list.Count);
					int value = list[rand];
					added.Add(value);
					btree.Add(value);
					btree.ValidateBinaryTree();
					list.RemoveAt(rand);
					if (btree.Find(value) == null)
						return false;
				}
				else if (!btree.IsEmpty()) // test remove
				{
					int rand = r.Next(0, added.Count);
					int value = added[rand];
					added.RemoveAt(rand);
					btree.Remove(value);
					btree.ValidateBinaryTree();
					list.Add(value);
					if (btree.Find(value) != null)
						return false;
				}
			}

			//btree.BreadthFirstTraversal((node) =>
			//	{
			//		Console.Write(node == null ? "# " : node.Value + " ");
			//	}, true);

			string serialized = btree.Serialize();
			var deserialized = BST<int>.Deserialize(serialized);
			string traversed = "";
			deserialized.DepthFirstTraversal(BST<int>.TraversalOrder.Pre, (node) =>
				{
					traversed += node == null ? "# " : node.Value + " ";
				}, true);
			if (!serialized.Equals(traversed)) return false;
            
			return true;
		}

		public static bool TestStack()
		{
			List<int> list = GenerateRandomList(10000);
			var stack = new Data_Structures.Stack<int>();
			foreach (int i in list) stack.Push(i);
		    for (int i = 0; i < list.Count; i++) if (stack.Pop() != list[list.Count - i - 1]) return false;
			return true;
		}

		public static bool TestQueue()
		{
			List<int> list = GenerateRandomList(10000);
			var queue = new Data_Structures.Queue<int>();
			foreach (int i in list) queue.Enqueue(i);
		    for (int i = 0; i < list.Count; i++) if (queue.Dequeue() != list[i]) return false;
			return true;
		}

		public static bool TestLinkedList()
		{
			List<int> list = GenerateRandomList(10000);
			var ll = new LinkedList.LinkedList<int>();
			foreach (int i in list) ll.Append(i);
		    for (int i = 0; i < list.Count; i++) if (!ll.Get(i).Equals(list[i])) return false;
			return true;
		}

		public static bool TestMap()
		{
			List<int> list = GenerateRandomList(10000);
			Map<int, int> map = new Map<int, int>(20000);
			for(int i = 0; i < list.Count; i++)
				map.Put(i, list[i]);

		    return !list.Where((t, i) => map.Get(i) != t).Any();
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

		private static List<int> GenerateRandomList(int length, int min = -10000, int max = 10000, bool allowDups = true)
        {
            List<int> list = new List<int>();
            Random r = new Random();
			while (list.Count < length)
			{
				int rand = r.Next(min, max);
				if (allowDups || !list.Contains(rand)) list.Add(rand);
			}
            return list;
        }
        #endregion Private Methods
    }
}
