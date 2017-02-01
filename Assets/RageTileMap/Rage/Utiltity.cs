using System;
using System.Linq;
using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	public enum DirectionType
	{
		None = -1,
		North,
		East,
		South,
		West,
		NorthEast,
		SouthEast,
		SouthWest,
		NorthWest,
	}
	public static partial class Utility
	{
		public static DirectionType GetDirection(Vector2 delta)
		{
			var facing = DirectionType.None;
			if ((delta.x > 0f) && (delta.y > 0f))
				facing = DirectionType.NorthEast;
			else if ((delta.x > 0f) && (delta.y < 0f))
				facing = DirectionType.SouthEast;
			else if ((delta.x < 0f) && (delta.y < 0f))
				facing = DirectionType.SouthWest;
			else if ((delta.x < 0f) && (delta.y > 0f))
				facing = DirectionType.NorthWest;
			else if (delta.y > 0f)
				facing = DirectionType.North;
			else if (delta.x > 0f)
				facing = DirectionType.East;
			else if (delta.y < 0f)
				facing = DirectionType.South;
			else if (delta.x < 0f)
				facing = DirectionType.West;
			return facing;
		}
		public static DirectionType GetDirection(Vector2 delta, Vector2 pathDelta)
		{
			var trending = Mathf.Abs(pathDelta.y) > Mathf.Abs(pathDelta.x);
			var facing = DirectionType.None;
			if ((delta.x > 0f) && (delta.y > 0f))
				facing = trending ? DirectionType.North : DirectionType.East;
			else if ((delta.x > 0f) && (delta.y < 0f))
				facing = trending ? DirectionType.South : DirectionType.East;
			else if ((delta.x < 0f) && (delta.y < 0f))
				facing = trending ? DirectionType.South : DirectionType.West;
			else if ((delta.x < 0f) && (delta.y > 0f))
				facing = trending ? DirectionType.North : DirectionType.West;
			else if (delta.y > 0f)
				facing = DirectionType.North;
			else if (delta.x > 0f)
				facing = DirectionType.East;
			else if (delta.y < 0f)
				facing = DirectionType.South;
			else if (delta.x < 0f)
				facing = DirectionType.West;
			return facing;
		}
		static public Vector2 ConstrainRect(Bounds screen, Bounds map)
		{
			return ConstrainRect(screen.min, screen.max, map.min, map.max);
		}
		static public Vector2 ConstrainRect(Vector2 minScreen, Vector2 maxScreen, Vector2 minMap, Vector2 maxMap)
		{
			var offset = Vector2.zero;
			var screenWidth = maxScreen.x - minScreen.x;
			var screenHeight = maxScreen.y - minScreen.y;
			var mapWidth = maxMap.x - minMap.x;
			var mapHeight = maxMap.y - minMap.y;
			if (screenWidth > mapWidth)
			{
				var diff = screenWidth - mapWidth;
				minMap.x -= diff;
				maxMap.x += diff;
			}
			if (screenHeight > mapHeight)
			{
				var diff = screenHeight - mapHeight;
				minMap.y -= diff;
				maxMap.y += diff;
			}
			if (minScreen.x < minMap.x) offset.x += minMap.x - minScreen.x;
			if (maxScreen.x > maxMap.x) offset.x -= maxScreen.x - maxMap.x;
			if (minScreen.y < minMap.y) offset.y += minMap.y - minScreen.y;
			if (maxScreen.y > maxMap.y) offset.y -= maxScreen.y - maxMap.y;
			return offset;
		}
	}
	public static partial class CameraExtensions
	{
		public static Bounds OrthographicBounds(this Camera camera)
		{
			var ratio = (float)Screen.width / (float)Screen.height;
			var height = camera.orthographicSize * 2f;
			var p = camera.transform.localPosition;
			return new Bounds(new Vector3(p.x, p.y, 0f), new Vector3(height * ratio, height, 2f));
		}
	}
	public static partial class StringExtensions
	{
		public static string RemoveWhitespace(this string input)
		{
			return new string(input.Where(c => !Char.IsWhiteSpace(c)).ToArray());
		}
	}
	public static class Constants
	{
		public const float TimeTurn = .1333f;
		public const float TimeTween = .333f;
		public static int AnimatorWalk = Animator.StringToHash("Walk");
		public static int AnimatorAttack = Animator.StringToHash("Attack");
	}
	public static class Colors
	{
		public static Color Disabled = new Color(.5f, .5f, .5f, .5f);
		public static Color ErrorWhite = new Color(.75f, .75f, .75f);
		public static Color ErrorRed = new Color(.75f, .5f, .5f);
		public static Color ErrorGreen = new Color(.5f, .75f, .5f);
		public static Color ErrorBlue = new Color(.5f, .5f, .75f);
		public static Color ErrorYellow = new Color(.75f, .75f, .5f);
		public static Color TargetRed = new Color(1f, .5f, .5f);
		public static Color TargetGreen = new Color(.5f, 1f, .5f);
		public static Color TargetBlue = new Color(.5f, .5f, 1f);
		public static Color TargetYellow = new Color(1f, 1f, .5f);

		public static Color ButtonBlue = new Color32(159, 176, 255, 255);
		public static Color HenryBlue = new Color32(59, 67, 82, 255);
		public static Color MapWaterDark = new Color32(159, 159, 191, 255);
		public static Color MapWaterLight = new Color32(223, 223, 255, 255);

		public static Color GreyLight = new Color(.75f, .75f, .75f);
		public static Color Grey = new Color(.5f, .5f, .5f);
		public static Color GreyDark = new Color(.25f, .25f, .25f);

		public static Color MagentaLight = new Color(.75f, .25f, .75f);
		public static Color Magenta = new Color(.5f, .125f, .5f);
		public static Color MagentaDark = new Color(.25f, 0f, .25f);

		public static Color CyanLight = new Color(.25f, .75f, .75f);
		public static Color Cyan = new Color(.125f, .5f, .5f);
		public static Color CyanDark = new Color(0f, .25f, .25f);

		public static Color RedLight = new Color(.75f, .25f, .25f);
		public static Color Red = new Color(.5f, .125f, .125f);
		public static Color RedDark = new Color(.25f, 0f, 0f);

		public static Color GreenLight = new Color(.25f, .75f, .25f);
		public static Color Green = new Color(.125f, .5f, .125f);
		public static Color GreenDark = new Color(0f, .25f, 0f);

		public static Color BlueLight = new Color(.25f, .25f, .75f);
		public static Color Blue = new Color(.125f, .125f, .5f);
		public static Color BlueDark = new Color(0f, 0f, .25f);

		public static Color YellowLight = new Color(.75f, .75f, .25f);
		public static Color Yellow = new Color(.5f, .5f, .125f);
		public static Color YellowDark = new Color(.25f, .25f, 0f);
	}
}
