namespace Google_Interview.Map
{
	public class Node<K, V>
	{
		public K Key { get; }
		public V Value { get; }

		public Node (K key, V value)
		{
			Key = key;
			Value = value;
		}

		public override string ToString ()
		{
			return $"[Node: Key={Key}, Value={Value}]";
		}
	}
}

