using System;

namespace Google_Interview.Map
{
	public class Node<K, V>
	{
		public K Key { get; private set; }
		public V Value { get; private set; }

		public Node (K key, V value)
		{
			Key = key;
			Value = value;
		}

		public override string ToString ()
		{
			return string.Format ("[Node: Key={0}, Value={1}]", Key, Value);
		}
	}
}

