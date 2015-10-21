using System;

namespace Google_Interview.LinkedList
{
	public class LinkedList<T> where T : struct
	{
		private Node<T> Head { get; set; }
		private Node<T> Tail { get; set; }
		public int Length { get; private set; }

		public void Append(T value)
		{
			Insert(value, Length);
		}

		public void Insert(T value, int index)
		{
			if (index < 0 || index > Length) throw new ArgumentOutOfRangeException();

			Node<T> newNode = new Node<T>(value);
			if (index == 0)
			{
				newNode.Next = Head;
				Head = newNode;
				if (Length == 0) Tail = Head;
			}
			else if (index == Length)
			{
				newNode.Prev = Tail;
				Tail.Next = newNode;
				Tail = newNode;
			}
			else
			{
				Node<T> prevNode = InternalGet(index - 1);
				Node<T> nextNode = prevNode.Next;

				newNode.Prev = prevNode;
				prevNode.Next = newNode;
				newNode.Next = nextNode;
				nextNode.Prev = newNode;
			}
			Length++;
		}

		public T Get(int index)
		{
			Node<T> n = InternalGet(index);
			return n.Value;
		}

		private Node<T> InternalGet(int index)
		{
			Node<T> n = Head;
			for (int i = 0; i < index; i++) n = n.Next;
			return n;
		}
	}
}

