using System;
namespace ca.HenrySoftware.Rage
{
	public class PathNode : IComparable<PathNode>
	{
		public int X;
		public int Y;
		public bool Walkable;
		public bool Reachable;
		public PathNode Parent;
		public int TileIndex;
		public int SortedIndex;
		public int F; // G + H
		public int G; // real cost from start to here
		public int H; // estimated cost from here to goal
		public int CompareTo(PathNode b)
		{
			var compare = F.CompareTo(b.F);
			if (compare == 0) compare = H.CompareTo(b.H);
			return compare;
		}
		public override string ToString()
		{
			return TileIndex + " (" + X + ", " + Y + ") F:" + F + " H:" + H + " W:" + Walkable + " R:" + Reachable;
		}
	}
}
