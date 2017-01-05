using System;
using System.Collections.Generic;
using UnityEngine;
namespace HenrySoftware.Rage
{
	[Serializable]
	public class StateGame
	{
		public string Name;
		public int Depth;
		public int Experience;
		public int Level;
		public bool Started;
		public bool Dead;
		public StateMap Map;
	}
	public class Manager : Singleton<Manager>
	{
		public StateGame State;
		public Mob Character;
		public List<Mob> Mobs;
		public TileMap TileMap;
		public Indicator Indicator;
		public PathFinder PathFinder;
		bool SomethingToSave()
		{
			return State.Started && !State.Dead;
		}
		void OnApplicationFocus(bool focus)
		{
			if (!focus && SomethingToSave())
				Save();
		}
		void OnApplicationPause(bool pause)
		{
			if (pause && SomethingToSave())
				Save();
		}
		void OnApplicationQuit()
		{
			if (SomethingToSave())
				Save();
		}
		public void Save()
		{
			Prefs.State = JsonUtility.ToJson(State);
		}
		public void Load()
		{
			State = (StateGame)JsonUtility.FromJson(Prefs.State, typeof(StateGame));
		}
		public bool SaveExistAndNotDead()
		{
			return !string.IsNullOrEmpty(State.Name) && !State.Dead;
		}
	}
}
