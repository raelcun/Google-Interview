using System;

namespace Google_Interview.LinkedList
{
	public class Node<T>
	{
		public T Value { get; private set; }
		public Node<T> Next { get; set; }
		public Node<T> Prev { get; set; }

		public Node(T value, Node<T> next = null, Node<T> prev = null)
		{
			Value = value;
			Next = next;
			Prev = prev;
		}
	}
}

