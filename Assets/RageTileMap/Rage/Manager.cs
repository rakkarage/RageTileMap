using System.Collections.Generic;
using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	public class Manager : Singleton<Manager>
	{
		public Mob Character;
		public List<Mob> Mobs;
		public TileMap TileMap;
		public Indicator Indicator;
		public PathFinder PathFinder;
		private string ToJson(StateMap map)
		{
			return JsonUtility.ToJson(map);
		}
		private StateMap FromJson(string map)
		{
			return (StateMap)JsonUtility.FromJson(map, typeof(StateMap));
		}
		[ContextMenu("SaveJsonPref")]
		public void SaveJsonPref()
		{
			Prefs.State = ToJson(TileMap.State);
		}
		[ContextMenu("LoadJsonPref")]
		public void LoadJsonPref()
		{
			TileMap.State = FromJson(Prefs.State);
		}
	}
}
