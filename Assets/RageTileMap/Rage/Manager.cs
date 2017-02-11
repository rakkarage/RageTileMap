using System.Collections.Generic;
namespace ca.HenrySoftware.Rage
{
	public class Manager : Singleton<Manager>
	{
		public Mob Character;
		public List<Mob> Mobs;
		public TileMap TileMap;
		public Indicator Indicator;
		public PathFinder PathFinder;
	}
}
