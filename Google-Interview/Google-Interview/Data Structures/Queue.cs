using System;
using System.Collections.Generic;

namespace Google_Interview.Data_Structures
{
	public class Queue<T>
	{
		private List<T> list = new List<T>();

		public int Length()
		{
			return list.Count;
		}

		public void Enqueue(T value)
		{
			list.Add(value);
		}

		public T Dequeue()
		{
			if (Length() == 0) throw new Exception("Queue is empty");

			T ret = list[0];
			for (int i = 0; i < list.Count - 1; i++)
				list[i] = list[i+1];
			return ret;
		}

		public T Peek()
		{
			if (Length() == 0) throw new Exception("Queue is empty");

			return list[0];
		}
	}
}

