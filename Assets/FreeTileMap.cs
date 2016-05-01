using System.Collections.Generic;
using System.Linq;
using HenrySoftware.Rage;
public partial class FreeTileMap : TileMap
{
	public enum Tile
	{
		None = -1,
		Floor0, Floor1, Floor2, Floor3, Floor4, Floor5,
		FloorRoom0, FloorRoom1, FloorRoom2, FloorRoom3, FloorRoom4, FloorRoom5,
		StairsDown, StairsUp,
		DoorShut, DoorOpen, DoorBroke,
		WallTorch0, WallTorch1, WallTorch2, WallTorch3,
		Wall0, Wall1, Wall2, Wall3, Wall4, Wall5, Wall6, Wall7,
		Rug0, Rug1, Rug2, Rug3, Rug4, Rug5,
		Bed, Sacks,
		Chair0, Chair1, Chair2,
		Table, Stool, Globe, Bellows, Forge, Anvil, Workbench,
		BannerA0, BannerA1, BannerA2, BannerA3, BannerA4, BannerA5,
		BannerB0, BannerB1, BannerB2, BannerB3, BannerB4,
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
		var animations = new List<Animation>();
		animations.Add(new Animation() { Frames = new int[][] { _torch }, Fps = _torchSpeed } );
		animations.Add(new Animation() { Frames = _bannerA, Fps = _bannerSpeed } );
		animations.Add(new Animation() { Frames = _bannerB, Fps = _bannerSpeed } );
		return animations;
	}
	void Start()
	{
		Build(7, 7);
		Floor();
		Wall();
		Other();
	}
	void Floor()
	{
		for (var y = 0; y < Map.Height; y++)
			for (var x = 0; x < Map.Width; x++)
				SetTile(0, x, y, (int)Tile.Floor0 + Utility.Random.Next(6));
	}
	void Wall()
	{
		for (var y = 0; y < Map.Height; y++)
			for (var x = 0; x < Map.Width; x++)
				if (x == 0 || x == Map.Width - 1 || y == 0 || y == Map.Height - 1)
					SetTile(1, x, y, RandomWall());
	}
	int RandomWall()
	{
		return (int)Tile.WallTorch0 + Utility.Random.Next(12);
	}
	void Other()
	{
		SetTile(1, 2, 5, (int)Tile.BannerA0);
		SetTile(1, 3, 5, (int)Tile.Bed);
		SetTile(1, 4, 5, (int)Tile.BannerB0);
		SetTile(1, 2, 4, (int)Tile.Table);
		SetTile(1, 3, 4, (int)Tile.Rug2);
		SetTile(1, 4, 4, (int)Tile.Chair0);
		SetTile(1, 1, 2, (int)Tile.DoorOpen);
		var maxX = Map.Width - 2;
		for (var i = 2; i <= maxX; i++)
			SetTile(1, i, 2, RandomWall());
		SetTile(1, maxX, 1, (int)Tile.StairsDown);
	}
}
