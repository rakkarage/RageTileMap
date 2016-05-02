using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace HenrySoftware.Rage
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
		Dictionary<int, IEnumerator> _runningAnimations = new Dictionary<int, IEnumerator>();
		[NonSerialized]
		public Mesh Mesh;
		public StateMap Map;
		void Awake()
		{
			_t = transform;
			_animations = GetAnimations();
		}
		public int TileIndex(Vector2 p) { return TileIndex((int)p.x, (int)p.y); }
		public int TileIndex(Vector2 p, int width) { return TileIndex((int)p.x, (int)p.y, width); }
		public int TileIndex(int x, int y) { return TileIndex(x, y, Map.Width); }
		public int TileIndex(int x, int y, int width)
		{
			return y * width + x;
		}
		public Vector2 TilePosition(int index) { return TilePosition(index, Map.Width); }
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
				(x < Map.Width - edge) && (y < Map.Height - edge);
		}
		public void Rebuild()
		{
			if (Mesh != null)
			{
				var oldTilesAcross = (int)Mesh.bounds.size.x;
				var saved = JsonUtility.ToJson(Map);
				var savedMap = JsonUtility.FromJson<StateMap>(saved);
				Build(Map.Width, Map.Height);
				Apply(savedMap, oldTilesAcross);
			}
			else
			{
				Build(Map.Width, Map.Height);
			}
		}
		public void Build()
		{
			Build(Map.Width, Map.Height);
		}
		public void Build(int width, int height)
		{
			var tileCount = width * height;
			var emptyTiles = Enumerable.Repeat(-1, tileCount).ToList();
			var emptyFlags = Enumerable.Repeat(TileFlags.Nothing, tileCount).ToList();
			var count = LayerNames.Count;
			var layers = new List<StateLayer>(count);
			for (var i = 0; i < count; i++)
				layers.Add(new StateLayer() { Tiles = new List<int>(emptyTiles), Flags = new List<TileFlags>(emptyFlags) });
			var map = new StateMap() {Width = width, Height = height, Layers = layers};
			Build(map);
		}
		public void Build(StateMap map)
		{
			DestroyChildren();
			_layers.Clear();
			_uv.Clear();
			Map = map;
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
		public void Load(string json)
		{
			JsonUtility.FromJsonOverwrite(json, Map);
			Mesh = BuildMesh();
			Load();
		}
		Mesh BuildMesh()
		{
			var quads = Map.Width * Map.Height;
			var vertices = new Vector3[quads * 4];
			var triangles = new int[quads * 6];
			var normals = new Vector3[vertices.Length];
			var uv = new Vector2[vertices.Length];
			for (var y = 0; y < Map.Height; y++)
			{
				for (var x = 0; x < Map.Width; x++)
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
			for (var layer = 0; layer < Map.Layers.Count; layer++)
				for (var tile = 0; tile < Map.Layers[layer].Tiles.Count; tile++)
					SetTile(layer, tile, GetTile(layer, tile), GetTileFlags(layer, tile), false);
			Commit();
		}
		public void Clear()
		{
			for (var layer = 0; layer < Map.Layers.Count; layer++)
				for (var tile = 0; tile < Map.Layers[layer].Tiles.Count; tile++)
					SetTile(layer, tile, -1, TileFlags.Nothing, false);
			Commit();
		}
		public void Apply(StateMap map, int oldWidth)
		{
			for (var layer = 0; (layer < map.Layers.Count) && (layer < Map.Layers.Count); layer++)
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
			return Map.Layers[layer].Tiles[index];
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
			return Map.Layers[layer].Flags[index];
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
			Map.Layers[layer].Tiles[index] = tile;
			Map.Layers[layer].Flags[index] = flags;
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
			if (_runningAnimations.TryGetValue(index, out current))
			{
				StopCoroutine(current);
				_runningAnimations.Remove(index);
			}
			for (var i = 0; i < _animations.Count; i++)
			{
				var a = _animations[i];
				if (a.Frames[0].Contains(tile))
				{
					IEnumerator test = Animate(layer, index, a.Frames, a.Fps, a.Sync);
					_runningAnimations[index] = test;
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
	}
}
