using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	[CustomEditor(typeof(TileMap), true)]
	public class TileMapEditor : Editor
	{
		static Vector3 _mouse;
		static int _tileX;
		static int _usedX;
		static int _tileY;
		static int _usedY;
		int _palleteX;
		int _palleteY;
		bool _tiles = true;
		static bool _edit = false;
		int _layer;
		TileFlags _flags;
		bool _randomFlags;
		bool _fill;
		public override void OnInspectorGUI()
		{
			var element = target as TileMap;
			DrawDefaultInspector();
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Build"))
				element.Build();
			if (GUILayout.Button("Rebuild"))
				element.Rebuild();
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Save"))
			{
				var path = EditorUtility.SaveFilePanel("Save map...", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), element.name, "json");
				if (!string.IsNullOrEmpty(path))
					File.WriteAllText(path, JsonUtility.ToJson(element.State));
			}
			if (GUILayout.Button("Load"))
			{
				var path = EditorUtility.OpenFilePanel("Load map...", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "json");
				if (!string.IsNullOrEmpty(path))
				{
					element.State = JsonUtility.FromJson<StateMap>(File.ReadAllText(path));
					element.Build(element.State);
					element.Load();
				}
			}
			GUILayout.EndHorizontal();
			if (GUILayout.Button("Import Tiled TMX CSV"))
			{
				var path = EditorUtility.OpenFilePanel("Load map...", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "tmx");
				if (!string.IsNullOrEmpty(path))
				{
					element.LoadTmx(File.ReadAllText(path));
				}
			}
			if (GUILayout.Button("Clear"))
				element.Clear();
			_edit = EditorGUILayout.Foldout(_edit, "Edit");
			if (_edit)
			{
				EditorGUILayout.HelpBox("In Scene View: Left Button: Draw, Right Button: Erase", MessageType.Info);
				_layer = EditorGUILayout.IntSlider("Layer", _layer, 0, element.LayerNames.Count - 1);
				_flags = (TileFlags)EditorGUILayout.EnumMaskField("Flags", _flags);
				_randomFlags = EditorGUILayout.Toggle("Random Flags", _randomFlags);
				_fill = EditorGUILayout.Toggle("Fill", _fill);
				_tiles = EditorGUILayout.Foldout(_tiles, "Tile Sheet");
				if (_tiles)
				{
					EditorGUILayout.HelpBox("Select a tile to draw with.", MessageType.Info);
					var width = Mathf.Min(Screen.width - EditorGUIUtility.singleLineHeight * 2, element.Sheet.texture.width);
					var scale = width / element.Sheet.texture.width;
					var height = element.Sheet.texture.height * scale;
					var columns = element.Sheet.texture.width / element.Sheet.pixelsPerUnit;
					var rows = element.Sheet.texture.height / element.Sheet.pixelsPerUnit;
					var sizex = width / columns;
					var sizey = height / rows;
					var r = EditorGUILayout.GetControlRect(false, height);
					r.width = width;
					EditorGUI.DrawTextureTransparent(r, element.Sheet.texture);
					if (Event.current.type == EventType.MouseUp && r.Contains(Event.current.mousePosition))
					{
						var p = Event.current.mousePosition - new Vector2(r.x, r.y);
						var tilesAcross = element.Sheet.texture.width / element.Sheet.pixelsPerUnit;
						var index = element.TileIndex((int)(p.x / sizex), (int)(p.y / sizey), (int)tilesAcross);
						p = element.TilePosition(index, (int)tilesAcross);
						_palleteX = (int)p.x;
						_palleteY = (int)p.y;
						EditorUtility.SetDirty(element);
					}
					var size = element.Sheet.pixelsPerUnit * scale;
					var rect0 = new Rect(r.x + (_palleteX * size), r.y + (_palleteY * size), size, size);
					EditorGUI.DrawRect(rect0, new Color(1f, 1f, 1f, .333f));
					var rect1 = new Rect(r.x + (_palleteX * size) + 2, r.y + (_palleteY * size) + 2, size - 4, size - 4);
					EditorGUI.DrawRect(rect1, new Color(1f, .5f, .5f, .333f));
				}
			}
		}
		void OnSceneGUI()
		{
			HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
			if (UpdateMouse())
				SceneView.RepaintAll();
			if (_edit && MouseOver())
			{
				var current = Event.current;
				var used = _usedX == _tileX && _usedY == _tileY;
				if (current.type == EventType.MouseDown || (current.type == EventType.MouseDrag && !used))
				{
					if (current.button == 1)
					{
						current.Use();
						_usedX = _tileX;
						_usedY = _tileY;
						Erase();
					}
					else if (current.button == 0)
					{
						current.Use();
						_usedX = _tileX;
						_usedY = _tileY;
						Draw();
					}
				}
			}
		}
		TileFlags GetFlags()
		{
			var flags = TileFlags.Nothing;
			if (_randomFlags)
			{
				if (((_flags & TileFlags.FlipX) == TileFlags.FlipX) && Utility.Random.NextBool())
					flags |= TileFlags.FlipX;
				if (((_flags & TileFlags.FlipY) == TileFlags.FlipY) && Utility.Random.NextBool())
					flags |= TileFlags.FlipY;
				if (((_flags & TileFlags.Rot90) == TileFlags.Rot90) && Utility.Random.NextBool())
					flags |= TileFlags.Rot90;
			}
			else
				flags = _flags;
			return flags;
		}
		void Draw()
		{
			var element = target as TileMap;
			var index = element.TileIndex(_tileX, _tileY);
			var tilesAcross = element.Sheet.texture.width / element.Sheet.pixelsPerUnit;
			var newTile = element.TileIndex(_palleteX, _palleteY, (int)tilesAcross);
			if (_fill && (element.GetTile(_layer, index) == -1))
				Fill(element, _layer, _tileX, _tileY, newTile);
			else
				element.SetTile(_layer, index, newTile, GetFlags());
		}
		void Fill(TileMap tileMap, int layer, int x, int y, int tile)
		{
			var points = new Stack<Vector2>();
			points.Push(new Vector2(x, y));
			while (points.Count > 0)
			{
				var current = points.Pop();
				var index = tileMap.TileIndex((int)current.x, (int)current.y);
				if (tileMap.GetTile(layer, index) == -1)
					tileMap.SetTile(layer, index, tile, GetFlags());
				for (var yy = (int)current.y - 1; yy <= (int)current.y + 1; yy++)
				{
					for (var xx = (int)current.x - 1; xx <= (int)current.x + 1; xx++)
					{
						if ((yy != current.y || xx != current.x) && tileMap.InsideMap(xx, yy))
						{
							var checkTile = tileMap.GetTile(layer, tileMap.TileIndex(xx, yy));
							if (checkTile == -1)
								points.Push(new Vector2(xx, yy));
						}
					}
				}
			}
		}
		void Erase()
		{
			var element = target as TileMap;
			var index = element.TileIndex(_tileX, _tileY);
			element.SetTile(_layer, index, -1);
		}
		bool UpdateMouse()
		{
			var element = target as TileMap;
			var plane = new Plane(element.transform.TransformDirection(Vector3.forward), element.transform.localPosition);
			var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
			var hit = Vector3.zero;
			float dist;
			if (plane.Raycast(ray, out dist))
			{
				hit = ray.origin + (ray.direction.normalized * dist);
			}
			var changed = false;
			var p = element.transform.InverseTransformPoint(hit);
			if (p != _mouse)
			{
				_mouse = p;
				_tileX = Mathf.Clamp((int)Math.Round(_mouse.x, 5, MidpointRounding.ToEven), 0, element.State.Width - 1);
				_tileY = Mathf.Clamp((int)Math.Round(_mouse.y, 5, MidpointRounding.ToEven), 0, element.State.Height - 1);
				changed = true;
			}
			return changed;
		}
		bool MouseOver()
		{
			var element = target as TileMap;
			return (_mouse.x > 0) && (_mouse.x < element.State.Width) &&
				(_mouse.y > 0) && (_mouse.y < element.State.Height);
		}
		[DrawGizmo(GizmoType.Selected | GizmoType.Active)]
		static void RenderMapGizmo(TileMap tileMap, GizmoType gizmoType)
		{
			if (tileMap.Mesh == null || tileMap.State == null || tileMap.State.Layers == null || tileMap.State.Layers.Count == 0)
				return;
			var p = tileMap.transform.localPosition;
			var width = tileMap.Mesh.bounds.size.x;
			var height = tileMap.Mesh.bounds.size.y;
			if (_edit)
			{
				Gizmos.color = Color.green;
				for (float i = 1; i < width; i++)
				{
					Gizmos.DrawLine(p + new Vector3(i, 0), p + new Vector3(i, height));
				}
				for (float i = 1; i < height; i++)
				{
					Gizmos.DrawLine(p + new Vector3(0, i), p + new Vector3(width, i));
				}
				Gizmos.color = Color.red;
				Gizmos.DrawWireCube(new Vector3(_tileX, _tileY, p.z) + tileMap.transform.localPosition + new Vector3(.5f, .5f), new Vector3(1.1f, 1.1f, .2f));
			}
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(p, p + new Vector3(width, 0));
			Gizmos.DrawLine(p, p + new Vector3(0, height));
			Gizmos.DrawLine(p + new Vector3(width, 0), p + new Vector3(width, height));
			Gizmos.DrawLine(p + new Vector3(0, height), p + new Vector3(width, height));
		}
		public override bool HasPreviewGUI()
		{
			return true;
		}
		public override void OnInteractivePreviewGUI(Rect rect, GUIStyle style)
		{
			var element = target as TileMap;
			if (element != null && element.Sheet != null)
				EditorGUI.DrawTextureTransparent(rect, element.Sheet.texture);
		}
	}
}
