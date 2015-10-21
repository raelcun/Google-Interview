using System;
using System.Collections.Generic;

namespace Google_Interview.Map
{
	public class Map<K,V> where V : struct
	{
		private Node<K,V>[] list;

		public Map(int capacity)
		{
			list = new Node<K, V>[capacity];
		}

		public void put(K key, V value)
		{
			int hash = key.GetHashCode();
			int bucket = Math.Abs(hash % list.Length);
			for (int i = 0; i < list.Length; i++)
			{
				if (list[(bucket+i)%list.Length] == null)
				{
					list[(bucket+i)%list.Length] = new Node<K, V>(key, value);
					return;
				}
				if (list[(bucket+i)%list.Length].Equals(key)) throw new Exception("Key Already Exists");
			}
			throw new Exception("Hashmap is full");
		}

		public V? get(K key)
		{
			int hash = key.GetHashCode ();
			int bucket = Math.Abs(hash % list.Length);
			for (int i = 0; i < list.Length; i++)
			{
				var e = list [(i + bucket) % list.Length];
				if (e == null) return null;
				if (e.Key.Equals(key)) return e.Value;
			}
			return null;
		}
	}
}