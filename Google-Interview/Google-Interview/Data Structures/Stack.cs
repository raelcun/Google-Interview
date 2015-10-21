using System;
using System.Collections.Generic;

namespace Google_Interview.Data_Structures
{
	public class Stack<T>
	{
		private List<T> list = new List<T>();

		public int Length()
		{
			return list.Count;
		}

		public void Push(T value)
		{
			list.Add(value);
		}

		public T Pop()
		{
			if (list.Count == 0) throw new Exception("Stack is empty");

			T ret = list[list.Count-1];
			list.RemoveAt(list.Count-1);
			return ret;
		}
	}
}

