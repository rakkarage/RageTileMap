using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	public static partial class Utility
	{
		public static class Random
		{
			static System.Random _random = new System.Random();
			public static int Next(int max)
			{
				return _random.Next(max);
			}
			public static int Next(int min, int max)
			{
				return _random.Next(min, max);
			}
			public static int NextEven(int min, int max)
			{
				return Next(min / 2, max / 2) * 2;
			}
			public static int NextOdd(int min, int max)
			{
				return Next(min, max) + 1;
			}
			public static double NextDouble()
			{
				return _random.NextDouble();
			}
			public static double NextDouble(double max)
			{
				return NextDouble() * max;
			}
			public static double NextDouble(double min, double max)
			{
				return min + NextDouble() * (max - min);
			}
			public static float NextFloat()
			{
				return (float)NextDouble();
			}
			public static float NextFloat(float max)
			{
				return (float)NextDouble(max);
			}
			public static float NextFloat(float min, float max)
			{
				return (float)NextDouble(min, max);
			}
			public static bool NextBool()
			{
				return NextDouble() > 0.5;
			}
			public static bool NextPercent(double target)
			{
				return NextDouble() < target;
			}
			public static Color NextColor()
			{
				return new Color((float)NextDouble(), (float)NextDouble(), (float)NextDouble());
			}
		}
	}
}
