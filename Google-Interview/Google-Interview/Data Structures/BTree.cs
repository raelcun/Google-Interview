using System;
using Google_Interview.Data_Structures;

namespace Google_Interview.Data_Structures
{
	public class BNode<K,V>
	{
		public K Key { get; set; }
		public V Value { get; set; }
		public BNode<K,V> Left { get; set; }
		public BNode<K,V> Right { get; set; }

		public BNode(K key, V value, BNode<K,V> left = null, BNode<K,V> right = null)
		{
			Key = key;
			Value = value;
			Left = left;
			Right = right;
		}
	}

	public class BTree<K,V> where K : IComparable where V : struct
	{
		private BNode<K,V> Root { get; set; }
		public int Count { get; private set; }

		public BTree ()
		{
			Root = null;
		}

		public bool IsEmpty()
		{
			return Root == null;
		}

		public void Add(K key, V value)
		{
			if (IsEmpty()) { Root = new BNode<K, V>(key, value); return; }
			Add(Root, key, value);
		}

		public V? Get(K key)
		{
			return Find(Root, key);
		}

		public void AllDFS(Action<K, V> callback)
		{
			if (IsEmpty()) return;

			Stack<BNode<K,V>> stack = new Stack<BNode<K, V>>();
			stack.Push(Root);
			while(stack.Length() > 0)
			{
				BNode<K, V> current = stack.Pop();
				callback(current.Key, current.Value);
				if (current.Left != null) stack.Push(current.Left);
				if (current.Right != null) stack.Push(current.Right);
			}
		}

		public void AllBFS(Action<K, V> callback)
		{
			if (IsEmpty()) return;

			Queue<BNode<K,V>> q = new Queue<BNode<K, V>>();
			q.Enqueue(Root);
			while(q.Length() > 0)
			{
				BNode<K, V> current = q.Dequeue();
				callback(current.Key, current.Value);
				if (current.Left != null) q.Enqueue(current.Left);
				if (current.Right != null) q.Enqueue(current.Right);
			}
		}

		protected void Add(BNode<K,V> root, K key, V value)
		{
			int direction = key.CompareTo(root.Key);

			if (direction <= 0) // go left
			{
				if (root.Left == null)
				{
					root.Left = new BNode<K, V>(key, value);
					Count++;
					return;
				}
				Add(root.Left, key, value);
			}
			else // go right
			{
				if (root.Right == null)
				{
					root.Right = new BNode<K, V>(key, value);
					Count++;
					return;
				}
				Add(root.Right, key, value);
			}
		}

		protected V? Find(BNode<K, V> root, K key)
		{
			if (root == null) return null;
			if (root.Key.Equals(key)) return root.Value;
			return key.CompareTo(root.Key) <= 0 ? Find(root.Left, key) : Find(root.Right, key);
		}
	}
}

