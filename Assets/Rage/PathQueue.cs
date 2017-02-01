namespace ca.HenrySoftware.Rage
{
	public sealed class PathQueue
	{
		public int Count { get; set; }
		PathNode[] _nodes;
		public PathQueue(int count)
		{
			Count = 0;
			_nodes = new PathNode[count];
		}
		public void Clear()
		{
			for (var i = 1; i < _nodes.Length; i++)
				_nodes[i] = null;
			Count = 0;
		}
		public bool Contains(PathNode node)
		{
			return _nodes[node.SortedIndex] == node;
		}
		void Swap(PathNode node1, PathNode node2)
		{
			_nodes[node1.SortedIndex] = node2;
			_nodes[node2.SortedIndex] = node1;
			var temp = node1.SortedIndex;
			node1.SortedIndex = node2.SortedIndex;
			node2.SortedIndex = temp;
		}
		public void Enqueue(PathNode node)
		{
			Count++;
			_nodes[Count] = node;
			node.SortedIndex = Count;
			CascadeUp(Count);
		}
		public PathNode Dequeue()
		{
			var node = _nodes[1];
			if (Count == 1)
			{
				Count--;
			}
			else
			{
				_nodes[1] = _nodes[Count];
				_nodes[1].SortedIndex = 1;
				Count--;
				CascadeDown(1);
			}
			return node;
		}
		public void CascadeUp(PathNode node)
		{
			CascadeUp(node.SortedIndex);
		}
		void CascadeUp(int v)
		{
			while (v != 1)
			{
				var node = _nodes[v];
				var parent = _nodes[v / 2];
				if (node.CompareTo(parent) < 0)
				{
					Swap(node, parent);
					v = v / 2;
				}
				else break;
			}
		}
		void CascadeDown(int v)
		{
			while (true)
			{
				var u = v;
				var rightIndex = 2 * u;
				var leftIndex = 2 * u + 1;
				if (leftIndex <= Count)
				{
					if (_nodes[u].CompareTo(_nodes[rightIndex]) >= 0)
						v = rightIndex;
					if (_nodes[v].CompareTo(_nodes[leftIndex]) >= 0)
						v = leftIndex;
				}
				else if (rightIndex <= Count)
				{
					var rightNode = _nodes[rightIndex];
					if (_nodes[u].CompareTo(rightNode) >= 0)
						v = rightIndex;
				}
				if (u == v)
					break;
				Swap(_nodes[u], _nodes[v]);
			}
		}
	}
}
