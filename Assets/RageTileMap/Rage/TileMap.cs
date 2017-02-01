using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	[Flags]
	[Serializable]
	public enum TileFlags
	{
		Nothing = 0,
		FlipX = (1 << 1),
		FlipY = (1 << 2),
		Rot90 = (1 << 3),
	}
	[Serializable]
	public class StateLayer
	{
		public List<int> Tiles;
		public List<TileFlags> Flags;
	}
	[Serializable]
	public class StateMap
	{
		public int X;
		public int Y;
		public int Width;
		public int Height;
		public List<StateLayer> Layers;
	}
	public class TileMap : MonoBehaviour
	{
		Transform _t;
		public Sprite Sheet;
		public float LayerOffset = .1f;
		List<MeshFilter> _layers = new List<MeshFilter>();
		List<Vector2[]> _uv = new List<Vector2[]>();
		public List<string> LayerNames;
		public List<Material> LayerMaterials;
		List<Animation> _animations;
		List<Dictionary<int, IEnumerator>> _runningAnimations;
		[NonSerialized]
		public Mesh Mesh;
		public StateMap State;
		void Awake()
		{
			_t = transform;
			_animations = GetAnimations();
		}
		public int TileIndex(Vector2 p) { return TileIndex((int)p.x, (int)p.y); }
		public int TileIndex(Vector2 p, int width) { return TileIndex((int)p.x, (int)p.y, width); }
		public int TileIndex(int x, int y) { return TileIndex(x, y, State.Width); }
		public int TileIndex(int x, int y, int width)
		{
			return y * width + x;
		}
		public Vector2 TilePosition(int index) { return TilePosition(index, State.Width); }
		public Vector2 TilePosition(int index, int width)
		{
			var y = index / width;
			var x = index - width * y;
			return new Vector2(x, y);
		}
		public bool InsideEdge(Vector2 p) { return InsideEdge((int)p.x, (int)p.y); }
		public bool InsideEdge(int x, int y) { return InsideMap(x, y, 1); }
		public bool InsideMap(Vector2 p) { return InsideMap((int)p.x, (int)p.y); }
		public bool InsideMap(int x, int y, int edge = 0)
		{
			return (x >= 0 + edge) && (y >= 0 + edge) &&
				(x < State.Width - edge) && (y < State.Height - edge);
		}
		public void Rebuild()
		{
			if (Mesh != null)
			{
				var oldTilesAcross = (int)Mesh.bounds.size.x;
				var saved = JsonUtility.ToJson(State);
				var savedMap = JsonUtility.FromJson<StateMap>(saved);
				Build(State.Width, State.Height);
				Apply(savedMap, oldTilesAcross);
			}
			else
			{
				Build(State.Width, State.Height);
			}
		}
		public void Build()
		{
			Build(State.Width, State.Height);
		}
		public void Build(int width, int height, int x = 0, int y = 0)
		{
			if (_runningAnimations != null)
				for (var i = 0; i < _runningAnimations.Count; i++)
					foreach (var a in _runningAnimations[i])
						StopCoroutine(a.Value);
			var tileCount = width * height;
			var emptyTiles = Enumerable.Repeat(-1, tileCount).ToList();
			var emptyFlags = Enumerable.Repeat(TileFlags.Nothing, tileCount).ToList();
			var count = LayerNames.Count;
			var layers = new List<StateLayer>(count);
			for (var i = 0; i < count; i++)
				layers.Add(new StateLayer() { Tiles = new List<int>(emptyTiles), Flags = new List<TileFlags>(emptyFlags) });
			var map = new StateMap() {Width = width, Height = height, X = x, Y = y, Layers = layers};
			Build(map);
			_runningAnimations = new List<Dictionary<int, IEnumerator>>();
			for (var i = 0; i < count; i++)
				_runningAnimations.Add(new Dictionary<int, IEnumerator>());
		}
		public void Build(StateMap map)
		{
			DestroyChildren();
			_layers.Clear();
			_uv.Clear();
			State = map;
			Manager.Instance.Character.transform.localPosition = new Vector3(State.X, State.Y, -LayerOffset * 2);
			Mesh = BuildMesh();
			for (var i = 0; i < LayerNames.Count; i++)
			{
				var go = new GameObject() { name = LayerNames[i] };
				go.transform.localPosition = new Vector3(0f, 0f, -LayerOffset * i);
				go.transform.SetParent(_t, false);
				var filter = go.AddComponent<MeshFilter>();
				filter.sharedMesh = Mesh;
				_layers.Add(filter);
				_uv.Add(Mesh.uv);
				var renderer = go.AddComponent<MeshRenderer>();
				renderer.sortingOrder = i;
				renderer.sharedMaterial = LayerMaterials[LayerMaterials.Count > 1 ? i : 0];
			}
		}
		void DestroyChildren()
		{
			StopAllCoroutines();
			var count = _t.childCount;
			for (int i = count - 1; i >= 0; i--)
			{
				Destroy(_t.GetChild(i).gameObject);
			}
		}
		public virtual void LoadXml(string text)
		{
			var xml = new XmlDocument();
			xml.LoadXml(text);
			var map = xml.GetElementsByTagName("map")[0];
			State.Width = int.Parse(map.Attributes.GetNamedItem("width").Value);
			State.Height = int.Parse(map.Attributes.GetNamedItem("height").Value);
			Build();
			var firstgid = 0;
			foreach (XmlNode child in map.ChildNodes)
			{
				if (child.Name == "tileset")
					firstgid = int.Parse(child.Attributes.GetNamedItem("firstgid").Value);
				else if (child.Name == "layer")
				{
					var name = child.Attributes.GetNamedItem("name").Value;
					if (!LayerNames.Contains(name))
						LayerNames.Add(name);
					var index = LayerNames.IndexOf(name);
					var csv = child.FirstChild.FirstChild.Value.RemoveWhitespace();
					var list = csv.Split(',').ToList().ConvertAll(s => int.Parse(s) - firstgid);
					State.Layers[index].Tiles = list;
				}
			}
			Load();
			Manager.Instance.PathFinder.SetupMap();
			var p = new Vector2(4, 4);
			Manager.Instance.Character.transform.localPosition = p;
			Manager.Instance.PathFinder.ReachableFrom(p);
		}
		public void Load(string json)
		{
			JsonUtility.FromJsonOverwrite(json, State);
			Mesh = BuildMesh();
			Load();
		}
		Mesh BuildMesh()
		{
			var quads = State.Width * State.Height;
			var vertices = new Vector3[quads * 4];
			var triangles = new int[quads * 6];
			var normals = new Vector3[vertices.Length];
			var uv = new Vector2[vertices.Length];
			for (var y = 0; y < State.Height; y++)
			{
				for (var x = 0; x < State.Width; x++)
				{
					var i = TileIndex(x, y);
					var vi = i * 4;
					var ti = i * 6;
					vertices[vi] = new Vector2(x, y);
					vertices[vi + 1] = new Vector2(x + 1, y);
					vertices[vi + 2] = new Vector2(x, y + 1);
					vertices[vi + 3] = new Vector2(x + 1, y + 1);
					triangles[ti] = vi;
					triangles[ti + 1] = vi + 2;
					triangles[ti + 2] = vi + 3;
					triangles[ti + 3] = vi;
					triangles[ti + 4] = vi + 3;
					triangles[ti + 5] = vi + 1;
				}
			}
			for (var i = 0; i < vertices.Length; i++)
			{
				normals[i] = Vector3.forward;
				uv[i] = Vector2.zero;
			}
			var mesh = new Mesh
			{
				vertices = vertices,
				triangles = triangles,
				normals = normals,
				uv = uv,
				name = "TileMapMesh"
			};
			return mesh;
		}
		public void Load()
		{
			for (var layer = 0; layer < State.Layers.Count; layer++)
				for (var tile = 0; tile < State.Layers[layer].Tiles.Count; tile++)
					SetTile(layer, tile, GetTile(layer, tile), GetTileFlags(layer, tile), false);
			Commit();
		}
		public void Clear()
		{
			for (var layer = 0; layer < State.Layers.Count; layer++)
				for (var tile = 0; tile < State.Layers[layer].Tiles.Count; tile++)
					SetTile(layer, tile, -1, TileFlags.Nothing, false);
			Commit();
		}
		public void Apply(StateMap map, int oldWidth)
		{
			for (var layer = 0; (layer < map.Layers.Count) && (layer < State.Layers.Count); layer++)
			{
				for (var tile = 0; tile < map.Layers[layer].Tiles.Count; tile++)
				{
					var p = TilePosition(tile, oldWidth);
					if (InsideMap(p))
					{
						var index = TileIndex(p, oldWidth);
						var oldTile = map.Layers[layer].Tiles[index];
						var oldFlags = map.Layers[layer].Flags[index];
						SetTile(layer, (int)p.x, (int)p.y, oldTile, oldFlags, false);
					}
				}
			}
			Commit();
		}
		public int GetTile(int layer, Vector2 p)
		{
			return GetTile(layer, (int)p.x, (int)p.y);
		}
		public int GetTile(int layer, int x, int y)
		{
			return GetTile(layer, TileIndex(x, y));
		}
		public int GetTile(int layer, int index)
		{
			return State.Layers[layer].Tiles[index];
		}
		public TileFlags GetTileFlags(int layer, Vector2 p)
		{
			return GetTileFlags(layer, (int)p.x, (int)p.y);
		}
		public TileFlags GetTileFlags(int layer, int x, int y)
		{
			return GetTileFlags(layer, TileIndex(x, y));
		}
		public TileFlags GetTileFlags(int layer, int index)
		{
			return State.Layers[layer].Flags[index];
		}
		public void SetTileFlags(int layer, int x, int y, TileFlags flags)
		{
			SetTile(layer, x, y, GetTile(layer, x, y), flags);
		}
		public void SetTile(int layer, int x, int y, int tile, bool commit = true)
		{
			SetTile(layer, TileIndex(x, y), tile, commit);
		}
		public void SetTile(int layer, int index, int tile, bool commit = true)
		{
			SetTile(layer, index, tile, GetTileFlags(layer, index), commit);
		}
		public void SetTile(int layer, int x, int y, int tile, TileFlags flags, bool commit = true)
		{
			SetTile(layer, TileIndex(x, y), tile, flags, commit);
		}
		public void SetTile(int layer, int index, int tile, TileFlags flags, bool commit = true, bool find = true)
		{
			State.Layers[layer].Tiles[index] = tile;
			State.Layers[layer].Flags[index] = flags;
			var uv = _uv[layer];
			if (find)
				FindAnimation(layer, index, tile);
			index *= 4;
			if (tile < 0)
			{
				uv[index + 0] = Vector2.zero;
				uv[index + 1] = Vector2.zero;
				uv[index + 2] = Vector2.zero;
				uv[index + 3] = Vector2.zero;
			}
			else
			{
				var ppu = Sheet.pixelsPerUnit;
				var width = Sheet.texture.width;
				var height = Sheet.texture.height;
				var tp = TilePosition(tile, (int)(width / ppu));
				tp = new Vector2(tp.x * ppu, tp.y * ppu);
				tp.y = height - tp.y - ppu;
				var rect = new Rect(tp.x, tp.y, ppu, ppu);
				var temp0 = uv[index + 0] = new Vector2(rect.xMin / width, rect.yMin / height);
				var temp1 = uv[index + 1] = new Vector2(rect.xMax / width, rect.yMin / height);
				var temp2 = uv[index + 2] = new Vector2(rect.xMin / width, rect.yMax / height);
				var temp3 = uv[index + 3] = new Vector2(rect.xMax / width, rect.yMax / height);
				if ((flags & TileFlags.Rot90) == TileFlags.Rot90)
				{
					uv[index + 0] = temp2;
					uv[index + 1] = temp0;
					uv[index + 2] = temp3;
					uv[index + 3] = temp1;
				}
				if ((flags & TileFlags.FlipX) == TileFlags.FlipX)
				{
					uv[index + 0] = temp1;
					uv[index + 1] = temp0;
					uv[index + 2] = temp3;
					uv[index + 3] = temp2;
				}
				if ((flags & TileFlags.FlipY) == TileFlags.FlipY)
				{
					uv[index + 0] = temp2;
					uv[index + 1] = temp3;
					uv[index + 2] = temp0;
					uv[index + 3] = temp1;
				}
			}
			_dirty |= commit;
		}
		public void Commit()
		{
			for (var i = 0; i < _layers.Count; i++)
				Commit(i);
		}
		public void Commit(int layer)
		{
			_layers[layer].mesh.uv = _uv[layer];
		}
		bool _dirty = false;
		void LateUpdate()
		{
			if (_dirty)
			{
				_dirty = false;
				Commit();
			}
		}
		public class Animation
		{
			public float Fps;
			public int[][] Frames;
			public bool Sync;
		}
		protected virtual List<Animation> GetAnimations()
		{
			return null;
		}
		void FindAnimation(int layer, int index, int tile)
		{
			if (_animations == null)
				return;
			IEnumerator current;
			if (_runningAnimations[layer].TryGetValue(index, out current))
			{
				StopCoroutine(current);
				_runningAnimations[layer].Remove(index);
			}
			for (var i = 0; i < _animations.Count; i++)
			{
				var a = _animations[i];
				if (a.Frames[0].Contains(tile))
				{
					IEnumerator test = Animate(layer, index, a.Frames, a.Fps, a.Sync);
					_runningAnimations[layer].Add(index, test);
					StartCoroutine(test);
				}
			}
		}
		IEnumerator Animate(int layer, int index, int[][] animations, float fps, bool sync)
		{
			var time = 1f / fps;
			var animationIndex = 0;
			var frameIndex = sync ? 0 : Utility.Random.Next(animations[animationIndex].Length);
			yield return null;
			while (true)
			{
				if (frameIndex >= animations[animationIndex].Length)
				{
					if (animations.Length > 1)
						animationIndex = Utility.Random.Next(animations.Length);
					frameIndex = 0;
				}
				SetTile(layer, index, animations[animationIndex][frameIndex], GetTileFlags(layer, index), true, false);
				frameIndex++;
				yield return new WaitForSeconds(time);
			}
		}
		public bool Blocked(Vector2 p) { return Blocked((int)p.x, (int)p.y); }
		public bool Blocked(int x, int y) { return Blocked(TileIndex(x, y)); }
		public virtual bool Blocked(int index)
		{
			return false;
		}
		public bool IsDoor(Vector2 p) { return IsDoor((int)p.x, (int)p.y); }
		public bool IsDoor(int x, int y) { return IsDoor(TileIndex(x, y)); }
		public bool IsDoor(int index)
		{
			return IsDoorOpen(index) || IsDoorShut(index);
		}
		public bool IsDoorOpen(Vector2 p) { return IsDoorOpen((int)p.x, (int)p.y); }
		public bool IsDoorOpen(int x, int y) { return IsDoorOpen(TileIndex(x, y)); }
		public virtual bool IsDoorOpen(int index)
		{
			return false;
		}
		public bool IsDoorShut(Vector2 p) { return IsDoorShut((int)p.x, (int)p.y); }
		public bool IsDoorShut(int x, int y) { return IsDoorShut(TileIndex(x, y)); }
		public virtual bool IsDoorShut(int index)
		{
			return false;
		}
		public bool IsStair(Vector2 p) { return IsStair((int)p.x, (int)p.y); }
		public bool IsStair(int x, int y) { return IsStair(TileIndex(x, y)); }
		public bool IsStair(int index)
		{
			return IsStairDown(index) || IsStairUp(index);
		}
		public bool IsStairDown(Vector2 p) { return IsStairDown((int)p.x, (int)p.y); }
		public bool IsStairDown(int x, int y) { return IsStairDown(TileIndex(x, y)); }
		public virtual bool IsStairDown(int index)
		{
			return false;
		}
		public bool IsStairUp(Vector2 p) { return IsStairUp((int)p.x, (int)p.y); }
		public bool IsStairUp(int x, int y) { return IsStairUp(TileIndex(x, y)); }
		public virtual bool IsStairUp(int index)
		{
			return false;
		}
		public Color GetColor(Vector2 p)
		{
			return GetColor((int)p.x, (int)p.y);
		}
		public Color GetColor(int x, int y)
		{
			return GetColor(TileIndex(x, y));
		}
		public virtual Color GetColor(int index)
		{
			var color = Colors.Green;
			if (IsDoor(index))
				color = Colors.Blue;
			else if (IsStair(index))
				color = Colors.Yellow;
			return color;
		}
	}
}
