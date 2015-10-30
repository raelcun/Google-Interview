using System;
using System.Diagnostics;
using System.Collections.Generic;
using Google_Interview.Data_Structures;
using System.Runtime.Serialization;
using System.Linq;

namespace Google_Interview.Data_Structures
{
	public abstract class Node<T>
	{
		public string Id { get; protected set; }
		public T Value { get; protected set; }
		protected Node<T> Parent { get; set; }
		protected List<Node<T>> Neighbors { get; set; }

		public Node() : this(default(T)) { }
		public Node(T value) : this(value, null) { }
		public Node(T value, Node<T> parent) : this(value, parent, null) { }
		public Node(T value, Node<T> parent, List<Node<T>> neighbors)
		{
			this.Id = Guid.NewGuid().ToString();
			this.Value = value;
			this.Parent = parent;
			this.Neighbors = neighbors;
		}

		public override string ToString ()
		{
			return string.Format ("[Node: Id={0}, Value={1}]", Id, Value);
		}
	}

	public class BNode<T> : Node<T>
	{
		public new BNode<T> Parent { get { return base.Parent as BNode<T>; } set { base.Parent = value as BNode<T>; } }
		public BNode<T> Left { get { return base.Neighbors[0] as BNode<T>; } set { base.Neighbors[0] = value as BNode<T>; } }
		public BNode<T> Right { get { return base.Neighbors[1] as BNode<T>; } set { base.Neighbors[1] = value as BNode<T>; } }

		public BNode() : this(default(T)) { }
		public BNode(T value) : this(value, null, null) { }
		public BNode(T value, BNode<T> left, BNode<T> right)
		{
			base.Value = value;
			base.Neighbors = new List<Node<T>>(new[] { left, right });
		}
	}

	public class BST<T> where T : IComparable
	{
		#region Public Constants
		public enum TraversalOrder
		{
			PRE,
			IN,
			POST
		}
		#endregion Public Constants


		#region Private Fields
		private BNode<T> Root { get; set; }
		#endregion Private Fields


		#region Constructors
		public BST ()
		{
			Root = null;
		}

		private BST (BNode<T> root)
		{
			this.Root = root;
		}
		#endregion Constructors


		#region Public Methods
		public bool IsEmpty()
		{
			return Root == null;
		}

		public virtual void Clear()
		{
			Root = null;
		}

		public virtual BNode<T> Add(T value)
		{
			if (IsEmpty()) { Root = new BNode<T>(value); return Root; }

			BNode<T> current = this.Root;
			BNode<T> parent = null;
			while (current != null)
			{
				int direction = value.CompareTo(current.Value);
				if (direction == 0) return null; // value already exists in tree
				parent = current;
				current = direction < 0 ? current.Left : current.Right;
			}

			BNode<T> newNode = new BNode<T>(value);
			if (parent == null)
				this.Root = newNode;
			else
				if (value.CompareTo(parent.Value) < 0) { parent.Left = newNode; newNode.Parent = parent; }
				else { parent.Right = newNode; newNode.Parent = parent; }
			return newNode;
		}

		public virtual BNode<T> Remove(T value)
		{
			if (IsEmpty()) return null;

			BNode<T> current = Root;
			int direction = 0;
			int parentDirection = 0;
			while (true)
			{
				direction = value.CompareTo(current.Value);
				if (direction == 0) break;
				parentDirection = direction;
				current = direction < 0 ? current.Left : current.Right;
				if (current == null) return null;
			}

			if (current.Left == null && current.Right == null) // no children
			{
				if (current.Parent == null) // if root being removed
					this.Root = null;
				else // remove reference to current node from parent node
				{
					if (parentDirection < 0) current.Parent.Left = null;
					else current.Parent.Right = null;
				}
			}
			else if (current.Left != null && current.Right != null) // two children
			{
				BNode<T> successor = current.Right;
				while (successor.Left != null) successor = successor.Left; // find smallest value greater than current value

				if (successor.Parent.Left == successor)
					successor.Parent.Left = null; // remove successor from tree
				else
					successor.Parent.Right = null; // remove successor from tree

				// connect parent and successor together
				successor.Parent = current.Parent; // attach successor to same parent as current
				if (current.Parent == null)
					this.Root = successor;
				else
				{
					if (parentDirection < 0) current.Parent.Left = successor;
					else current.Parent.Right = successor;
				}

				// connect successor to children of removed node
				successor.Left = current.Left;
				successor.Right = current.Right;
				if (current.Left != null) current.Left.Parent = successor;
				if (current.Right != null) current.Right.Parent = successor;
			}
			else // one child
			{
				if (current.Parent == null)
					this.Root = current.Left == null ? current.Left : current.Right;
				else
				{
					BNode<T> successor = current.Right != null ? current.Right : current.Left;

					// connect parent to successor
					if (parentDirection > 0)
						current.Parent.Right = successor;
					else
						current.Parent.Left = successor;

					// connect successor to parent
					successor.Parent = current.Parent;
				}
			}

			return current;
		}

		public virtual void BreadthFirstTraversal(Action<BNode<T>> cb, bool includeNulls = false)
		{
			if (IsEmpty()) return;

			Queue<BNode<T>> q = new Queue<BNode<T>>();
			q.Enqueue(Root);
			while(q.Length() > 0)
			{
				BNode<T> current = q.Dequeue();
				if (includeNulls && current == null) { cb(null); continue; }

				cb(current);
				if (current.Left != null || includeNulls) q.Enqueue(current.Left);
				if (current.Right != null || includeNulls) q.Enqueue(current.Right);
			}
		}

		public virtual void DepthFirstTraversal(TraversalOrder order, Action<BNode<T>> cb, bool includeNulls = false)
		{
			InternalDepthFirstTraversal(Root, order, cb, includeNulls);
		}

		public BNode<T> Find(T value)
		{
			return Find(Root, value);
		}

		public void ValidateBinaryTree()
		{
			if (IsEmpty()) return;

			DepthFirstTraversal(TraversalOrder.PRE, (node) =>
				{
					if (node.Left != null)
					{
						Debug.Assert(node.Left.Parent.Equals(node), string.Format("Pointers between parent {0} and left child {1} don't match up", node.Value, node.Left.Value));
						Debug.Assert(node.Left.Value.CompareTo(node.Value) < 0, string.Format("Value of left child {0} is greater than parent {1}", node.Left.Value, node.Value));
					}

					if (node.Right != null)
					{
						Debug.Assert(node.Right.Parent.Equals(node), string.Format("Pointers between parent {0} and right child {1} don't match up", node.Value, node.Right.Value));
						Debug.Assert(node.Right.Value.CompareTo(node.Value) > 0, string.Format("Value of right child {0} is greater than parent {1}", node.Right.Value, node.Value));
					}
				});
		}

		public string Serialize()
		{
			string ret = "";
			DepthFirstTraversal(TraversalOrder.PRE, (node) =>
				{
					if (node == null) { ret += "# "; return; }
					ret += node.Value + " ";
				}, true);
			return ret;
		}

		// TODO: remove int requirement
		public static BST<int> Deserialize(string str)
		{
			return new BST<int>(Deserialize(str.Split(' ').ToList()));
		}
		#endregion Public Methods


		#region Protected Methods
		protected virtual BNode<T> Find(BNode<T> root, T value)
		{
			if (root == null) return null;
			if (root.Value.Equals(value)) return root;
			return value.CompareTo(root.Value) <= 0 ? Find(root.Left, value) : Find(root.Right, value);
		}

		protected void InternalDepthFirstTraversal(BNode<T> start, TraversalOrder order, Action<BNode<T>> cb, bool includeNulls)
		{
			if (start == null)
			{
				if (includeNulls) cb(null);
				return;
			}
			if (order == TraversalOrder.PRE) cb(start);
			InternalDepthFirstTraversal(start.Left, order, cb, includeNulls);
			if (order == TraversalOrder.IN) cb(start);
			InternalDepthFirstTraversal(start.Right, order, cb, includeNulls);
			if (order == TraversalOrder.POST) cb(start);
		}
		#endregion Protected Methods


		private static BNode<int> Deserialize(List<string> tokens)
		{
			if (tokens.Count == 0) return null;

			string token = tokens[0];
			tokens.RemoveAt(0);
			if (token == "#")
			{
				return null;
			}

			BNode<int> root = new BNode<int>(int.Parse(token));
			root.Left = Deserialize(tokens);
			root.Right = Deserialize(tokens);
			return root;
		}
	}
}

