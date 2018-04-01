using System;
using System.Collections.Generic;
using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	public static partial class VectorExtensions
	{
		public static bool Approximately(this Vector2 p, Vector2 q)
		{
			return p.Approximately(q);
		}
		public static bool Approximately(this Vector3 p, Vector3 q)
		{
			return Mathf.Approximately(p.x, q.x) && Mathf.Approximately(p.y, q.y) && Mathf.Approximately(p.z, q.z);
		}
	}
	[RequireComponent(typeof(Pool))]
	public class PathFinder : MonoBehaviour
	{
		int Limit = 10;
		PathNode[] _map;
		PathNode[] _closed;
		PathQueue _open;
		PathNode _start;
		PathNode _finish;
		PathNode _current;
		public List<Vector2> Path;
		public int SortingOrder = 1;
		Pool _pool;
		public TileMap Map;
		int _opacityStep = 0;
		const int _opacityStart = 64;
		const int _opacityEnd = 192;
		const int _opacityRange = _opacityEnd - _opacityStart;
		void Awake()
		{
			_pool = GetComponent<Pool>();
		}
		public void SetupMap()
		{
			var width = Map.State.Width;
			var height = Map.State.Height;
			var count = width * height;
			_map = new PathNode[count];
			_closed = new PathNode[count];
			_open = new PathQueue(count);
			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					var index = Map.TileIndex(x, y);
					var walkable = !Map.Blocked(index);
					_map[index] = new PathNode() { X = x, Y = y, TileIndex = index, Walkable = walkable };
				}
			}
		}
		void Search()
		{
			Array.Clear(_closed, 0, _closed.Length);
			_open.Clear();
			_map[_start.TileIndex].Parent = null;
			_open.Enqueue(_start);
			while (_open.Count > 0)
			{
				_current = _open.Dequeue();
				_closed[_current.TileIndex] = _current;
				if (_current.TileIndex == _finish.TileIndex)
					break;
				NeighbourCheck();
			}
		}
		public void Find(Vector2 start, Vector2 finish)
		{
			Path = new List<Vector2>();
			SetStartAndFinish(start, finish);
			if (_start != null)
			{
				if (_finish == null)
					return;
				Search();
				var reached = Mathf.Approximately(finish.x, _current.X) && Mathf.Approximately(finish.y, _current.Y);
				if (!reached)
					Search();
				while (_current.Parent != null)
				{
					Path.Add(new Vector2(_current.X, _current.Y));
					_current = _current.Parent;
				}
				var startPoint = new Vector2(_start.X, _start.Y);
				Path.Add(startPoint);
				Path.Reverse();
			}
		}
		void SetStartAndFinish(Vector2 start, Vector2 finish)
		{
			_start = FindClose(start, finish);
			_finish = FindClose(finish, start);
		}
		PathNode FindClose(Vector2 start, Vector2 finish)
		{
			PathNode closestNode = null;
			var startIndex = Map.TileIndex(start);
			var startNode = _map[startIndex];
			var startDoor = Map.IsDoorShut(startIndex);
			var finishIndex = Map.TileIndex(finish);
			var finishNode = _map[finishIndex];
			if (startDoor || (startNode.Walkable && startNode.Reachable))
			{
				return startNode;
			}
			else
			{
				var radius = 1;
				var nodes = new List<PathNode>();
				while ((nodes.Count < 1) && (radius < Limit))
				{
					nodes = FindClosestCheck((int)start.x, (int)start.y, radius);
					radius++;
				}
				if (nodes.Count > 0)
				{
					var closest = float.MaxValue;
					var test0 = new Vector2(finishNode.X, finishNode.Y);
					foreach (var node in nodes)
					{
						var test1 = new Vector2(node.X, node.Y);
						var distance = Vector2.Distance(test0, test1);
						if (distance < closest)
						{
							closest = distance;
							closestNode = node;
						}
					}
				}
			}
			return closestNode;
		}
		List<PathNode> FindClosestCheck(int x, int y, int r)
		{
			var nodes = new List<PathNode>();
			for (var yy = y - r; yy <= y + r; yy++)
			{
				for (var xx = x - r; xx <= x + r; xx++)
				{
					if (((yy == y - r) || (xx == x - r) || (yy == y + r) || (xx == x + r)) && Map.InsideEdge(xx, yy))
					{
						var index = Map.TileIndex(xx, yy);
						var node = _map[index];
						if (node.Walkable && node.Reachable)
						{
							nodes.Add(node);
						}
					}
				}
			}
			return nodes;
		}
		public bool IsReachable(Vector2 p)
		{
			return _map[Map.TileIndex(p)].Reachable;
		}
		public void ReachableClear()
		{
			foreach (var node in _map)
			{
				node.Reachable = false;
			}
		}
		public void ReachableFrom()
		{
			ReachableFrom(Manager.Instance.Character.transform.localPosition);
		}
		public void ReachableFrom(Vector2 p)
		{
			SetupMap();
			ReachableFromMain((int)p.x, (int)p.y);
		}
		void ReachableFromMain(int x, int y)
		{
			ReachableClear();
			var points = new Stack<Vector2>();
			points.Push(new Vector2(x, y));
			while (points.Count > 0)
			{
				var current = _map[Map.TileIndex(points.Pop())];
				current.Reachable = true;
				for (var yy = current.Y - 1; yy <= current.Y + 1; yy++)
				{
					for (var xx = current.X - 1; xx <= current.X + 1; xx++)
					{
						if ((yy != current.Y || xx != current.X) && Map.InsideEdge(xx, yy))
						{
							var index = Map.TileIndex(xx, yy);
							var node = _map[index];
							if (!node.Reachable && (node.Walkable && !Map.IsDoorShut(index)))
							{
								points.Push(new Vector2(xx, yy));
							}
						}
					}
				}
			}
		}
		void NeighbourCheck()
		{
			for (var y = _current.Y - 1; y <= _current.Y + 1; y++)
			{
				for (var x = _current.X - 1; x <= _current.X + 1; x++)
				{
					if ((y != _current.Y || x != _current.X) && Map.InsideEdge(x, y))
					{
						var index = Map.TileIndex(x, y);
						var finish = (index == _finish.TileIndex);
						var node = _map[index];
						var door = Map.IsDoorShut(node.X, node.Y);
						if ((finish && door) || node.Walkable && !IsClosed(node))
						{
							if (!_open.Contains(node))
							{
								var addNode = node;
								addNode.H = GetHeuristics(node);
								addNode.G = GetMovementCost(node) + _current.G;
								addNode.F = addNode.H + addNode.G;
								addNode.Parent = _current;
								_open.Enqueue(addNode);
							}
							else
							{
								if (_current.G + GetMovementCost(node) < node.G)
								{
									node.Parent = _current;
									node.G = GetMovementCost(node) + _current.G;
									node.F = node.H + node.G;
									node.Parent = _current;
									_open.CascadeUp(node);
								}
							}
						}
					}
				}
			}
		}
		bool IsClosed(PathNode node)
		{
			return (_closed[node.TileIndex] != null);
		}
		int GetMovementCost(PathNode node)
		{
			return ((_current.X != node.X) && (_current.Y != node.Y)) ? 14 : 10;
		}
		int GetHeuristics(PathNode node)
		{
			return GetHeuristics(node, _finish);
		}
		int GetHeuristics(PathNode start, PathNode finish)
		{
			return (int)(Mathf.Abs(start.X - finish.X) * 10f) + (int)(Mathf.Abs(start.Y - finish.Y) * (10f));
		}
		public void RemovePathAt(Vector2 p)
		{
			Transform found = null;
			foreach (Transform child in transform)
			{
				if (child != transform)
				{
					if (child.gameObject.activeInHierarchy)
					{
						var c = child.localPosition;
						if (Mathf.Approximately(p.x, c.x) && Mathf.Approximately(p.y, c.y))
							found = child;
					}
				}
			}
			if (found != null)
				_pool.Exit(found.gameObject);
		}
		public void Draw(bool update)
		{
			RemovePath();
			if (Path == null || Path.Count == 0) return;
			_opacityStep = _opacityRange / Path.Count;
			var first = Path[0];
			var last = Path[Path.Count - 1];
			var color = Map.GetColor(last);
			Manager.Instance.Indicator.SetTargetColor(color);
			var pathDelta = GetDelta(first, last);
			float r = 0f;
			for (var i = 0; i < Path.Count; i++)
			{
				var p = Path[i];
				var nextExists = i + 1 < Path.Count;
				if (nextExists)
				{
					var next = Path[i + 1];
					var nodeDelta = GetDelta(p, next);
					r = GetRotation(nodeDelta, pathDelta);
				}
				var o = EnterPath();
				o.transform.localPosition = new Vector2(p.x, p.y);
				o.transform.parent = transform;
				o.transform.localEulerAngles = new Vector3(0f, 0f, r);
				var sr = o.GetComponent<SpriteRenderer>();
				var opacity = (((i * _opacityStep) + _opacityStart) / 255f);
				sr.color = color.SetAlpha(opacity);
				sr.sortingOrder = SortingOrder;
			}
		}
		GameObject EnterPath()
		{
			var path = _pool.Enter();
			path.SetActive(true);
			return path;
		}
		public void RemovePath()
		{
			foreach (Transform child in transform)
				_pool.Exit(child.gameObject);
		}
		Vector2 GetDelta(Vector2 start, Vector2 finish)
		{
			return finish - start;
		}
		int GetRotation(Vector2 nodeDelta, Vector2 pathDelta)
		{
			var rotation = 0;
			var trending = Mathf.Abs(pathDelta.y) > Mathf.Abs(pathDelta.x);
			if ((nodeDelta.x > 0) && (nodeDelta.y < 0))
			{
				rotation = trending ? 270 : 0;
			}
			else if ((nodeDelta.x > 0) && (nodeDelta.y > 0))
			{
				rotation = trending ? 90 : 0;
			}
			else if ((nodeDelta.x < 0) && (nodeDelta.y < 0))
			{
				rotation = trending ? 270 : 180;
			}
			else if ((nodeDelta.x < 0 && nodeDelta.y > 0))
			{
				rotation = trending ? 90 : 180;
			}
			else if (nodeDelta.x > 0)
			{
				rotation = 0;
			}
			else if (nodeDelta.x < 0)
			{
				rotation = 180;
			}
			else if (nodeDelta.y < 0)
			{
				rotation = 270;
			}
			else if (nodeDelta.y > 0)
			{
				rotation = 90;
			}
			return rotation;
		}
#if UNITY_EDITOR
		void OnDrawGizmosSelected()
		{
			if ((_map == null) || (_map.Length == 0)) return;
			for (var y = 0; y < Map.State.Height; y++)
			{
				for (var x = 0; x < Map.State.Width; x++)
				{
					var node = _map[Map.TileIndex(x, y)];
					var color = Color.white;
					if (_open.Contains(node) || IsClosed(node))
						if (node.Parent != null)
							ArrowGizmo(new Vector2(x, y), new Vector2(node.Parent.X, node.Parent.Y), Color.white);
					if (_open.Contains(node))
						color = Colors.ErrorBlue.SetAlpha(.5f);
					else if (IsClosed(node))
						color = Colors.ErrorYellow.SetAlpha(.5f);
					else if (node.Walkable && node.Reachable)
						color = Colors.ErrorGreen.SetAlpha(.5f);
					else
						color = Colors.ErrorRed.SetAlpha(.5f);
					Gizmos.color = color;
					Gizmos.DrawCube(new Vector3(x, y, -.5f), Vector3.one);
				}
			}
		}
		void ArrowGizmo(Vector2 child, Vector2 parent, Color c)
		{
			const float length = .25f;
			const float angle = 20f;
			Gizmos.color = c;
			var d = (parent - child) * .5f;
			Gizmos.DrawRay(child, d);
			var right = Quaternion.LookRotation(d, Vector3.right) * Quaternion.Euler(180f + angle, 0f, 0f) * Vector3.forward;
			var left = Quaternion.LookRotation(d, Vector3.right) * Quaternion.Euler(180f - angle, 0f, 0f) * Vector3.forward;
			Gizmos.DrawRay(child + d, right * length);
			Gizmos.DrawRay(child + d, left * length);
		}
#endif
	}
}
