using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ca.HenrySoftware.Rage;
public partial class FreeTileMap : TileMap
{
	public enum Layer
	{
		Back,
		Fore,
		Light
	}
	private const int TilesAcross = 32;
	public enum Tile
	{
		None = -1,
		Floor0, Floor1, Floor2, Floor3, Floor4, Floor5,
		FloorRoom0, FloorRoom1, FloorRoom2, FloorRoom3, FloorRoom4, FloorRoom5,
		StairsDown, StairsUp,
		DoorShut, DoorOpen, DoorBroke,
		WallTorch0, WallTorch1, WallTorch2, WallTorch3,
		Wall0, Wall1, Wall2, Wall3, Wall4, Wall5, Wall6, Wall7,
		Rug0 = TilesAcross, Rug1, Rug2, Rug3, Rug4, Rug5,
		Bed, Sacks,
		Chair0, Chair1, Chair2,
		Table, Stool, Globe, Bellows, Forge, Anvil, Workbench,
		BannerA0, BannerA1, BannerA2, BannerA3, BannerA4, BannerA5,
		BannerB0, BannerB1, BannerB2, BannerB3, BannerB4,
		Light0 = TilesAcross * 2,
		Light1, Light2, Light3, Light4, Light5, Light6, Light7, Light8, Light9, Light10,
		Light11, Light12, Light13, Light14, Light15, Light16, Light17, Light18, Light19, Light20,
		Light21, Light22, Light23, Light24, Light25, Light26, Light27, Light28, Light29, Light30,
		Light31,
	};
	const float _torchSpeed = 8f;
	static readonly int[] _torch =
		new Tile[] { Tile.WallTorch0, Tile.WallTorch1, Tile.WallTorch2, Tile.WallTorch3 }.Cast<int>().ToArray();
	const float _bannerSpeed = 4f;
	static readonly int[][] _bannerA =
	{
		new Tile[] { Tile.BannerA0, Tile.BannerA1, Tile.BannerA2, Tile.BannerA3, Tile.BannerA4, Tile.BannerA5 }.Cast<int>().ToArray(),
		new Tile[] { Tile.BannerA0, Tile.BannerA0, Tile.BannerA0, Tile.BannerA0, Tile.BannerA0, Tile.BannerA0 }.Cast<int>().ToArray(),
		new Tile[] { Tile.BannerA5, Tile.BannerA4, Tile.BannerA3, Tile.BannerA2, Tile.BannerA1, Tile.BannerA0 }.Cast<int>().ToArray()
	};
	static readonly int[][] _bannerB =
	{
		new Tile[] { Tile.BannerB0, Tile.BannerB1, Tile.BannerB2, Tile.BannerB3, Tile.BannerB4 }.Cast<int>().ToArray(),
		new Tile[] { Tile.BannerB0, Tile.BannerB0, Tile.BannerB0, Tile.BannerB0, Tile.BannerB0 }.Cast<int>().ToArray(),
		new Tile[] { Tile.BannerB4, Tile.BannerB3, Tile.BannerB2, Tile.BannerB1, Tile.BannerB0 }.Cast<int>().ToArray()
	};
	protected override List<Animation> GetAnimations()
	{
	    var animations = new List<Animation>
	    {
	        new Animation() {Frames = new int[][] {_torch}, Fps = _torchSpeed},
	        new Animation() {Frames = _bannerA, Fps = _bannerSpeed},
	        new Animation() {Frames = _bannerB, Fps = _bannerSpeed}
	    };
	    return animations;
	}
	public override bool Blocked(int index)
	{
		return IsWall(index) || IsDoorShut(index);
	}
	public override bool IsDoorOpen(int index)
	{
		var fore = GetTile((int)Layer.Fore, index);
		return fore == (int)Tile.DoorOpen;
	}
	public override bool IsDoorShut(int index)
	{
		var fore = GetTile((int)Layer.Fore, index);
		return fore == (int)Tile.DoorShut;
	}
	public override void ToggleDoor(Vector2 p)
	{
		var index = TileIndex(p);
		if (IsDoor(index))
		{
			var shut = IsDoorShut(index);
			SetTile((int)Layer.Fore, index, shut ? (int)Tile.DoorOpen : (int)Tile.DoorShut);
		}
	}
	public override bool IsStairDown(int index)
	{
		var fore = GetTile((int)Layer.Fore, index);
		return fore == (int)Tile.StairsDown;
	}
	public override bool IsStairUp(int index)
	{
		var fore = GetTile((int)Layer.Fore, index);
		return fore == (int)Tile.StairsUp;
	}
	public bool IsTorch(Vector2 p) { return IsTorch((int)p.x, (int)p.y); }
	public bool IsTorch(int x, int y) { return IsTorch(TileIndex(x, y)); }
	public bool IsTorch(int index)
	{
		var fore = GetTile((int)Layer.Fore, index);
		return (fore >= (int)Tile.WallTorch0) && (fore <= (int)Tile.WallTorch3);
	}
	public bool IsWall(Vector2 p) { return IsWall((int)p.x, (int)p.y); }
	public bool IsWall(int x, int y) { return IsWall(TileIndex(x, y)); }
	public bool IsWall(int index)
	{
		var fore = GetTile((int)Layer.Fore, index);
		return (fore >= (int)Tile.WallTorch0) && (fore <= (int)Tile.Wall7);
	}
	public bool IsFloor(Vector2 p) { return IsFloor((int)p.x, (int)p.y); }
	public bool IsFloor(int x, int y) { return IsFloor(TileIndex(x, y)); }
	public bool IsFloor(int index)
	{
		var back = GetTile((int)Layer.Back, index);
		return (back >= (int)Tile.Floor0) && (back <= (int)Tile.FloorRoom5);
	}
	public TextAsset MapTmxToLoad;
	void Start()
	{
		if (MapTmxToLoad != null)
		{
			LoadTmx(MapTmxToLoad.text);
		}
		else
		{
			Build(7, 7, 3, 3);
			Floor();
			Wall();
			Other();
		}
		var c = Manager.Instance.Character.transform.localPosition;
		Manager.Instance.Character.transform.localPosition = new Vector3(State.X, State.Y, c.z);
		var t = Manager.Instance.Indicator.transform.localPosition;
		Manager.Instance.Indicator.transform.localPosition = new Vector3(State.X, State.Y, t.z);
		var p = new Vector2(State.X, State.Y);
		Manager.Instance.CenterOnCharacter();
		Manager.Instance.PathFinder.ReachableFrom(p);
		Dark();
		Light(p);
		FindTorches();
	}
	public void FindTorches()
	{
		_torches.Clear();
		for (var y = 0; y < State.Height; y++)
			for (var x = 0; x < State.Width; x++)
				if (IsTorch(x, y))
					_torches.Add(TileIndex(x, y));
	}
	void Floor()
	{
		for (var y = 0; y < State.Height; y++)
			for (var x = 0; x < State.Width; x++)
				SetTile(0, x, y, (int)Tile.Floor0 + Utility.Random.Next(6));
	}
	void Wall()
	{
		for (var y = 0; y < State.Height; y++)
			for (var x = 0; x < State.Width; x++)
				if (x == 0 || x == State.Width - 1 || y == 0 || y == State.Height - 1)
					SetTile(1, x, y, RandomWall());
	}
	int RandomWall(bool plain = false)
	{
		int tile;
		if (plain)
		{
			tile = (int)Tile.Wall0 + Utility.Random.Next(4);
		}
		else
		{
			var random = Utility.Random.Next(100);
			if (random <= 5) // torch: 5%
			{
				tile = (int)Tile.WallTorch0;
			}
			else if (random <= 15) // special wall: 10%
			{
				tile = (int)Tile.Wall4 + Utility.Random.Next(2);
			}
			else if (random <= 18) // extra special wall: 3%
			{
				tile = (int)Tile.Wall6 + Utility.Random.Next(2);
			}
			else // else normal wall
			{
				tile = (int)Tile.Wall0 + Utility.Random.Next(4);
			}
		}
		return tile;
	}
	void Other()
	{
		SetTile(1, 2, 5, (int)Tile.BannerA0);
		SetTile(1, 3, 5, (int)Tile.Bed);
		SetTile(1, 4, 5, (int)Tile.BannerB0);
		SetTile(1, 2, 4, (int)Tile.Table);
		SetTile(1, 3, 4, (int)Tile.Rug2);
		SetTile(1, 4, 4, (int)Tile.Chair0);
		SetTile(1, 1, 2, (int)Tile.DoorShut);
		var maxX = State.Width - 2;
		for (var i = 2; i <= maxX; i++)
			SetTile(1, i, 2, RandomWall());
		SetTile(1, maxX, 1, (int)Tile.StairsDown);
	}
    const int TorchRadius = 5;
    int RandomTorchRadius()
    {
        return Utility.Random.Next(TorchRadius) + 1;
    }
    public const int LightRadius = 3;
	Tile _themeLightMin = Tile.Light0;
	Tile _themeLightMax = Tile.Light31;
	const int _lightExploredOffset = 7;
	const int _lightCount = 25;
	List<int> _torches = new List<int>();
	public bool IsLight(Vector2 p) { return IsLight((int)p.x, (int)p.y); }
	public bool IsLight(int x, int y) { return IsLight(TileIndex(x, y)); }
	public bool IsLight(int index)
	{
		var tile = GetTile((int)Layer.Light, index);
		if (tile == -1) return false;
		var level = (Tile)tile;
		if ((level > _themeLightMin + _lightExploredOffset) && (level <= _themeLightMax)) return true;
		return false;
	}
	public bool IsExplored(int index)
	{
		var tile = GetTile((int)Layer.Light, index);
		if (tile == -1) return false;
		var level = (Tile)tile;
		if (level == _themeLightMin + _lightExploredOffset) return true;
		return false;
	}
	void SetLight(int x, int y, Tile light, bool test)
	{
		if (InsideMap(x, y))
		{
			var index = TileIndex(x, y);
			var existing = GetTile((int)Layer.Light, index);
			if ((test && ((int)light > existing)) || !test)
				SetTile((int)Layer.Light, index, (int)light, TileFlags.Nothing, false);
		}
	}
	void CommitLight()
	{
		Commit((int)Layer.Light);
	}
	public void Dark()
	{
		Dark(_themeLightMin);
	}
	public void Dark(Tile dark)
	{
		for (var y = 0; y <= State.Height - 1; y++)
		{
			for (var x = 0; x <= State.Width - 1; x++)
			{
				SetLight(x, y, dark, false);
			}
		}
	}
	public void Darken()
	{
		for (var y = 0; y <= State.Height - 1; y++)
		{
			for (var x = 0; x <= State.Width - 1; x++)
			{
				var light = GetTile((int)Layer.Light, x, y);
				if (light != (int)_themeLightMin)
				{
					SetLight(x, y, _themeLightMin + _lightExploredOffset, false);
				}
			}
		}
	}
	static int[,] _fovOctants =
	{
			{1,  0,  0, -1, -1,  0,  0,  1},
			{0,  1, -1,  0,  0, -1,  1,  0},
			{0,  1,  1,  0,  0, -1, -1,  0},
			{1,  0,  0,  1, -1,  0,  0, -1},
		};
	void EmitLightFromRecursive(int x, int y, int radius, int maxRadius, float start, float end, int xx, int xy, int yx, int yy)
	{
		if (start < end) return;
		float rSquare = maxRadius * maxRadius;
		float r2 = maxRadius + maxRadius;
		float newStart = 0.0f;
		for (int i = radius; i <= maxRadius; i++)
		{
			int dx = -i - 1;
			int dy = -i;
			bool isBlocked = false;
			while (dx <= 0)
			{
				dx += 1;
				float mx = x + dx * xx + dy * xy;
				float my = y + dx * yx + dy * yy;
				float lSlope = (dx - 0.5f) / (dy + 0.5f);
				float rSlope = (dx + 0.5f) / (dy - 0.5f);
				if (start < rSlope) continue;
				else if (end > lSlope) break;
				else
				{
					var distanceSquare = (int)((mx - x) * (mx - x) + (my - y) * (my - y));
					if (distanceSquare < rSquare)
					{
						double intensity1 = 1d / (1d + distanceSquare / r2);
						double intensity2 = intensity1 - (1d / (1d + rSquare));
						double intensity = intensity2 / (1d - (1d / (1d + rSquare)));
						var lightIndex = (int)(intensity * _lightCount);
						if (lightIndex > 0)
						{
							Tile light = _themeLightMin + lightIndex + _lightExploredOffset;
							SetLight((int)mx, (int)my, light, true);
						}
					}
					if (!InsideMap((int)mx, (int)my))
						continue;
					if (isBlocked)
					{
						if (Blocked((int)mx, (int)my))
						{
							newStart = rSlope;
							continue;
						}
						else
						{
							isBlocked = false;
							start = newStart;
						}
					}
					else if (Blocked((int)mx, (int)my) && (radius < maxRadius))
					{
						isBlocked = true;
						EmitLightFromRecursive(x, y, i + 1, maxRadius, start, lSlope, xx, xy, yx, yy);
						newStart = rSlope;
					}
				}
			}
			if (isBlocked) break;
		}
	}
	void EmitLightFrom(Vector2 p, int radius)
	{
		EmitLightFrom((int)p.x, (int)p.y, radius);
	}
	void EmitLightFrom(int x, int y, int radius)
	{
		for (int i = 0; i < 8; i++)
		{
			EmitLightFromRecursive(x, y, 1, radius, 1f, 0f, _fovOctants[0, i], _fovOctants[1, i], _fovOctants[2, i], _fovOctants[3, i]);
		}
		SetLight(x, y, _themeLightMax, true);
	}
	public void Light(Vector2 p, int radius = LightRadius)
	{
		Darken();
		EmitLightFrom(p, radius);
		LightTorches();
		CommitLight();
	}
	void LightTorches()
	{
		var repeat = 2;
		while (repeat-- > 0)
		{
			foreach (var index in _torches)
			{
				Vector2 p, north, east, south, west;
				p = north = east = south = west = TilePosition(index);
				north.y += 1;
				east.x += 1;
				south.y -= 1;
				west.x -= 1;
				var emitted = false;
				if (InsideMap(p) && IsWall(index))
				{
					if (InsideMap(north) && IsLight(north) && !IsWall(north) && !IsDoorShut(north))
					{
						emitted = true;
						EmitLightFrom(north, RandomTorchRadius());
					}
					if (InsideMap(east) && IsLight(east) && !IsWall(east) && !IsDoorShut(east))
					{
						emitted = true;
						EmitLightFrom(east, RandomTorchRadius());
					}
					if (InsideMap(south) && IsLight(south) && !IsWall(south) && !IsDoorShut(south))
					{
						emitted = true;
						EmitLightFrom(south, RandomTorchRadius());
					}
					if (InsideMap(west) && IsLight(west) && !IsWall(west) && !IsDoorShut(west))
					{
						emitted = true;
						EmitLightFrom(west, RandomTorchRadius());
					}
					if (!emitted)
					{
						Vector2 northEast, southEast, southWest, northWest;
						northEast = southEast = southWest = northWest = p;
						northEast.y += 1;
						northEast.x += 1;
						northWest.y += 1;
						northWest.x -= 1;
						southEast.y -= 1;
						southEast.x += 1;
						southWest.y -= 1;
						southWest.x -= 1;
						var blockedNorth = !InsideMap(north) || IsWall(north) || IsDoorShut(north);
						var blockedEast = !InsideMap(east) || IsWall(east) || IsDoorShut(east);
						var blockedSouth = !InsideMap(south) || IsWall(south) || IsDoorShut(south);
						var blockedWest = !InsideMap(west) || IsWall(west) || IsDoorShut(west);
						if (InsideMap(northEast) && IsLight(northEast) && !IsWall(northEast) && !IsDoorShut(northEast) && blockedNorth && blockedEast)
							EmitLightFrom(northEast, RandomTorchRadius());
						if (InsideMap(southEast) && IsLight(southEast) && !IsWall(southEast) && !IsDoorShut(southEast) && blockedSouth && blockedEast)
							EmitLightFrom(southEast, RandomTorchRadius());
						if (InsideMap(southWest) && IsLight(southWest) && !IsWall(southWest) && !IsDoorShut(southWest) && blockedSouth && blockedWest)
							EmitLightFrom(southWest, RandomTorchRadius());
						if (InsideMap(northWest) && IsLight(northWest) && !IsWall(northWest) && !IsDoorShut(northWest) && blockedNorth && blockedWest)
							EmitLightFrom(northWest, RandomTorchRadius());
					}
				}
				else if (IsLight(index))
					EmitLightFrom(p, RandomTorchRadius());
			}
		}
	}
	const float _bright = 1f;
	const float _dim = .5f;
	const float _unlit = .333f;
	public override Color GetMapColor(int x, int y, bool screen = false)
	{
		var index = TileIndex(x, y);
		var lit = IsLight(index);
		var explored = IsExplored(index);
		var wall = IsWall(index);
		var stairs = IsStair(index);
		var door = IsDoor(index);
		var floor = IsFloor(index);
		var color = screen ? Color.magenta.SetAlpha(.5f) : Color.clear;
		if (explored || lit)
		{
			if (stairs)
				color = Colors.YellowLight.SetAlpha(_bright);
			else if (door)
				color = Colors.BlueLight.SetAlpha(_bright);
			else if (wall && !screen)
				color = Colors.GreyDark.SetAlpha(_dim);
			else if (floor && !screen)
				color = Colors.Grey.SetAlpha(lit ? _dim : _unlit);
		}
		return color;
	}
	public override void Turn()
	{
		Light(Manager.Instance.Character.transform.localPosition);
	}
}
