using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ca.HenrySoftware.Rage;
public partial class TownTileMap : TileMap
{
	public enum Layer
	{
		Back,
		Blood,
		Fore,
		Flower,
		SplitBack,
		SplitBackWater,
		SplitBackFog,
		Tree,
		SplitFore,
		SplitForeWater,
		SplitForeFog,
		Top,
		Effect,
		Light,
		Edge,
	}
	public const int TilesAcross = 64;
	public enum Tile
	{
		None = -1,
		// row 0 (0-63)
		Floor0, //0
		Floor1, //1
		Floor2, //2
		Floor3, //3
		Floor4, //4
		Floor5, //5
		FloorRoom0, //6
		FloorRoom1, //7
		FloorRoom2, //8
		FloorRoom3, //9
		FloorRoom4, //10
		FloorRoom5, //11
		StairsDown, //12
		StairsUp, //13
		DoorShut, //14
		DoorOpen, //15
		DoorBroke, //16
		WallTorch0, //17
		WallTorch1, //18
		WallTorch2, //19
		WallTorch3, //20
		Wall0, //21
		Wall1, //22
		Wall2, //23
		Wall3, //24
		Wall4, //25
		Wall5, //26
		Wall6, //27
		Wall7, //28
			   // row 20 (1280 - 1333)
		TreeDead0Top = 20 * TilesAcross + 56,
		TreeDead1Top,
		TreeDead2Top,
		// row 21 (1334 - 1407)
		Blood0 = 21 * TilesAcross,
		Blood1,
		Blood2,
		Blood3,
		Blood4,
		Blood5,
		Blood6,
		Blood7,
		Blood8,
		Blood9,
		Blood10,
		Blood11,
		Blood12,
		Blood13,
		Blood14,
		Blood15,
		Blood16,
		Blood17,
		Blood18,
		Blood19,
		Blood20,
		Blood21,
		Blood22,
		Blood23,
		Blood24,
		Blood25,
		Blood26,
		Blood27,
		Blood28,
		Blood29,
		Blood30,
		Blood31,
		Blood32,
		Blood33,
		Blood34,
		Blood35,
		Blood36,
		Blood37,
		TreeDead0Bottom = 21 * TilesAcross + 56,
		TreeDead1Bottom,
		TreeDead2Bottom,
		TreeDead0Stump,
		TreeDead1Stump,
		TreeDead2Stump,
		// row 22 (1408 - 1471)
		BloodWall0 = 22 * TilesAcross,
		BloodWall1,
		BloodWall2,
		BloodWall3,
		BloodWall4,
		BloodWall5,
		BloodWall6,
		BloodWall7,
		BloodWall8,
		BloodWall9,
		BloodWall10,
		BloodWall11,
		BloodWall12,
		BloodWall13,
		BloodWall14,
		BloodWall15,
		BloodWall16,
		BloodWall17,
		BloodWall18,
		BloodWall19,
		BloodWall20,
		BloodWall21,
		BloodWall22,
		BloodWall23,
		BloodWall24,
		BloodWall25,
		TreeStone0Top = 22 * TilesAcross + 56,
		TreeStone1Top,
		TreeStone2Top,
		// row 23 (1472 - 1535)
		EdgeNE = 23 * TilesAcross,
		EdgeNW,
		EdgeSW,
		EdgeSE,
		Edge0,
		Edge1,
		Edge2,
		Edge3,
		Edge4,
		Edge5,
		Edge6,
		Edge7,
		Edge8,
		Edge9,
		Edge10,
		Edge11,
		Edge12,
		Edge13,
		Edge14,
		Edge15,
		Edge16,
		Edge17,
		Edge18,
		Edge19,
		Edge20,
		Edge21,
		Edge22,
		Edge23,
		Edge24,
		Edge25,
		Edge26,
		Edge27,
		WebBottom0,
		WebBottom1,
		WebBottom2,
		WebBottom3,
		WebBottom4,
		WebBottom5,
		WebBottom6,
		WebBottom7,
		WebBottom8,
		WebBottom9,
		WebBottom10,
		WebBottom11,
		WebTop0,
		WebTop1,
		WebTop2,
		WebTop3,
		WebTop4,
		WebTop5,
		WebTop6,
		WebTop7,
		WebTop8,
		WebTop9,
		WebTop10,
		WebTop11,
		TreeStone0Bottom = 23 * TilesAcross + 56,
		TreeStone1Bottom,
		TreeStone2Bottom,
		TreeStone0Stump,
		TreeStone1Stump,
		TreeStone2Stump,
		// row 24 (1536 - 1599)
		Wardrobe0remove = 24 * TilesAcross,
		Wardrobe1remove,
		Stockade,
		Chair0,
		Chair1,
		Chair2,
		Chair3,
		Table,
		Stool,
		Shelf0putinitems,
		Shelf1putinitems,
		Globe,
		Bellows,
		Forge,
		Anvil,
		Workbench,
		RoseBush = Workbench + 2,
		LampPostTop,
		PineTree0Top,
		PineTree1Top,
		PineTree2Top,
		SignPostWest,
		SignPostEast,
		WellTop,
		TreeStump,
		CoffinOpen0,
		CoffinOpen1,
		CoffinOpen2,
		CoffinOpen3,
		Window0,
		Window1,
		Window2,
		Window3,
		HouseSmoke0,
		HouseSmoke1,
		HouseSmoke2,
		HouseSmoke3,
		HouseSmoke4,
		HouseSmoke5,
		Cliff20 = HouseSmoke5 + 5,
		Cliff21,
		Cliff22,
		Cliff23,
		Cliff24,
		Cliff25,
		Cliff26,
		Cliff27,
		Cliff28,
		Cliff29,
		Cliff30,
		Cliff31,
		Cliff32,
		Cliff33,
		Cliff34,
		Cliff35,
		Cliff36,
		Cliff37,
		Cliff38,
		Cliff39,
		// row 25 (1600 - 1663)
		FootlockerOpenremove = 25 * TilesAcross,
		FootlockerShutremove,
		Bed,
		Dresserremove,
		Sacks,
		OpenSack1,
		OpenSack2,
		OpenSack3,
		OpenSack4,
		CandleStick00,
		CandleStick01,
		CandleStick10,
		CandleStick11,
		GraveStone,
		TreeCut = GraveStone + 4,
		LampPostBottom,
		PineTree0Bottom,
		PineTree1Bottom,
		PineTree2Bottom,
		SignPostNorth,
		SignPostSouth,
		WellBottom,
		MailBox,
		CoffinShut0,
		CoffinShut1,
		CoffinShut2,
		CoffinShut3,
		Stump0,
		Stump1,
		Stump2,
		EmptyWall,
		HouseGreen,
		HouseGreenBroke0,
		HouseGreenBroke1,
		HouseBlue,
		HouseBlueBroke0,
		HouseBlueBroke1,
		HouseRed,
		HouseRedBroke0,
		HouseRedBroke1,
		Cliff00 = HouseRedBroke1 + 2,
		Cliff01,
		Cliff02,
		Cliff03,
		Cliff04,
		Cliff05,
		Cliff06,
		Cliff07,
		Cliff08,
		Cliff09,
		Cliff10,
		Cliff11,
		Cliff12,
		Cliff13,
		Cliff14,
		Cliff15,
		Cliff16,
		Cliff17,
		Cliff18,
		Cliff19,
		// row 26 (1664 - 1727)
		Skull0 = 26 * TilesAcross,
		Skull1,
		Skull2,
		Skull3,
		Skull4,
		Skull5,
		Skull6,
		Skull7,
		Skull8,
		Skull9,
		TrapSpikes0,
		TrapSpikes1,
		TrapSaw0,
		TrapSaw1,
		TrapSaw2,
		TrapSaw3,
		TrapSaw4,
		TrapSaw5,
		TrapSaw6,
		TrapSaw7,
		TrapSaw8,
		TrapPitCoveredShut,
		TrapPitCoveredOpen,
		TrapPit,
		TrapCrumble00,
		TrapCrumble01,
		TrapCrumble02,
		TrapCrumble03,
		TrapCrumble10,
		TrapCrumble11,
		TrapCrumble12,
		TrapCrumble13,
		TrapCrumble20,
		TrapCrumble21,
		TrapCrumble22,
		TrapCrumble23,
		TrapCrumble30,
		TrapCrumble31,
		TrapCrumble32,
		TrapCrumble33,
		TrapCrumble40,
		TrapCrumble41,
		TrapCrumble42,
		TrapCrumble43,
		Cliff40,
		Cliff41,
		Cliff42,
		Cliff43,
		Cliff44,
		Cliff45,
		Cliff46,
		Cliff47,
		Cliff48,
		Cliff49,
		Cliff50,
		Cliff51,
		Cliff52,
		Cliff53,
		Cliff54,
		Cliff55,
		Cliff56,
		Cliff57,
		Cliff58,
		Cliff59,
		// row 27 (1728 - 1791)
		Rug0 = 27 * TilesAcross,
		Rug1,
		Rug2,
		Rug3,
		Rug4,
		Rug5,
		Teleporter00,
		Teleporter01,
		Teleporter02,
		Teleporter03,
		Teleporter0Damaged0,
		Teleporter0Damaged1,
		Teleporter0Damaged2,
		Teleporter0Damaged3,
		Teleporter0Broken,
		Teleporter10,
		Teleporter11,
		Teleporter12,
		Teleporter13,
		Teleporter1Damaged0,
		Teleporter1Damaged1,
		Teleporter1Damaged2,
		Teleporter1Damaged3,
		Teleporter1Broken,
		Teleporter20,
		Teleporter21,
		Teleporter22,
		Teleporter23,
		Teleporter2Damaged0,
		Teleporter2Damaged1,
		Teleporter2Damaged2,
		Teleporter2Damaged3,
		Teleporter2Broken,
		Flame0,
		Flame1,
		Flame2,
		Flame3,
		Flame4,
		Shop0,
		Shop1,
		Portal0,
		Portal1,
		Portal2,
		Portal3,
		Portal4,
		// row 28 (1792 - 1855)
		Magic00 = 28 * TilesAcross,
		Magic01,
		Magic02,
		Magic03,
		Magic04,
		Magic05,
		Magic06,
		Magic07,
		Magic08,
		Magic09,
		Magic0A,
		Magic0B,
		Magic0C,
		Magic0D,
		Magic10,
		Magic11,
		Magic12,
		Magic13,
		Magic14,
		Magic15,
		Magic20,
		Magic21,
		Magic22,
		Magic23,
		Magic24,
		Magic25,
		Magic26,
		Magic27,
		Magic28,
		Magic29,
		Magic2A,
		Magic2B,
		Magic2C,
		Magic2D,
		Magic2E,
		Magic2F,
		Magic2G,
		Magic2H,
		Magic30,
		Magic31,
		Magic32,
		Magic33,
		Magic34,
		Magic35,
		Magic36,
		Magic37,
		Magic38,
		Magic40,
		Magic41,
		Magic42,
		Magic43,
		Magic50,
		Magic51,
		Magic52,
		Magic53,
		Magic54,
		Magic55,
		Magic56,
		Magic60,
		Magic61,
		Magic62,
		Magic63,
		// row 29 (1856 - 1919)
		Explode0 = 29 * TilesAcross,
		Explode1,
		Explode2,
		Explode3,
		Explode4,
		Explode5,
		Explode6,
		Explode7,
		Explode8,
		Explode9,
		ExplodeA,
		ExplodeB,
		Explode10,
		Explode11,
		Explode12,
		Explode13,
		Explode14,
		Explode15,
		Explode16,
		Explode17,
		Explode18,
		Explode19,
		Explode1A,
		Explode1B,
		Explode20,
		Explode21,
		Explode22,
		Explode23,
		Explode24,
		Explode25,
		Explode26,
		Explode27,
		Explode28,
		Explode29,
		Explode2A,
		Explode2B,
		Explode30,
		Explode31,
		Explode32,
		Explode33,
		Explode34,
		Explode35,
		Explode36,
		Explode37,
		Explode38,
		Explode39,
		Explode3A,
		Explode3B,
		Explode40,
		Explode41,
		Explode42,
		Explode43,
		Explode44,
		Explode45,
		Explode46,
		Explode47,
		Explode48,
		Explode49,
		Explode4A,
		Explode4B,
		// row 30 (1920 - 1983)
		BannerA0 = 30 * TilesAcross,
		BannerA1,
		BannerA2,
		BannerA3,
		BannerA4,
		BannerA5,
		BannerB0,
		BannerB1,
		BannerB2,
		BannerB3,
		BannerB4,
		BannerC0,
		BannerC1,
		BannerC2,
		BannerC3,
		BannerC4,
		BannerC5,
		BannerD0,
		BannerD1,
		BannerD2,
		BannerD3,
		BannerD4,
		BannerE0,
		BannerE1,
		BannerE2,
		BannerE3,
		BannerE4,
		BannerE5,
		BannerF0,
		BannerF1,
		BannerF2,
		BannerF3,
		BannerF4,
		Brazier0,
		Brazier1,
		Brazier2,
		Brazier3,
		Range0,
		Range1,
		Range2,
		Range3,
		Range4,
		Range5,
		Range6,
		Range7,
		QuestQuestionMark,
		QuestExclamationMark,
		QuestQuestionMarkGreen,
		QuestExclamationMarkGreen,
		// row 31 (1984 - 2047)
		Fountain0 = 31 * TilesAcross,
		Fountain1,
		Fountain2,
		Fountain3,
		Fountain4,
		Fountain5,
		Corpse00,
		Corpse01,
		Corpse02,
		Corpse03,
		Corpse10,
		Corpse11,
		Corpse12,
		Corpse13,
		Corpse20,
		Corpse21,
		Corpse22,
		Corpse23,
		Corpse30,
		Corpse31,
		Corpse32,
		Corpse33,
		FireBubble0,
		FireBubble1,
		FireBubble2,
		CampFire0,
		CampFire1,
		CampFire2,
		CampFire3,
		Magic70,
		Magic71,
		Magic72,
		Rubble0,
		Rubble1,
		Rubble2,
		Rubble3,
		Rubble4,
		Rubble5,
		Scorch0,
		Scorch1,
		Scorch2,
		Scorch3,
		Scorch4,
		DesertGrassShortBack,
		DesertGrassShortFore,
		GrassShortBack = DesertGrassShortFore + 3,
		GrassShortFore,
		// row 32 (2048 - 2111)
		Grass0 = 32 * TilesAcross,
		Grass1,
		Grass2,
		Grass3,
		Grass4,
		Grass5,
		GrassRock0,
		GrassRock1,
		GrassRock2,
		GrassRock3,
		GrassRock4,
		GrassPillar0,
		GrassPillar1,
		GrassRoad0,
		GrassRoad1,
		GrassRoad2,
		GrassStairsDown,
		GrassStairsUp,
		GrassFlower0,
		GrassFlower1,
		GrassFlower2,
		GrassFlower3,
		GrassFlower4,
		GrassFlower5,
		GrassFlower6,
		GrassFlower7,
		Desert0,
		Desert1,
		Desert2,
		Desert3,
		Desert4,
		Desert5,
		Desert6,
		DesertSkull,
		DesertDoodad0,
		DesertDoodad1,
		DesertDoodad2,
		DesertDoodad3,
		DesertDoodad4,
		DesertDoodad5,
		DesertDoodad6,
		DesertDoodad7,
		DesertDoodad8,
		DesertGrassBack,
		DesertGrassFore,
		DesertStairsDown,
		DesertStairsUp,
		GrassBack,
		GrassFore,
		Hedge0,
		Hedge1,
		Hedge2,
		Hedge3,
		Ruins0,
		Ruins1,
		Ruins2,
		Ruins3,
		Ruins4,
		Ruins5,
		RoadDesert0,
		RoadDesert1,
		RoadDesert2,
		RoadDesert3,
		RoadDesert4,
		// row 33 (2112 - 2175)
		WaterShallowBack0 = 33 * TilesAcross,
		WaterShallowBack1,
		WaterShallowBack2,
		WaterShallowBack3,
		WaterShallowBack4,
		WaterShallowBack5,
		WaterShallowBack6,
		WaterDeepBack0,
		WaterDeepBack1,
		WaterDeepBack2,
		WaterDeepBack3,
		WaterDeepBack4,
		WaterDeepBack5,
		WaterDeepBack6,
		WaterShallowFront0,
		WaterShallowFront1,
		WaterShallowFront2,
		WaterShallowFront3,
		WaterShallowFront4,
		WaterShallowFront5,
		WaterShallowFront6,
		WaterDeepFront0,
		WaterDeepFront1,
		WaterDeepFront2,
		WaterDeepFront3,
		WaterDeepFront4,
		WaterDeepFront5,
		WaterDeepFront6,
		GreenWaterShallowBack0,
		GreenWaterShallowBack1,
		GreenWaterShallowBack2,
		GreenWaterShallowBack3,
		GreenWaterShallowBack4,
		GreenWaterShallowBack5,
		GreenWaterShallowBack6,
		GreenWaterDeepBack0,
		GreenWaterDeepBack1,
		GreenWaterDeepBack2,
		GreenWaterDeepBack3,
		GreenWaterDeepBack4,
		GreenWaterDeepBack5,
		GreenWaterDeepBack6,
		GreenWaterShallowFront0,
		GreenWaterShallowFront1,
		GreenWaterShallowFront2,
		GreenWaterShallowFront3,
		GreenWaterShallowFront4,
		GreenWaterShallowFront5,
		GreenWaterShallowFront6,
		GreenWaterDeepFront0,
		GreenWaterDeepFront1,
		GreenWaterDeepFront2,
		GreenWaterDeepFront3,
		GreenWaterDeepFront4,
		GreenWaterDeepFront5,
		GreenWaterDeepFront6,
		// row 34 (2176 - 2239)
		WhiteBlood0 = 34 * TilesAcross,
		WhiteBlood1,
		WhiteBlood2,
		WhiteBlood3,
		WhiteBlood4,
		WhiteBlood5,
		WhiteBlood6,
		WhiteBlood7,
		WhiteBlood8,
		WhiteBlood9,
		WhiteBlood10,
		WhiteBlood11,
		WhiteBlood12,
		WhiteBlood13,
		WhiteBlood14,
		WhiteBlood15,
		WhiteBlood16,
		WhiteBlood17,
		WhiteBlood18,
		WhiteBlood19,
		WhiteBlood20,
		WhiteBlood21,
		WhiteBlood22,
		WhiteBlood23,
		WhiteBlood24,
		WhiteBlood25,
		WhiteBlood26,
		WhiteBlood27,
		WhiteBlood28,
		WhiteBlood29,
		WhiteBlood30,
		WhiteBlood31,
		WhiteBlood32,
		WhiteBlood33,
		WhiteBlood34,
		WhiteBlood35,
		WhiteBlood36,
		WhiteBlood37,
		Grave0 = WhiteBlood37 + 10,
		Grave1,
		Grave2,
		Grave3,
		Grave4,
		Grave5,
		GravePath0,
		GravePath1,
		GravePath2,
		GravePath3,
		GravePath4,
		GraveGrass0,
		GraveGrass1,
		GraveGrass2,
		GraveGrass3,
		GraveGrass4,
		GraveGrass5,
		// row 35 (2240 - 2303)
		WhiteBloodWall0 = 35 * TilesAcross,
		WhiteBloodWall1,
		WhiteBloodWall2,
		WhiteBloodWall3,
		WhiteBloodWall4,
		WhiteBloodWall5,
		WhiteBloodWall6,
		WhiteBloodWall7,
		WhiteBloodWall8,
		WhiteBloodWall9,
		WhiteBloodWall10,
		WhiteBloodWall11,
		WhiteBloodWall12,
		WhiteBloodWall13,
		WhiteBloodWall14,
		WhiteBloodWall15,
		WhiteBloodWall16,
		WhiteBloodWall17,
		WhiteBloodWall18,
		WhiteBloodWall19,
		WhiteBloodWall20,
		WhiteBloodWall21,
		WhiteBloodWall22,
		WhiteBloodWall23,
		WhiteBloodWall24,
		WhiteBloodWall25,
		TownEdge00 = WhiteBloodWall25 + 12,
		TownEdge01,
		TownEdge02,
		TownEdge03,
		FlowerRed0 = 35 * TilesAcross + 47,
		FlowerRed1,
		FlowerRed2,
		FlowerRed3,
		FlowerRed4,
		FlowerRed5,
		FlowerRed6,
		FlowerRed7,
		FlowerWhite0,
		FlowerWhite1,
		FlowerWhite2,
		FlowerWhite3,
		FlowerWhite4,
		FlowerWhite5,
		FlowerWhite6,
		FlowerWhite7,
		// row 36 (2304 - 2367)
		ObeliskBlueTopX = 36 * TilesAcross,
		ObeliskBlueTop0,
		ObeliskBlueTop1,
		ObeliskBlueTop2,
		ObeliskBlueTop3,
		ObeliskBlueTop4,
		ObeliskBlueTop5,
		ObeliskBlueTop6,
		ObeliskBlueTop7,
		ObeliskBlueTop8,
		ObeliskBlueTop9,
		ObeliskBlueTopA,
		ObeliskBlueTopB,
		ObeliskBlueTopC,
		ObeliskBlueTopD,
		ObeliskBlueTopE,
		ObeliskBlueTopF,
		TownTavernSmoke00,
		TownTavernSmoke10,
		TownTavernSmoke20,
		TownTavernSmoke30,
		TownTavernSmoke40,
		TownTavernSmoke50,
		TownShopSmoke00,
		TownShopSmoke10,
		TownShopSmoke20,
		TownShopSmoke30,
		TownShopSmoke40,
		TownShopSmoke50,
		TownGrass0 = ObeliskBlueTopF + 17,
		TownGrass1,
		TownEdge10 = TownGrass1 + 3,
		TownEdge11,
		TownEdge12,
		TownEdge13,
		FlowerBlue0 = 36 * TilesAcross + 55,
		FlowerBlue1,
		FlowerBlue2,
		FlowerBlue3,
		FlowerBlue4,
		FlowerBlue5,
		FlowerBlue6,
		FlowerBlue7,
		// row 37 (2368 - 2431)
		ObeliskBlueBottomX = 37 * TilesAcross,
		ObeliskBlueBottom0,
		ObeliskBlueBottom1,
		ObeliskBlueBottom2,
		ObeliskBlueBottom3,
		ObeliskBlueBottom4,
		ObeliskBlueBottom5,
		ObeliskBlueBottom6,
		ObeliskBlueBottom7,
		ObeliskBlueBottom8,
		ObeliskBlueBottom9,
		ObeliskBlueBottomA,
		ObeliskBlueBottomB,
		ObeliskBlueBottomC,
		ObeliskBlueBottomD,
		ObeliskBlueBottomE,
		ObeliskBlueBottomF,
		TownTavernSmoke01,
		TownTavernSmoke11,
		TownTavernSmoke21,
		TownTavernSmoke31,
		TownTavernSmoke41,
		TownTavernSmoke51,
		TownShopSmoke01,
		TownShopSmoke11,
		TownShopSmoke21,
		TownShopSmoke31,
		TownShopSmoke41,
		TownShopSmoke51,
		TownGrass2 = ObeliskBlueBottomF + 17,
		TownGrass3,
		TownEdge20 = TownGrass3 + 3,
		TownEdge21,
		TownEdge22,
		TownEdge23,
		// row 38 (2432 - 2495)
		ObeliskPinkTopX = 38 * TilesAcross,
		ObeliskPinkTop0,
		ObeliskPinkTop1,
		ObeliskPinkTop2,
		ObeliskPinkTop3,
		ObeliskPinkTop4,
		ObeliskPinkTop5,
		ObeliskPinkTop6,
		ObeliskPinkTop7,
		ObeliskPinkTop8,
		ObeliskPinkTop9,
		ObeliskPinkTopA,
		ObeliskPinkTopB,
		ObeliskPinkTopC,
		ObeliskPinkTopD,
		ObeliskPinkTopE,
		ObeliskPinkTopF,
		TownShopSmoke02 = ObeliskPinkTopF + 7,
		TownShopSmoke12,
		TownShopSmoke22,
		TownShopSmoke32,
		TownShopSmoke42,
		TownShopSmoke52,
		TownTavern00 = ObeliskPinkTopF + 17,
		TownTavern01,
		TownTavern02,
		TownTavern03,
		TownEdge30,
		TownEdge31,
		TownEdge32,
		TownEdge33,
		// row 39 (2496 - 2559)
		ObeliskPinkBottomX = 39 * TilesAcross,
		ObeliskPinkBottom0,
		ObeliskPinkBottom1,
		ObeliskPinkBottom2,
		ObeliskPinkBottom3,
		ObeliskPinkBottom4,
		ObeliskPinkBottom5,
		ObeliskPinkBottom6,
		ObeliskPinkBottom7,
		ObeliskPinkBottom8,
		ObeliskPinkBottom9,
		ObeliskPinkBottomA,
		ObeliskPinkBottomB,
		ObeliskPinkBottomC,
		ObeliskPinkBottomD,
		ObeliskPinkBottomE,
		ObeliskPinkBottomF,
		TownHomeSmoke000,
		TownHomeSmoke010,
		TownHomeSmoke100,
		TownHomeSmoke110,
		TownHomeSmoke200,
		TownHomeSmoke210,
		TownHomeSmoke300,
		TownHomeSmoke310,
		TownHomeSmoke400,
		TownHomeSmoke410,
		TownHomeSmoke500,
		TownHomeSmoke510,
		TownTavern10 = ObeliskPinkBottomF + 17,
		TownTavern11,
		TownTavern12,
		TownTavern13,
		TownGrave0 = TownTavern13 + 2,
		TownGrave1,
		TownShop00 = TownGrave1 + 2,
		TownShop01,
		// row 40 (2560 - 2623)
		ObeliskBlackTopX = 40 * TilesAcross,
		ObeliskBlackTop0,
		ObeliskBlackTop1,
		ObeliskBlackTop2,
		ObeliskBlackTop3,
		ObeliskBlackTop4,
		ObeliskBlackTop5,
		ObeliskBlackTop6,
		ObeliskBlackTop7,
		ObeliskBlackTop8,
		ObeliskBlackTop9,
		ObeliskBlackTopA,
		ObeliskBlackTopB,
		ObeliskBlackTopC,
		ObeliskBlackTopD,
		ObeliskBlackTopE,
		ObeliskBlackTopF,
		TownHomeSmoke001,
		TownHomeSmoke011,
		TownHomeSmoke101,
		TownHomeSmoke111,
		TownHomeSmoke201,
		TownHomeSmoke211,
		TownHomeSmoke301,
		TownHomeSmoke311,
		TownHomeSmoke401,
		TownHomeSmoke411,
		TownHomeSmoke501,
		TownHomeSmoke511,
		TownTavern20 = ObeliskBlackTopF + 17,
		TownTavern21,
		TownTavern22,
		TownTavern23,
		TownGraveBack = TownTavern23 + 3,
		TownShop10,
		TownShop11,
		TownShop12,
		// row 41 (2624 - 2687)
		ObeliskBlackBottomX = 41 * TilesAcross,
		ObeliskBlackBottom0,
		ObeliskBlackBottom1,
		ObeliskBlackBottom2,
		ObeliskBlackBottom3,
		ObeliskBlackBottom4,
		ObeliskBlackBottom5,
		ObeliskBlackBottom6,
		ObeliskBlackBottom7,
		ObeliskBlackBottom8,
		ObeliskBlackBottom9,
		ObeliskBlackBottomA,
		ObeliskBlackBottomB,
		ObeliskBlackBottomC,
		ObeliskBlackBottomD,
		ObeliskBlackBottomE,
		ObeliskBlackBottomF,
		TownRockSmoke00,
		TownRockSmoke01,
		TownRockSmoke10,
		TownRockSmoke11,
		TownRockSmoke20,
		TownRockSmoke21,
		TownRockSmoke30,
		TownRockSmoke31,
		TownRockSmoke40,
		TownRockSmoke41,
		TownTavern30 = ObeliskBlackBottomF + 17,
		TownTavern31,
		TownTavern32,
		TownShop20 = TownTavern32 + 5,
		TownShop21,
		TownShop22,
		TownShop23,
		// row 42 (2688 - 2751)
		ObeliskGreenTopX = 42 * TilesAcross,
		ObeliskGreenTop0,
		ObeliskGreenTop1,
		ObeliskGreenTop2,
		ObeliskGreenTop3,
		ObeliskGreenTop4,
		ObeliskGreenTop5,
		ObeliskGreenTop6,
		ObeliskGreenTop7,
		ObeliskGreenTop8,
		ObeliskGreenTop9,
		ObeliskGreenTopA,
		ObeliskGreenTopB,
		ObeliskGreenTopC,
		ObeliskGreenTopD,
		ObeliskGreenTopE,
		ObeliskGreenTopF,
		TownOuthouseBird00,
		TownOuthouseBird10,
		TownOuthouseBird20,
		TownOuthouseBird30,
		TownOuthouseBird40,
		TownOuthouseBird50,
		TownOuthouseBird60,
		TownOuthouseBird70,
		TownOuthouseBird80,
		TownTavern40 = ObeliskGreenTopF + 17,
		TownTavern41,
		TownOuthouse00 = TownTavern41 + 5,
		TownShop30 = TownOuthouse00 + 2,
		// row 43 (2752 - 2815)
		ObeliskGreenBottomX = 43 * TilesAcross,
		ObeliskGreenBottom0,
		ObeliskGreenBottom1,
		ObeliskGreenBottom2,
		ObeliskGreenBottom3,
		ObeliskGreenBottom4,
		ObeliskGreenBottom5,
		ObeliskGreenBottom6,
		ObeliskGreenBottom7,
		ObeliskGreenBottom8,
		ObeliskGreenBottom9,
		ObeliskGreenBottomA,
		ObeliskGreenBottomB,
		ObeliskGreenBottomC,
		ObeliskGreenBottomD,
		ObeliskGreenBottomE,
		ObeliskGreenBottomF,
		TownOuthouseBird01,
		TownOuthouseBird11,
		TownOuthouseBird21,
		TownOuthouseBird31,
		TownOuthouseBird41,
		TownOuthouseBird51,
		TownOuthouseBird61,
		TownOuthouseBird71,
		TownOuthouseBird81,
		TownOuthouse10 = ObeliskGreenBottomF + 23,
		TownShop40 = TownOuthouse10 + 2,
		// row 44 (2816 - 2879)
		FogHeavyPurpleBottom0 = 44 * TilesAcross,
		FogHeavyPurpleBottom1,
		FogHeavyPurpleBottom2,
		FogHeavyPurpleBottom3,
		FogHeavyPurpleBottom4,
		FogHeavyPurpleBottom5,
		FogHeavyPurpleBottom6,
		FogHeavyPurpleBottom7,
		FogHeavyPurpleTop0,
		FogHeavyPurpleTop1,
		FogHeavyPurpleTop2,
		FogHeavyPurpleTop3,
		FogHeavyPurpleTop4,
		FogHeavyPurpleTop5,
		FogHeavyPurpleTop6,
		FogHeavyPurpleTop7,
		Lava00,
		Lava01,
		Lava02,
		Lava03,
		Lava04,
		Lava05,
		Lava06,
		Lava07,
		Lava08,
		Lava09,
		Lava0A,
		Lava0B,
		Lava0C,
		Lava0D,
		Lava0E,
		Lava0F,
		TownHome00 = Lava0F + 4,
		TownHome01,
		TownHome02,
		TownOuthouse20,
		TownOuthouse21,
		// row 45 (2880 - 2943)
		FogHeavyRedBottom0 = 45 * TilesAcross,
		FogHeavyRedBottom1,
		FogHeavyRedBottom2,
		FogHeavyRedBottom3,
		FogHeavyRedBottom4,
		FogHeavyRedBottom5,
		FogHeavyRedBottom6,
		FogHeavyRedBottom7,
		FogHeavyRedTop0,
		FogHeavyRedTop1,
		FogHeavyRedTop2,
		FogHeavyRedTop3,
		FogHeavyRedTop4,
		FogHeavyRedTop5,
		FogHeavyRedTop6,
		FogHeavyRedTop7,
		Lava10,
		Lava11,
		Lava12,
		Lava13,
		Lava14,
		Lava15,
		Lava16,
		Lava17,
		Lava18,
		Lava19,
		Lava1A,
		Lava1B,
		Lava1C,
		Lava1D,
		Lava1E,
		Lava1F,
		TownHome10 = Lava1F + 3,
		TownHome11,
		TownHome12,
		TownHome13,
		TownHome14,
		TownOuthouseBack,
		// row 46 (2944 - 3007)
		FogHeavyBlueBottom0 = 46 * TilesAcross,
		FogHeavyBlueBottom1,
		FogHeavyBlueBottom2,
		FogHeavyBlueBottom3,
		FogHeavyBlueBottom4,
		FogHeavyBlueBottom5,
		FogHeavyBlueBottom6,
		FogHeavyBlueBottom7,
		FogHeavyBlueTop0,
		FogHeavyBlueTop1,
		FogHeavyBlueTop2,
		FogHeavyBlueTop3,
		FogHeavyBlueTop4,
		FogHeavyBlueTop5,
		FogHeavyBlueTop6,
		FogHeavyBlueTop7,
		Lava20,
		Lava21,
		Lava22,
		Lava23,
		Lava24,
		Lava25,
		Lava26,
		Lava27,
		Lava28,
		Lava29,
		Lava2A,
		Lava2B,
		Lava2C,
		Lava2D,
		Lava2E,
		Lava2F,
		TownHome20 = Lava2F + 3,
		TownHome21,
		TownHome22,
		TownHome23,
		TownHome24,
		// row 47 (3008 - 3071)
		FogHeavyGreenBottom0 = 47 * TilesAcross,
		FogHeavyGreenBottom1,
		FogHeavyGreenBottom2,
		FogHeavyGreenBottom3,
		FogHeavyGreenBottom4,
		FogHeavyGreenBottom5,
		FogHeavyGreenBottom6,
		FogHeavyGreenBottom7,
		FogHeavyGreenTop0,
		FogHeavyGreenTop1,
		FogHeavyGreenTop2,
		FogHeavyGreenTop3,
		FogHeavyGreenTop4,
		FogHeavyGreenTop5,
		FogHeavyGreenTop6,
		FogHeavyGreenTop7,
		Lava30,
		Lava31,
		Lava32,
		Lava33,
		Lava34,
		Lava35,
		Lava36,
		Lava37,
		Lava38,
		Lava39,
		Lava3A,
		Lava3B,
		Lava3C,
		Lava3D,
		Lava3E,
		Lava3F,
		TownHome30 = Lava3F + 5,
		TownHome31,
		TownSign = TownHome31 + 5,
		// row 48 (3072 - 3135)
		FogHeavyBlackBottom0 = 48 * TilesAcross,
		FogHeavyBlackBottom1,
		FogHeavyBlackBottom2,
		FogHeavyBlackBottom3,
		FogHeavyBlackBottom4,
		FogHeavyBlackBottom5,
		FogHeavyBlackBottom6,
		FogHeavyBlackBottom7,
		FogHeavyBlackTop0,
		FogHeavyBlackTop1,
		FogHeavyBlackTop2,
		FogHeavyBlackTop3,
		FogHeavyBlackTop4,
		FogHeavyBlackTop5,
		FogHeavyBlackTop6,
		FogHeavyBlackTop7,
		Lava40,
		Lava41,
		Lava42,
		Lava43,
		Lava44,
		Lava45,
		Lava46,
		Lava47,
		Lava48,
		Lava49,
		Lava4A,
		Lava4B,
		Lava4C,
		Lava4D,
		Lava4E,
		Lava4F,
		TownHome40 = Lava4F + 5,
		TownHome41,
		TownHome42,
		TownStairsDown = TownHome42 + 5,
		// row 49 (3136 - 3199)
		FogLightPurpleBottom0 = 49 * TilesAcross,
		FogLightPurpleBottom1,
		FogLightPurpleBottom2,
		FogLightPurpleBottom3,
		FogLightPurpleBottom4,
		FogLightPurpleBottom5,
		FogLightPurpleBottom6,
		FogLightPurpleBottom7,
		FogLightPurpleTop0,
		FogLightPurpleTop1,
		FogLightPurpleTop2,
		FogLightPurpleTop3,
		FogLightPurpleTop4,
		FogLightPurpleTop5,
		FogLightPurpleTop6,
		FogLightPurpleTop7,
		Lava50,
		Lava51,
		Lava52,
		Lava53,
		Lava54,
		Lava55,
		Lava56,
		Lava57,
		Lava58,
		Lava59,
		Lava5A,
		Lava5B,
		Lava5C,
		Lava5D,
		Lava5E,
		Lava5F,
		TownGrid00,
		TownGrid01,
		TownGrid02,
		TownGrid03,
		TownGrid04,
		TownGrid05,
		TownGrid06,
		TownGrid07,
		TownGrid08,
		TownGrid09,
		TownGrid0A,
		TownGrid0B,
		TownGrid0C,
		// row 50 (3200 - 3263)
		FogLightRedBottom0 = 50 * TilesAcross,
		FogLightRedBottom1,
		FogLightRedBottom2,
		FogLightRedBottom3,
		FogLightRedBottom4,
		FogLightRedBottom5,
		FogLightRedBottom6,
		FogLightRedBottom7,
		FogLightRedTop0,
		FogLightRedTop1,
		FogLightRedTop2,
		FogLightRedTop3,
		FogLightRedTop4,
		FogLightRedTop5,
		FogLightRedTop6,
		FogLightRedTop7,
		Lava60,
		Lava61,
		Lava62,
		Lava63,
		Lava64,
		Lava65,
		Lava66,
		Lava67,
		Lava68,
		Lava69,
		Lava6A,
		Lava6B,
		Lava6C,
		Lava6D,
		Lava6E,
		Lava6F,
		TownGrid10,
		TownGrid11,
		TownGrid12,
		TownGrid13,
		TownGrid14,
		TownGrid15,
		TownGrid16,
		TownGrid17,
		TownGrid18,
		TownGrid19,
		TownGrid1A,
		TownGrid1B,
		TownGrid1C,
		// row 51 (3264 - 3327)
		FogLightBlueBottom0 = 51 * TilesAcross,
		FogLightBlueBottom1,
		FogLightBlueBottom2,
		FogLightBlueBottom3,
		FogLightBlueBottom4,
		FogLightBlueBottom5,
		FogLightBlueBottom6,
		FogLightBlueBottom7,
		FogLightBlueTop0,
		FogLightBlueTop1,
		FogLightBlueTop2,
		FogLightBlueTop3,
		FogLightBlueTop4,
		FogLightBlueTop5,
		FogLightBlueTop6,
		FogLightBlueTop7,
		Lava70,
		Lava71,
		Lava72,
		Lava73,
		Lava74,
		Lava75,
		Lava76,
		Lava77,
		Lava78,
		Lava79,
		Lava7A,
		Lava7B,
		Lava7C,
		Lava7D,
		Lava7E,
		Lava7F,
		TownGrid20,
		TownGrid21,
		TownGrid22,
		TownGrid23,
		TownGrid24,
		TownGrid25,
		TownGrid26,
		TownGrid27,
		TownGrid28,
		TownGrid29,
		TownGrid2A,
		TownGrid2B,
		TownGrid2C,
		// row 52 (3328 - 3391)
		FogLightGreenBottom0 = 52 * TilesAcross,
		FogLightGreenBottom1,
		FogLightGreenBottom2,
		FogLightGreenBottom3,
		FogLightGreenBottom4,
		FogLightGreenBottom5,
		FogLightGreenBottom6,
		FogLightGreenBottom7,
		FogLightGreenTop0,
		FogLightGreenTop1,
		FogLightGreenTop2,
		FogLightGreenTop3,
		FogLightGreenTop4,
		FogLightGreenTop5,
		FogLightGreenTop6,
		FogLightGreenTop7,
		Lava80,
		Lava81,
		Lava82,
		Lava83,
		Lava84,
		Lava85,
		Lava86,
		Lava87,
		Lava88,
		Lava89,
		Lava8A,
		Lava8B,
		Lava8C,
		Lava8D,
		Lava8E,
		Lava8F,
		TownGrid30,
		TownGrid31,
		TownGrid32,
		TownGrid33,
		TownGrid34,
		TownGrid35,
		TownGrid36,
		TownGrid37,
		TownGrid38,
		TownGrid39,
		TownGrid3A,
		TownGrid3B,
		TownGrid3C,
		// row 53 (3392 - 3455)
		FogLightBlackBottom0 = 53 * TilesAcross,
		FogLightBlackBottom1,
		FogLightBlackBottom2,
		FogLightBlackBottom3,
		FogLightBlackBottom4,
		FogLightBlackBottom5,
		FogLightBlackBottom6,
		FogLightBlackBottom7,
		FogLightBlackTop0,
		FogLightBlackTop1,
		FogLightBlackTop2,
		FogLightBlackTop3,
		FogLightBlackTop4,
		FogLightBlackTop5,
		FogLightBlackTop6,
		FogLightBlackTop7,
		Lava90,
		Lava91,
		Lava92,
		Lava93,
		Lava94,
		Lava95,
		Lava96,
		Lava97,
		Lava98,
		Lava99,
		Lava9A,
		Lava9B,
		Lava9C,
		Lava9D,
		Lava9E,
		Lava9F,
		TownGrid40,
		TownGrid41,
		TownGrid42,
		TownGrid43,
		TownGrid44,
		TownGrid45,
		TownGrid46,
		TownGrid47,
		TownGrid48,
		TownGrid49,
		TownGrid4A,
		TownGrid4B,
		TownGrid4C,
		// row 54 (3456 - 3519)
		LavaA0 = 54 * TilesAcross + 16,
		LavaA1,
		LavaA2,
		LavaA3,
		LavaA4,
		LavaA5,
		LavaA6,
		LavaA7,
		LavaA8,
		LavaA9,
		LavaAA,
		LavaAB,
		LavaAC,
		LavaAD,
		LavaAE,
		LavaAF,
		TownGrid50,
		TownGrid51,
		TownGrid52,
		TownGrid53,
		TownGrid54,
		TownGrid55,
		TownGrid56,
		TownGrid57,
		TownGrid58,
		TownGrid59,
		TownGrid5A,
		NightDesertGrassShortBack,
		NightDesertGrassShortFore,
		NightGrassShortBack = NightDesertGrassShortFore + 3,
		NightGrassShortFore,
		TrapBear0,
		TrapBear1,
		// row 55 (3520 - 3583)
		NightGrass0 = 55 * TilesAcross,
		NightGrass1,
		NightGrass2,
		NightGrass3,
		NightGrass4,
		NightGrass5,
		NightGrassRock0,
		NightGrassRock1,
		NightGrassRock2,
		NightGrassRock3,
		NightGrassRock4,
		NightGrassPillar0,
		NightGrassPillar1,
		NightGrassRoad0,
		NightGrassRoad1,
		NightGrassRoad2,
		NightGrassStairsDown,
		NightGrassStairsUp,
		NightDesert0 = NightGrassStairsUp + 9,
		NightDesert1,
		NightDesert2,
		NightDesert3,
		NightDesert4,
		NightDesert5,
		NightDesert6,
		NightDesertSkull,
		NightDesertDoodad0,
		NightDesertDoodad1,
		NightDesertDoodad2,
		NightDesertDoodad3,
		NightDesertDoodad4,
		NightDesertDoodad5,
		NightDesertDoodad6,
		NightDesertDoodad7,
		NightDesertDoodad8,
		NightDesertGrassBack,
		NightDesertGrassFore,
		NightDesertStairsDown,
		NightDesertStairsUp,
		NightGrassBack,
		NightGrassFore,
		NightHedge0,
		NightHedge1,
		NightHedge2,
		NightHedge3,
		NightRuins0,
		NightRuins1,
		NightRuins2,
		NightRuins3,
		NightRuins4,
		NightRuins5,
		NightRoadDesert0,
		NightRoadDesert1,
		NightRoadDesert2,
		NightRoadDesert3,
		NightRoadDesert4,
		// row 56 (3584 - 3647)
		LightA0 = 56 * TilesAcross,
		LightA1,
		LightA2,
		LightA3,
		LightA4,
		LightA5,
		LightA6,
		LightA7,
		LightA8,
		LightA9,
		LightA10,
		LightA11,
		LightA12,
		LightA13,
		LightA14,
		LightA15,
		LightA16,
		LightA17,
		LightA18,
		LightA19,
		LightA20,
		LightA21,
		LightA22,
		LightA23,
		LightA24,
		LightA25,
		LightA26,
		LightA27,
		LightA28,
		LightA29,
		LightA30,
		LightA31,
		// row 57 (3648 - 3711)
		LightB0 = 57 * TilesAcross,
		LightB1,
		LightB2,
		LightB3,
		LightB4,
		LightB5,
		LightB6,
		LightB7,
		LightB8,
		LightB9,
		LightB10,
		LightB11,
		LightB12,
		LightB13,
		LightB14,
		LightB15,
		LightB16,
		LightB17,
		LightB18,
		LightB19,
		LightB20,
		LightB21,
		LightB22,
		LightB23,
		LightB24,
		LightB25,
		LightB26,
		LightB27,
		LightB28,
		LightB29,
		LightB30,
		LightB31,
		// row 58 (3712 - 3775)
		Light0 = 58 * TilesAcross,
		Light1,
		Light2,
		Light3,
		Light4,
		Light5,
		Light6,
		Light7,
		Light8,
		Light9,
		Light10,
		Light11,
		Light12,
		Light13,
		Light14,
		Light15,
		Light16,
		Light17,
		Light18,
		Light19,
		Light20,
		Light21,
		Light22,
		Light23,
		Light24,
		Light25,
		Light26,
		Light27,
		Light28,
		Light29,
		Light30,
		Light31,
		EmptyLight,
		Red,
		Green,
		Blue,
		Yellow,
		Magenta,
		Cyan,
		Black,
		Empty,
		// row 59 (3776 - 3839)
		InsideEdge0 = 59 * TilesAcross,
		InsideEdge1,
		InsideEdge2,
		InsideEdge3,
		InsideEdge4,
		InsideEdge5,
		InsideEdge6,
		InsideEdge7,
		InsideEdge8,
		InsideEdge9,
		InsideEdge10,
		InsideEdge11,
		InsideEdge12,
		InsideEdge13,
		InsideEdge14,
		InsideEdge15,
		InsideEdge16,
		InsideEdge17,
		InsideEdge18,
		InsideEdge19,
		InsideEdge20,
		InsideEdge21,
		InsideEdge22,
		InsideEdge23,
		InsideEdge24,
		InsideEdge25,
		InsideEdge26,
		InsideEdge27,
		InsideEdge28,
		InsideEdge29,
		InsideEdge30,
		InsideEdge31,
		InsideEdge32,
		InsideEdge33,
		InsideEdge34,
		InsideEdge35,
		// row 60 (3840 - 3903)
		InsideEdgeCorner0 = 60 * TilesAcross,
		InsideEdgeCorner1,
		InsideEdgeCorner2,
		InsideEdgeCorner3,
		InsideEdgeCorner4,
		InsideEdgeCorner5,
		InsideEdgeCorner6,
		InsideEdgeCorner7,
		InsideEdgeCorner8,
		InsideEdgeCorner9,
		InsideEdgeCorner10,
		InsideEdgeCorner11,
		InsideEdgeCorner12,
		InsideEdgeCorner13,
		InsideEdgeCorner14,
		InsideEdgeCorner15,
		InsideEdgeCorner16,
		InsideEdgeCorner17,
		InsideEdgeCorner18,
		InsideEdgeCorner19,
		InsideEdgeCorner20,
		InsideEdgeCorner21,
	};
	public enum Item
	{
		None = -1,
		// row 0
		FoodCookie,
		FoodChocolate,
		FoodSteelMug,
		FoodWine,
		FoodAlchohol,
		FoodPie,
		FoodSushi0,
		FoodSushi1,
		FoodSaki,
		FoodBoar,
		FoodMarmalade,
		FoodJam,
		FoodAppleWorm0,
		FoodAppleWorm1,
		FoodTurnip,
		FoodPotato0, //???
		FoodEggs,
		FoodHoneycomb,
		FoodPineapple,
		FoodBacon,
		FoodGlassBeerMug,
		FoodSteak,
		FoodWineAndGlass,
		FoodFish,
		FoodCheese,
		FoodChicken,
		FoodBread,
		FoodEggplant,
		FoodPepperRed,
		FoodPepperGreen,
		FoodMilkBottleEmpty,
		FoodMilkBottleFull,
		FoodApple,
		FoodStrawberry,
		FoodCherry0, //???
		FoodLemon,
		FoodPiePumpkin,
		FoodPieLemon,
		FoodPieApple,
		FoodPickle,
		FoodPretzel,
		FoodPepperoni,
		FoodFillet,
		FoodOrange, //???
		FoodChip, //???
		FoodPotato1, //???
		FoodMelon0,
		FoodMelon1,
		FoodMelon2,
		FoodWaffles,
		FoodChickenLeg,
		FoodCherry1,
		FoodRibs,
		FoodSardines,
		FoodPassionFruit,
		FoodSausages,
		FoodAvacado,
		FoodSalmon,
		FoodHotSauce,
		FoodOlive,
		FoodPickledEggs,
		FoodFig,
		FoodOnion,
		FoodShrimp,
		// row 1
		PotionSmallEmpty = TilesAcross,
		PotionSmallRed,
		PotionSmallOrange,
		PotionSmallBlue,
		PotionSmallPurple,
		PotionSmallPink,
		PotionSmallGreen,
		PotionSmallBrown,
		PotionSmallBlack,
		PotionSmallWhite,
		PotionSmallYellow,
		PotionSmallMagenta,
		PotionSmallCyan,
		PotionSmallMulticolored,
		PotionMediumEmpty,
		PotionMediumRed,
		PotionMediumOrange,
		PotionMediumBlue,
		PotionMediumPurple,
		PotionMediumPink,
		PotionMediumGreen,
		PotionMediumBrown,
		PotionMediumBlack,
		PotionMediumWhite,
		PotionMediumYellow,
		PotionMediumMagenta,
		PotionMediumCyan,
		PotionMediumMulticolored,
		PotionLargeEmpty,
		PotionLargeRed,
		PotionLargeOrange,
		PotionLargeBlue,
		PotionLargePurple,
		PotionLargePink,
		PotionLargeGreen,
		PotionLargeBrown,
		PotionLargeBlack,
		PotionLargeWhite,
		PotionLargeYellow,
		PotionLargeMagenta,
		PotionLargeCyan,
		PotionLargeMulticolored,
		FlaskEmpty,
		FlaskBrown,
		FlaskMagenta,
		FlaskBlue,
		FlaskGreen,
		FlaskBlack,
		FlaskWhite,
		FlaskPurple,
		FlaskCyan,
		FlaskOrange,
		Flask,
		// row 2
		Chest0Shut = 2 * TilesAcross,
		Chest1Shut,
		Chest2Shut,
		Chest3Shut,
		Chest4Shut,
		Chest5Shut,
		Chest6Shut,
		Chest7Shut,
		Chest8Shut,
		Chest9Shut,
		ChestAShut,
		ChestBShut,
		Chest0OpenFull,
		Chest1OpenFull,
		Chest2OpenFull,
		Chest3OpenFull,
		Chest4OpenFull,
		Chest5OpenFull,
		Chest6OpenFull,
		Chest7OpenFull,
		Chest8OpenFull,
		Chest9OpenFull,
		ChestAOpenFull,
		ChestBOpenFull,
		Chest0OpenEmpty,
		Chest1OpenEmpty,
		Chest2OpenEmpty,
		Chest3OpenEmpty,
		Chest4OpenEmpty,
		Chest5OpenEmpty,
		Chest6OpenEmpty,
		Chest7OpenEmpty,
		Chest8OpenEmpty,
		Chest9OpenEmpty,
		ChestAOpenEmpty,
		ChestBOpenEmpty,
		Chest0Broke,
		Chest1Broke,
		Chest2Broke,
		Chest3Broke,
		Chest4Broke,
		Chest5Broke,
		Chest6Broke,
		Chest7Broke,
		Chest8Broke,
		Chest9Broke,
		ChestABroke,
		ChestBBroke,
		Loot,
		BerryBushFull,
		BerryBushEmpty,
		BerryBushBroke,
		// row 3
		Mushroom0_0 = 3 * TilesAcross,
		Mushroom0_1,
		Mushroom0_2,
		Mushroom0_3,
		Mushroom0_4,
		Mushroom0_5,
		Mushroom0_6,
		Mushroom0_7,
		Mushroom1_0,
		Mushroom1_1,
		Mushroom1_2,
		Mushroom1_3,
		Mushroom1_4,
		Mushroom1_5,
		Mushroom1_6,
		Mushroom1_7,
		Mushroom2_0,
		Mushroom2_1,
		Mushroom2_2,
		Mushroom2_3,
		Mushroom2_4,
		Mushroom2_5,
		Mushroom2_6,
		Mushroom2_7,
		Mushroom2_8,
		Mushroom2_9,
		Mushroom2_10,
		Mushroom3_0,
		Mushroom3_1,
		Mushroom3_2,
		Mushroom3_3,
		Mushroom3_4,
		Mushroom3_5,
		Mushroom3_6,
		Mushroom3_7,
		Shell0,
		Shell1,
		Shell2,
		Shell3,
		Shell4,
		Shell5,
		Shell6,
		Starfish0,
		Starfish1,
		Starfish2,
		Starfish3,
		Starfish4,
		Starfish5,
		Starfish6,
		Starfish7,
		Starfish8,
		Starfish9,
		Starfish10,
		Starfish11,
		Starfish12,
		Starfish13,
		CrystalBall0,
		CrystalBall1,
		CrystalBall2,
		// row 4
		Ring00 = 4 * TilesAcross,
		Ring01,
		Ring02,
		Ring03,
		Ring04,
		Ring05,
		Ring06,
		Ring07,
		Ring08,
		Ring09,
		Ring20,
		Ring21,
		Ring22,
		Ring23,
		Ring24,
		Ring25,
		Ring26,
		Ring27,
		Ring28,
		Ring29,
		Ring30,
		Ring31,
		Ring32,
		Ring33,
		Ring34,
		Ring35,
		Ring36,
		Ring37,
		Ring38,
		Ring39,
		Ring40,
		Ring41,
		Ring42,
		Ring43,
		Ring44,
		Ring45,
		Ring46,
		Ring47,
		Ring48,
		Ring49,
		Ring50,
		Ring51,
		Ring52,
		Ring53,
		Ring54,
		Ring55,
		Ring56,
		Ring57,
		Ring58,
		Ring59,
		Ring510,
		Ring511,
		NewEgg0,
		NewEgg1,
		NewEgg2,
		NewEgg3,
		NewEgg4,
		NewEgg5,
		NewEgg6,
		NewEgg7,
		NewEgg8,
		NewEgg9,
		NewEgg10,
		NewEgg11,
		// row 5
		Necklace00 = 5 * TilesAcross,
		Necklace01,
		Necklace02,
		Necklace03,
		Necklace04,
		Necklace20,
		Necklace21,
		Necklace22,
		Necklace23,
		Necklace24,
		Necklace30,
		Necklace31,
		Necklace32,
		Necklace33,
		Necklace34,
		Necklace4_0,
		Necklace4_1,
		Necklace4_2,
		Necklace4_3,
		Necklace4_4,
		Necklace4_5,
		Necklace4_6,
		Necklace4_7,
		Necklace4_8,
		Necklace4_9,
		Necklace4_10,
		Necklace4_11,
		Necklace4_12,
		Necklace4_13,
		Necklace4_14,
		Necklace4_15,
		Necklace4_16,
		Necklace4_17,
		Necklace4_18,
		Necklace4_19,
		Necklace4_20,
		Necklace4_21,
		Necklace4_22,
		Necklace4_23,
		Necklace4_24,
		Necklace4_25,
		Necklace4_26,
		Necklace4_27,
		Compass,
		EmptyItem,
		// row 6
		Book00 = 6 * TilesAcross,
		Book01,
		Book02,
		Book10,
		Book11,
		Book12,
		Book20,
		Book21,
		Book22,
		Book30,
		Book31,
		Book32,
		Book40,
		Book41,
		Book42,
		Book50,
		Book51,
		Book60,
		Book61,
		Coffin0,
		Coffin1,
		HourGlass,
		Harp,
		Lute,
		Horn0,
		Drum,
		Flute,
		PenAndInkwell,
		Horn1,
		RabbitFoot,
		Telescope,
		Mirror,
		Hide0,
		Hide1,
		Hide2,
		Hide3,
		Hide4,
		Hide5,
		Hide6,
		Hide7,
		Hide8,
		Hide9,
		Potion0,
		Potion1,
		Potion2,
		Urn0,
		Urn1,
		Urn6 = Urn1 + 4,
		Urn7,
		Urn8,
		Urn9,
		// row 7
		LetterShut = 7 * TilesAcross,
		LetterOpen,
		Scroll,
		ScrollIdentify,
		ScrollAnvil,
		Scroll0,
		Scroll1,
		Scroll2,
		Scroll3,
		Scroll4,
		Scroll5,
		Scroll6,
		Scroll7,
		ScrollLight,
		ScrollMedium,
		ScrollDark,
		Lard,
		Date,
		CandyCorn,
		Hooch,
		ShishKaBob,
		Bun,
		MonkeyBrain,
		Onion,
		Berry0,
		Berry1,
		Berry2,
		Berry3,
		Berry4,
		Berry5,
		Berry6,
		Berry7,
		Berry8,
		Roe0,
		Roe1,
		Roe2,
		Branch,
		BrokenPottery,
		RustyHammer,
		Boulder,
		Bone0,
		Bone1,
		Board0,
		Board1,
		Manacle0,
		Manacle1,
		// row 8
		GoldCoin0 = 8 * TilesAcross,
		GoldCoin1,
		GoldCoin2,
		GoldCoin3,
		GoldCoin4,
		GoldCoin5,
		GoldNugget,
		GoldTooth,
		GoldBar0,
		GoldBar1,
		GoldBar2,
		RareGoldCoin0,
		RareGoldCoin1,
		RareGoldCoin2,
		RareGoldCoin3,
		SilverCoin0,
		SilverCoin1,
		SilverCoin2,
		SilverCoin3,
		SilverCoin4,
		SilverCoin5,
		SilverNugget,
		RareSilverCoin0,
		RareSilverCoin1,
		RareSilverCoin2,
		RareSilverCoin3,
		SilverBar0,
		SilverBar1,
		SilverBar2,
		CopperCoin0,
		CopperCoin1,
		CopperCoin2,
		CopperCoin3,
		CopperCoin4,
		CopperCoin5,
		Bag0,
		Bag1,
		Bag2,
		ClamPearl = Bag2 + 10,
		ClamNoPearl,
		ClamClosed,
		Pearl,
		Pearls,
		Gem0,
		Gem1,
		Gem2,
		Gem3,
		Gem4,
		MetalBar0 = Gem4 + 3,
		MetalBar1,
		MetalBar2,
		MetalBar3,
		MetalBar4,
		// row 9
		Herb0 = 9 * TilesAcross,
		Herb1,
		HerbJar,
		Herb2,
		Herb3,
		Herb4,
		Herb5,
		Herb6,
		Herb7,
		Herb8,
		Herb9,
		Bomb = Herb9 + 20,
		SwordInStone,
		MortarAndPestle = SwordInStone + 2,
		GiftBox,
		AnotherUrn0,
		AnotherUrn1,
		AnotherUrn2,
		Mallet = AnotherUrn2 + 2,
		Backpack = Mallet + 3,
		Wineskin,
		Pipe,
		Shot = Pipe + 2,
		Powder,
		CrownSilver0,
		CrownSilver1,
		CrownSilver2,
		CrownSilver3,
		CrownBronze0,
		CrownBronze1,
		CrownBronze2,
		CrownBronze3,
		CrownGold0,
		CrownGold1,
		CrownGold2,
		CrownGold3,
		CrownFancy0,
		CrownFancy1,
		// row 10
		Key0 = 10 * TilesAcross,
		Key1,
		Key2,
		ScrollCase0 = Key2 + 37,
		ScrollCase1,
		ScrollCase2,
		// row 11
		Candle = 11 * TilesAcross,
		Torch,
		Lantern0,
		Lantern1,
		Lantern2,
		Lantern3,
		Lantern4,
		Lamp0,
		Lamp1,
		Lamp2,
		Lamp3,
		Lamp4,
		Phial0,
		Phial1,
		Phial2,
		CrateShut,
		CrateOpenFull,
		CrateOpenEmpty,
		CrateBroke,
		BarrelShut,
		BarrelOpenFull,
		BarrelOpenEmpty,
		BarrelBroke,
		ShelfFull,
		ShelfEmpty,
		ShelfBroke,
		StrongBoxShut,
		StrongBoxOpenFull,
		StrongBoxOpenEmpty,
		StrongBoxBroke,
		WardrobeShut,
		WardrobeOpenFull,
		WardrobeOpenEmpty,
		WardrobeBroke,
		DresserShut,
		DresserOpenFull,
		DresserOpenEmpty,
		DresserBroke,
		// row 12
		PickAxe = 12 * TilesAcross,
		Shovel,
		Scythe,
		Mace,
		Club,
		// row 13
		Shield0 = 13 * TilesAcross,
		Shield1,
		Shield2,
		Shield3,
		Shield4,
		Shield5,
		Shield6,
		Shield7,
		Shield8,
		Shield9,
		Shield10,
		Shield11,
		Shield12,
		Shield13,
		Shield14,
		Shield15,
		Shield16,
		Shield17,
		Shield18,
		Shield19,
		Shield20,
		Shield21,
		Shield22,
		Shield23,
		Shield24,
		Shield25,
		// row 14
		Sword1H00 = 14 * TilesAcross,
		Sword1H01,
		Sword1H02,
		Sword2H00,
		Sword2H01,
		Sword2H02,
		Dagger0,
		Sword2H03,
		Sword2H04,
		Sword1H03,
		Mace0,
		Bow0,
		Axe0,
		HeadSetArmor0,
		ChestSetArmor0,
		LegSetArmor0,
		HandSetArmor0,
		FeetSetArmor0,
		ShieldSet0,
		// row 15
		Sword1H10 = 15 * TilesAcross,
		Sword1H11,
		Sword1H12,
		Sword2H10,
		Sword2H11,
		Sword2H12,
		Dagger1,
		Sword2H13,
		Sword2H14,
		Sword1H13,
		Mace1,
		Bow1,
		Axe1,
		HeadSetArmor1,
		ChestSetArmor1,
		LegSetArmor1,
		HandSetArmor1,
		FeetSetArmor1,
		ShieldSet1,
		// row 16
		Sword1H20 = 16 * TilesAcross,
		Sword1H21,
		Sword1H22,
		Sword2H20,
		Sword2H21,
		Sword2H22,
		Dagger2,
		Sword2H23,
		Sword2H24,
		Sword1H23,
		Mace2,
		Bow2,
		Axe2,
		HeadSetArmor2,
		ChestSetArmor2,
		LegSetArmor2,
		HandSetArmor2,
		FeetSetArmor2,
		ShieldSet2,
		// row 17
		Sword1H30 = 17 * TilesAcross,
		Sword1H31,
		Sword1H32,
		Sword2H30,
		Sword2H31,
		Sword2H32,
		Dagger3,
		Sword2H33,
		Sword2H34,
		Sword1H33,
		Mace3,
		Bow3,
		Axe3,
		HeadSetArmor3,
		ChestSetArmor3,
		LegSetArmor3,
		HandSetArmor3,
		FeetSetArmor3,
		ShieldSet3,
		// row 18
		Sword1H40 = 18 * TilesAcross,
		Sword1H41,
		Sword1H42,
		Sword2H40,
		Sword2H41,
		Sword2H42,
		Dagger4,
		Sword2H43,
		Sword2H44,
		Sword1H43,
		Mace4,
		Bow4,
		Axe4,
		HeadSetArmor4,
		ChestSetArmor4,
		LegSetArmor4,
		HandSetArmor4,
		FeetSetArmor4,
		ShieldSet4,
		// row 19
		Sword1H50 = 19 * TilesAcross,
		Sword1H51,
		Sword1H52,
		Sword2H50,
		Sword2H51,
		Sword2H52,
		Dagger5,
		Sword2H53,
		Sword2H54,
		Sword1H53,
		Mace5,
		Bow5,
		Axe5,
		HeadSetArmor5,
		ChestSetArmor5,
		LegSetArmor5,
		HandSetArmor5,
		FeetSetArmor5,
		ShieldSet5,
		// row 20
		Sword1H60 = 20 * TilesAcross,
		Sword1H61,
		Sword1H62,
		Sword2H60,
		Sword2H61,
		Sword2H62,
		Dagger6,
		Sword2H63,
		Sword2H64,
		Sword1H63,
		Mace6,
		Bow6,
		Axe6,
		HeadSetArmor6,
		ChestSetArmor6,
		LegSetArmor6,
		HandSetArmor6,
		FeetSetArmor6,
		ShieldSet6,
		// row 21
		Sword1H70 = 21 * TilesAcross,
		Sword1H71,
		Sword1H72,
		Sword2H70,
		Sword2H71,
		Sword2H72,
		Dagger7,
		Sword2H73,
		Sword2H74,
		Sword1H73,
		Mace7,
		Bow7,
		Axe7,
		HeadSetArmor7,
		ChestSetArmor7,
		LegSetArmor7,
		HandSetArmor7,
		FeetSetArmor7,
		ShieldSet7,
		// row 22
		Sword1H80 = 22 * TilesAcross,
		Sword1H81,
		Sword1H82,
		Sword2H80,
		Sword2H81,
		Sword2H82,
		Dagger8,
		Sword2H83,
		Sword2H84,
		Sword1H83,
		Mace8,
		Bow8,
		Axe8,
		HeadSetArmor8,
		ChestSetArmor8,
		LegSetArmor8,
		HandSetArmor8,
		FeetSetArmor8,
		ShieldSet8,
		// row 23
		Sword1H90 = 23 * TilesAcross,
		Sword1H91,
		Sword1H92,
		Sword2H90,
		Sword2H91,
		Sword2H92,
		Dagger9,
		Sword2H93,
		Sword2H94,
		Sword1H93,
		Mace9,
		Bow9,
		Axe9,
		HeadSetArmor9,
		ChestSetArmor9,
		LegSetArmor9,
		HandSetArmor9,
		FeetSetArmor9,
		ShieldSet9,
		// row 24
		Sword1H100 = 24 * TilesAcross,
		Sword1H101,
		Sword1H102,
		Sword2H100,
		Sword2H101,
		Sword2H102,
		Dagger10,
		Sword2H103,
		Sword2H104,
		Sword1H103,
		Mace10,
		Bow10,
		Axe10,
		HeadSetArmor10,
		ChestSetArmor10,
		LegSetArmor10,
		HandSetArmor10,
		FeetSetArmor10,
		ShieldSet10,
		// row 25
		Sword1H110 = 25 * TilesAcross,
		Sword1H111,
		Sword1H112,
		Sword2H110,
		Sword2H111,
		Sword2H112,
		Dagger11,
		Sword2H113,
		Sword2H114,
		Sword1H113,
		Mace11,
		Bow11,
		Axe11,
		HeadSetArmor11,
		ChestSetArmor11,
		LegSetArmor11,
		HandSetArmor11,
		FeetSetArmor11,
		ShieldSet11,
		// row 26
		Sword1H120 = 26 * TilesAcross,
		Sword1H121,
		Sword1H122,
		Sword2H120,
		Sword2H121,
		Sword2H122,
		Dagger12,
		Sword2H123,
		Sword2H124,
		Sword1H123,
		Mace12,
		Bow12,
		Axe12,
		HeadSetArmor12,
		ChestSetArmor12,
		LegSetArmor12,
		HandSetArmor12,
		FeetSetArmor12,
		ShieldSet12,
		// row 27
		Sword1H130 = 27 * TilesAcross,
		Sword1H131,
		Sword1H132,
		Sword2H130,
		Sword2H131,
		Sword2H132,
		Dagger13,
		Sword2H133,
		Sword2H134,
		Sword1H133,
		Mace13,
		Bow13,
		Axe13,
		HeadSetArmor13,
		ChestSetArmor13,
		LegSetArmor13,
		HandSetArmor13,
		FeetSetArmor13,
		ShieldSet13,
		// row 28
		Sword1H140 = 28 * TilesAcross,
		Sword1H141,
		Sword1H142,
		Sword2H140,
		Sword2H141,
		Sword2H142,
		Dagger14,
		Sword2H143,
		Sword2H144,
		Sword1H143,
		Mace14,
		Bow14,
		Axe14,
		HeadSetArmor14,
		ChestSetArmor14,
		LegSetArmor14,
		HandSetArmor14,
		FeetSetArmor14,
		ShieldSet14,
		// row 29
		Sword1H150 = 29 * TilesAcross,
		Sword1H151,
		Sword1H152,
		Sword2H150,
		Sword2H151,
		Sword2H152,
		Dagger15,
		Sword2H153,
		Sword2H154,
		Sword1H153,
		Mace15,
		Bow15,
		Axe15,
		HeadSetArmor15,
		ChestSetArmor15,
		LegSetArmor15,
		HandSetArmor15,
		FeetSetArmor15,
		ShieldSet15,
		// row 30
		Sword1H160 = 30 * TilesAcross,
		Sword1H161,
		Sword1H162,
		Sword2H160,
		Sword2H161,
		Sword2H162,
		Dagger16,
		Sword2H163,
		Sword2H164,
		Sword1H163,
		Mace16,
		Bow16,
		Axe16,
		HeadSetArmor16,
		ChestSetArmor16,
		LegSetArmor16,
		HandSetArmor16,
		FeetSetArmor16,
		ShieldSet16,
		// row 31
		Sword1H170 = 31 * TilesAcross,
		Sword1H171,
		Sword1H172,
		Sword2H170,
		Sword2H171,
		Sword2H172,
		Dagger17,
		Sword2H173,
		Sword2H174,
		Sword1H173,
		Mace17,
		Bow17,
		Axe17,
		HeadSetArmor17,
		ChestSetArmor17,
		LegSetArmor17,
		HandSetArmor17,
		FeetSetArmor17,
		ShieldSet17,
		// row 32
		Sword1H180 = 32 * TilesAcross,
		Sword1H181,
		Sword1H182,
		Sword2H180,
		Sword2H181,
		Sword2H182,
		Dagger18,
		Sword2H183,
		Sword2H184,
		Sword1H183,
		Mace18,
		Bow18,
		Axe18,
		HeadSetArmor18,
		ChestSetArmor18,
		LegSetArmor18,
		HandSetArmor18,
		FeetSetArmor18,
		ShieldSet18,
		// row 33
		Sword1H190 = 33 * TilesAcross,
		Sword1H191,
		Sword1H192,
		Sword2H190,
		Sword2H191,
		Sword2H192,
		Dagger19,
		Sword2H193,
		Sword2H194,
		Sword1H193,
		Mace19,
		Bow19,
		Axe19,
		HeadSetArmor19,
		ChestSetArmor19,
		LegSetArmor19,
		HandSetArmor19,
		FeetSetArmor19,
		ShieldSet19,
		// row 34
		Sword1H200 = 34 * TilesAcross,
		Sword1H201,
		Sword1H202,
		Sword2H200,
		Sword2H201,
		Sword2H202,
		Dagger20,
		Sword2H203,
		Sword2H204,
		Sword1H203,
		Mace20,
		Bow20,
		Axe20,
		HeadSetArmor20,
		ChestSetArmor20,
		LegSetArmor20,
		HandSetArmor20,
		FeetSetArmor20,
		ShieldSet20,
		// row 35
		Sword1H210 = 35 * TilesAcross,
		Sword1H211,
		Sword1H212,
		Sword2H210,
		Sword2H211,
		Sword2H212,
		Dagger21,
		Sword2H213,
		Sword2H214,
		Sword1H213,
		Mace21,
		Bow21,
		Axe21,
		HeadSetArmor21,
		ChestSetArmor21,
		LegSetArmor21,
		HandSetArmor21,
		FeetSetArmor21,
		ShieldSet21,
		// row 36
		Sword1H220 = 36 * TilesAcross,
		Sword1H221,
		Sword1H222,
		Sword2H220,
		Sword2H221,
		Sword2H222,
		Dagger22,
		Sword2H223,
		Sword2H224,
		Sword1H223,
		Mace22,
		Bow22,
		Axe22,
		HeadSetArmor22,
		ChestSetArmor22,
		LegSetArmor22,
		HandSetArmor22,
		FeetSetArmor22,
		ShieldSet22,
		// row 37
		Sword1H230 = 37 * TilesAcross,
		Sword1H231,
		Sword1H232,
		Sword2H230,
		Sword2H231,
		Sword2H232,
		Dagger23,
		Sword2H233,
		Sword2H234,
		Sword1H233,
		Mace23,
		Bow23,
		Axe23,
		HeadSetArmor23,
		ChestSetArmor23,
		LegSetArmor23,
		HandSetArmor23,
		FeetSetArmor23,
		ShieldSet23,
		// row 38
		Sword1H240 = 38 * TilesAcross,
		Sword1H241,
		Sword1H242,
		Sword2H240,
		Sword2H241,
		Sword2H242,
		Dagger24,
		Sword2H243,
		Sword2H244,
		Sword1H243,
		Mace24,
		Bow24,
		Axe24,
		HeadSetArmor24,
		ChestSetArmor24,
		LegSetArmor24,
		HandSetArmor24,
		FeetSetArmor24,
		ShieldSet24,
		// row 39
		Sword1H250 = 39 * TilesAcross,
		Sword1H251,
		Sword1H252,
		Sword2H250,
		Sword2H251,
		Sword2H252,
		Dagger25,
		Sword2H253,
		Sword2H254,
		Sword1H253,
		Mace25,
		Bow25,
		Axe25,
		HeadSetArmor25,
		ChestSetArmor25,
		LegSetArmor25,
		HandSetArmor25,
		FeetSetArmor25,
		ShieldSet25,
		// row 40
		Sword1H260 = 40 * TilesAcross,
		Sword1H261,
		Sword1H262,
		Sword2H260,
		Sword2H261,
		Sword2H262,
		Dagger26,
		Sword2H263,
		Sword2H264,
		Sword1H263,
		Mace26,
		Bow26,
		Axe26,
		HeadSetArmor26,
		ChestSetArmor26,
		LegSetArmor26,
		HandSetArmor26,
		FeetSetArmor26,
		ShieldSet26,
		// row 43
		CharmA0 = 43 * TilesAcross,
		CharmA1,
		CharmA2,
		CharmA3,
		CharmA4,
		CharmA5,
		CharmA6,
		CharmA7,
		CharmA8,
		CharmA9,
		CharmA10,
		CharmA11,
		CharmA12,
		CharmA13,
		CharmA14,
		CharmA15,
		CharmA16,
		CharmA17,
		CharmA18,
		CharmA19,
		CharmA20,
		CharmA21,
		CharmA22,
		CharmA23,
		CharmA24,
		CharmA25,
		CharmA26,
		CharmA27,
		CharmA28,
		CharmA29,
		CharmA30,
		// row 44
		CharmB0 = 44 * TilesAcross,
		CharmB1,
		CharmB2,
		CharmB3,
		CharmB4,
		CharmB5,
		CharmB6,
		CharmB7,
		CharmB8,
		CharmB9,
		CharmB10,
		CharmB11,
		CharmB12,
		CharmB13,
		CharmB14,
		CharmB15,
		CharmB16,
		CharmB17,
		CharmB18,
		CharmB19,
		CharmB20,
		CharmB21,
		CharmB22,
		CharmB23,
		CharmB24,
		CharmB25,
		CharmB26,
		CharmB27,
		CharmB28,
		CharmB29,
		CharmB30,
		CharmB31,
		// row 45
		CharmC0 = 45 * TilesAcross,
		CharmC1,
		CharmC2,
		CharmC3,
		CharmC4,
		CharmC5,
		CharmC6,
		CharmC7,
		CharmC8,
		CharmC9,
		CharmC10,
		CharmC11,
		CharmC12,
		CharmC13,
		CharmC14,
		CharmC15,
		CharmC16,
		CharmC17,
		CharmC18,
		CharmC19,
		CharmC20,
		CharmC21,
		CharmC22,
		CharmC23,
		CharmC24,
		CharmC25,
		CharmC26,
		CharmC27,
		CharmC28,
		CharmC29,
		CharmC30,
		CharmC31,
		// row 46
		CharmD0 = 46 * TilesAcross,
		CharmD1,
		CharmD2,
		CharmD3,
		CharmD4,
		CharmD5,
		CharmD6,
		CharmD7,
		CharmD8,
		CharmD9,
		CharmD10,
		CharmD11,
		CharmD12,
		CharmD13,
		CharmD14,
		CharmD15,
		CharmD16,
		CharmD17,
		CharmD18,
		CharmD19,
		CharmD20,
		CharmD21,
		CharmD22,
		CharmD23,
		CharmD24,
		CharmD25,
		CharmD26,
		CharmD27,
		CharmD28,
		CharmD29,
		CharmD30,
		CharmD31,
		// row 47
		CharmE0 = 47 * TilesAcross,
		CharmE1,
		CharmE2,
		CharmE3,
		CharmE4,
		CharmE5,
		CharmE6,
		CharmE7,
		CharmE8,
		CharmE9,
		CharmE10,
		CharmE11,
		CharmE12,
		CharmE13,
		CharmE14,
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
	const float _waterSpeed = 8f;
	const float _fogSpeed = 4f;
	const float _lampSpeed = 8f;
	const float _smokeSpeed = 4f;
	static readonly int[] _shallowBack = new Tile[] { Tile.WaterShallowBack0, Tile.WaterShallowBack1, Tile.WaterShallowBack2, Tile.WaterShallowBack3, Tile.WaterShallowBack4, Tile.WaterShallowBack5, Tile.WaterShallowBack6 }.Cast<int>().ToArray();
	static readonly int[] _deepBack = new Tile[] { Tile.WaterDeepBack0, Tile.WaterDeepBack1, Tile.WaterDeepBack2, Tile.WaterDeepBack3, Tile.WaterDeepBack4, Tile.WaterDeepBack5, Tile.WaterDeepBack6 }.Cast<int>().ToArray();
	static readonly int[] _shallowFront = new Tile[] { Tile.WaterShallowFront0, Tile.WaterShallowFront1, Tile.WaterShallowFront2, Tile.WaterShallowFront3, Tile.WaterShallowFront4, Tile.WaterShallowFront5, Tile.WaterShallowFront6 }.Cast<int>().ToArray();
	static readonly int[] _deepFront = new Tile[] { Tile.WaterDeepFront0, Tile.WaterDeepFront1, Tile.WaterDeepFront2, Tile.WaterDeepFront3, Tile.WaterDeepFront4, Tile.WaterDeepFront5, Tile.WaterDeepFront6 }.Cast<int>().ToArray();
	static readonly int[] _shallowBackGreen = new Tile[] { Tile.GreenWaterShallowBack0, Tile.GreenWaterShallowBack1, Tile.GreenWaterShallowBack2, Tile.GreenWaterShallowBack3, Tile.GreenWaterShallowBack4, Tile.GreenWaterShallowBack5, Tile.GreenWaterShallowBack6 }.Cast<int>().ToArray();
	static readonly int[] _deepBackGreen = new Tile[] { Tile.GreenWaterDeepBack0, Tile.GreenWaterDeepBack1, Tile.GreenWaterDeepBack2, Tile.GreenWaterDeepBack3, Tile.GreenWaterDeepBack4, Tile.GreenWaterDeepBack5, Tile.GreenWaterDeepBack6 }.Cast<int>().ToArray();
	static readonly int[] _shallowFrontGreen = new Tile[] { Tile.GreenWaterShallowFront0, Tile.GreenWaterShallowFront1, Tile.GreenWaterShallowFront2, Tile.GreenWaterShallowFront3, Tile.GreenWaterShallowFront4, Tile.GreenWaterShallowFront5, Tile.GreenWaterShallowFront6 }.Cast<int>().ToArray();
	static readonly int[] _deepFrontGreen = new Tile[] { Tile.GreenWaterDeepFront0, Tile.GreenWaterDeepFront1, Tile.GreenWaterDeepFront2, Tile.GreenWaterDeepFront3, Tile.GreenWaterDeepFront4, Tile.GreenWaterDeepFront5, Tile.GreenWaterDeepFront6 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyPurpleFore = new Tile[] { Tile.FogHeavyPurpleBottom0, Tile.FogHeavyPurpleBottom1, Tile.FogHeavyPurpleBottom2, Tile.FogHeavyPurpleBottom3, Tile.FogHeavyPurpleBottom4, Tile.FogHeavyPurpleBottom5, Tile.FogHeavyPurpleBottom6, Tile.FogHeavyPurpleBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyPurpleBack = new Tile[] { Tile.FogHeavyPurpleTop0, Tile.FogHeavyPurpleTop1, Tile.FogHeavyPurpleTop2, Tile.FogHeavyPurpleTop3, Tile.FogHeavyPurpleTop4, Tile.FogHeavyPurpleTop5, Tile.FogHeavyPurpleTop6, Tile.FogHeavyPurpleTop7 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyRedFore = new Tile[] { Tile.FogHeavyRedBottom0, Tile.FogHeavyRedBottom1, Tile.FogHeavyRedBottom2, Tile.FogHeavyRedBottom3, Tile.FogHeavyRedBottom4, Tile.FogHeavyRedBottom5, Tile.FogHeavyRedBottom6, Tile.FogHeavyRedBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyRedBack = new Tile[] { Tile.FogHeavyRedTop0, Tile.FogHeavyRedTop1, Tile.FogHeavyRedTop2, Tile.FogHeavyRedTop3, Tile.FogHeavyRedTop4, Tile.FogHeavyRedTop5, Tile.FogHeavyRedTop6, Tile.FogHeavyRedTop7 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyBlueFore = new Tile[] { Tile.FogHeavyBlueBottom0, Tile.FogHeavyBlueBottom1, Tile.FogHeavyBlueBottom2, Tile.FogHeavyBlueBottom3, Tile.FogHeavyBlueBottom4, Tile.FogHeavyBlueBottom5, Tile.FogHeavyBlueBottom6, Tile.FogHeavyBlueBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyBlueBack = new Tile[] { Tile.FogHeavyBlueTop0, Tile.FogHeavyBlueTop1, Tile.FogHeavyBlueTop2, Tile.FogHeavyBlueTop3, Tile.FogHeavyBlueTop4, Tile.FogHeavyBlueTop5, Tile.FogHeavyBlueTop6, Tile.FogHeavyBlueTop7 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyGreenFore = new Tile[] { Tile.FogHeavyGreenBottom0, Tile.FogHeavyGreenBottom1, Tile.FogHeavyGreenBottom2, Tile.FogHeavyGreenBottom3, Tile.FogHeavyGreenBottom4, Tile.FogHeavyGreenBottom5, Tile.FogHeavyGreenBottom6, Tile.FogHeavyGreenBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyGreenBack = new Tile[] { Tile.FogHeavyGreenTop0, Tile.FogHeavyGreenTop1, Tile.FogHeavyGreenTop2, Tile.FogHeavyGreenTop3, Tile.FogHeavyGreenTop4, Tile.FogHeavyGreenTop5, Tile.FogHeavyGreenTop6, Tile.FogHeavyGreenTop7 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyBlackFore = new Tile[] { Tile.FogHeavyBlackBottom0, Tile.FogHeavyBlackBottom1, Tile.FogHeavyBlackBottom2, Tile.FogHeavyBlackBottom3, Tile.FogHeavyBlackBottom4, Tile.FogHeavyBlackBottom5, Tile.FogHeavyBlackBottom6, Tile.FogHeavyBlackBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogHeavyBlackBack = new Tile[] { Tile.FogHeavyBlackTop0, Tile.FogHeavyBlackTop1, Tile.FogHeavyBlackTop2, Tile.FogHeavyBlackTop3, Tile.FogHeavyBlackTop4, Tile.FogHeavyBlackTop5, Tile.FogHeavyBlackTop6, Tile.FogHeavyBlackTop7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightPurpleFore = new Tile[] { Tile.FogLightPurpleBottom0, Tile.FogLightPurpleBottom1, Tile.FogLightPurpleBottom2, Tile.FogLightPurpleBottom3, Tile.FogLightPurpleBottom4, Tile.FogLightPurpleBottom5, Tile.FogLightPurpleBottom6, Tile.FogLightPurpleBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightPurpleBack = new Tile[] { Tile.FogLightPurpleTop0, Tile.FogLightPurpleTop1, Tile.FogLightPurpleTop2, Tile.FogLightPurpleTop3, Tile.FogLightPurpleTop4, Tile.FogLightPurpleTop5, Tile.FogLightPurpleTop6, Tile.FogLightPurpleTop7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightRedFore = new Tile[] { Tile.FogLightRedBottom0, Tile.FogLightRedBottom1, Tile.FogLightRedBottom2, Tile.FogLightRedBottom3, Tile.FogLightRedBottom4, Tile.FogLightRedBottom5, Tile.FogLightRedBottom6, Tile.FogLightRedBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightRedBack = new Tile[] { Tile.FogLightRedTop0, Tile.FogLightRedTop1, Tile.FogLightRedTop2, Tile.FogLightRedTop3, Tile.FogLightRedTop4, Tile.FogLightRedTop5, Tile.FogLightRedTop6, Tile.FogLightRedTop7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightBlueFore = new Tile[] { Tile.FogLightBlueBottom0, Tile.FogLightBlueBottom1, Tile.FogLightBlueBottom2, Tile.FogLightBlueBottom3, Tile.FogLightBlueBottom4, Tile.FogLightBlueBottom5, Tile.FogLightBlueBottom6, Tile.FogLightBlueBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightBlueBack = new Tile[] { Tile.FogLightBlueTop0, Tile.FogLightBlueTop1, Tile.FogLightBlueTop2, Tile.FogLightBlueTop3, Tile.FogLightBlueTop4, Tile.FogLightBlueTop5, Tile.FogLightBlueTop6, Tile.FogLightBlueTop7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightGreenFore = new Tile[] { Tile.FogLightGreenBottom0, Tile.FogLightGreenBottom1, Tile.FogLightGreenBottom2, Tile.FogLightGreenBottom3, Tile.FogLightGreenBottom4, Tile.FogLightGreenBottom5, Tile.FogLightGreenBottom6, Tile.FogLightGreenBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightGreenBack = new Tile[] { Tile.FogLightGreenTop0, Tile.FogLightGreenTop1, Tile.FogLightGreenTop2, Tile.FogLightGreenTop3, Tile.FogLightGreenTop4, Tile.FogLightGreenTop5, Tile.FogLightGreenTop6, Tile.FogLightGreenTop7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightBlackFore = new Tile[] { Tile.FogLightBlackBottom0, Tile.FogLightBlackBottom1, Tile.FogLightBlackBottom2, Tile.FogLightBlackBottom3, Tile.FogLightBlackBottom4, Tile.FogLightBlackBottom5, Tile.FogLightBlackBottom6, Tile.FogLightBlackBottom7 }.Cast<int>().ToArray();
	static readonly int[] _fogLightBlackBack = new Tile[] { Tile.FogLightBlackTop0, Tile.FogLightBlackTop1, Tile.FogLightBlackTop2, Tile.FogLightBlackTop3, Tile.FogLightBlackTop4, Tile.FogLightBlackTop5, Tile.FogLightBlackTop6, Tile.FogLightBlackTop7 }.Cast<int>().ToArray();
	static readonly int[] _portal = new Tile[] { Tile.Portal0, Tile.Portal1, Tile.Portal2, Tile.Portal3, Tile.Portal4 }.Cast<int>().ToArray();
	static readonly int[] _bugs = new Tile[] { Tile.TownGrid01, Tile.TownGrid02, Tile.TownGrid03, Tile.TownGrid04, Tile.TownGrid05 }.Cast<int>().ToArray();
	static readonly int[] _fountain = new Tile[] { Tile.Fountain0, Tile.Fountain1, Tile.Fountain2, Tile.Fountain3, Tile.Fountain4, Tile.Fountain5 }.Cast<int>().ToArray();
	static readonly int[][] _lamps =
						{
							new Tile[] {
								Tile.TownGrid18,
								Tile.TownGrid19,
								Tile.TownGrid1A,
								Tile.TownGrid19,
							}.Cast<int>().ToArray(),
							new Tile[] {
								Tile.TownGrid18,
								Tile.TownGrid19,
								Tile.TownGrid1A,
								Tile.TownGrid19,

								Tile.TownGrid18,
								Tile.TownGrid19,
								Tile.TownGrid1A,
								Tile.TownGrid19,

								Tile.TownGrid18,
								Tile.TownGrid19,
								Tile.TownGrid1A,
								Tile.TownGrid19,
							}.Cast<int>().ToArray(),
							new Tile[] {
								Tile.TownGrid18,
								Tile.TownGrid19,
								Tile.TownGrid1A,
								Tile.TownGrid1B,
								Tile.TownGrid1A,
								Tile.TownGrid19,
							}.Cast<int>().ToArray(),
							new Tile[] {
								Tile.TownGrid18,
								Tile.TownGrid19,
								Tile.TownGrid1A,
								Tile.TownGrid1B,
								Tile.TownGrid1A,
								Tile.TownGrid19,

								Tile.TownGrid18,
								Tile.TownGrid19,
								Tile.TownGrid1A,
								Tile.TownGrid1B,
								Tile.TownGrid1A,
								Tile.TownGrid19,

								Tile.TownGrid18,
								Tile.TownGrid19,
								Tile.TownGrid1A,
								Tile.TownGrid1B,
								Tile.TownGrid1A,
								Tile.TownGrid19,
							}.Cast<int>().ToArray(),
							new Tile[] {
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18
							}.Cast<int>().ToArray(),
							new Tile[] {
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18,
								Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18, Tile.TownGrid18
							}.Cast<int>().ToArray(),
							new Tile[] {
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B
							}.Cast<int>().ToArray(),
							new Tile[] {
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B,
								Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B, Tile.TownGrid1B
							}.Cast<int>().ToArray()
						};
	static readonly int[] _outhouseBird0 = new Tile[] { Tile.TownOuthouseBird00, Tile.TownOuthouseBird10, Tile.TownOuthouseBird20, Tile.TownOuthouseBird30, Tile.TownOuthouseBird40, Tile.TownOuthouseBird50, Tile.TownOuthouseBird60, Tile.TownOuthouseBird70, Tile.TownOuthouseBird80 }.Cast<int>().ToArray();
	static readonly int[] _outhouseBird1 = new Tile[] { Tile.TownOuthouseBird01, Tile.TownOuthouseBird11, Tile.TownOuthouseBird21, Tile.TownOuthouseBird31, Tile.TownOuthouseBird41, Tile.TownOuthouseBird51, Tile.TownOuthouseBird61, Tile.TownOuthouseBird71, Tile.TownOuthouseBird81 }.Cast<int>().ToArray();
	static readonly int[] _homeSmoke0 = new Tile[] { Tile.TownHomeSmoke000, Tile.TownHomeSmoke100, Tile.TownHomeSmoke200, Tile.TownHomeSmoke300, Tile.TownHomeSmoke400, Tile.TownHomeSmoke500 }.Cast<int>().ToArray();
	static readonly int[] _homeSmoke1 = new Tile[] { Tile.TownHomeSmoke010, Tile.TownHomeSmoke110, Tile.TownHomeSmoke210, Tile.TownHomeSmoke310, Tile.TownHomeSmoke410, Tile.TownHomeSmoke510 }.Cast<int>().ToArray();
	static readonly int[] _homeSmoke2 = new Tile[] { Tile.TownHomeSmoke001, Tile.TownHomeSmoke101, Tile.TownHomeSmoke201, Tile.TownHomeSmoke301, Tile.TownHomeSmoke401, Tile.TownHomeSmoke501 }.Cast<int>().ToArray();
	static readonly int[] _homeSmoke3 = new Tile[] { Tile.TownHomeSmoke011, Tile.TownHomeSmoke111, Tile.TownHomeSmoke211, Tile.TownHomeSmoke311, Tile.TownHomeSmoke411, Tile.TownHomeSmoke511 }.Cast<int>().ToArray();
	static readonly int[] _rockSmoke0 = new Tile[] { Tile.TownRockSmoke00, Tile.TownRockSmoke10, Tile.TownRockSmoke20, Tile.TownRockSmoke30, Tile.TownRockSmoke40 }.Cast<int>().ToArray();
	static readonly int[] _rockSmoke1 = new Tile[] { Tile.TownRockSmoke01, Tile.TownRockSmoke11, Tile.TownRockSmoke21, Tile.TownRockSmoke31, Tile.TownRockSmoke41 }.Cast<int>().ToArray();
	static readonly int[] _shopSmoke0 = new Tile[] { Tile.TownShopSmoke00, Tile.TownShopSmoke10, Tile.TownShopSmoke20, Tile.TownShopSmoke30, Tile.TownShopSmoke40, Tile.TownShopSmoke50 }.Cast<int>().ToArray();
	static readonly int[] _shopSmoke1 = new Tile[] { Tile.TownShopSmoke01, Tile.TownShopSmoke11, Tile.TownShopSmoke21, Tile.TownShopSmoke31, Tile.TownShopSmoke41, Tile.TownShopSmoke51 }.Cast<int>().ToArray();
	static readonly int[] _shopSmoke2 = new Tile[] { Tile.TownShopSmoke02, Tile.TownShopSmoke12, Tile.TownShopSmoke22, Tile.TownShopSmoke32, Tile.TownShopSmoke42, Tile.TownShopSmoke52 }.Cast<int>().ToArray();
	static readonly int[] _tavernSmoke0 = new Tile[] { Tile.TownTavernSmoke00, Tile.TownTavernSmoke10, Tile.TownTavernSmoke20, Tile.TownTavernSmoke30, Tile.TownTavernSmoke40, Tile.TownTavernSmoke50 }.Cast<int>().ToArray();
	static readonly int[] _tavernSmoke1 = new Tile[] { Tile.TownTavernSmoke01, Tile.TownTavernSmoke11, Tile.TownTavernSmoke21, Tile.TownTavernSmoke31, Tile.TownTavernSmoke41, Tile.TownTavernSmoke51 }.Cast<int>().ToArray();
	protected override List<Animation> GetAnimations()
	{
		var animations = new List<Animation>
		{
			new Animation() { Frames = new int[][] {_torch}, Fps = _torchSpeed },
			new Animation() { Frames = _bannerA, Fps = _bannerSpeed },
			new Animation() { Frames = _bannerB, Fps = _bannerSpeed },
			new Animation() { Frames = new int[][] { _shallowBack }, Fps = _waterSpeed, Sync = true },
			new Animation() { Frames = new int[][] { _deepBack }, Fps = _waterSpeed, Sync = true },
			new Animation() { Frames = new int[][] { _shallowFront }, Fps = _waterSpeed, Sync = true },
			new Animation() { Frames = new int[][] { _deepFront }, Fps = _waterSpeed, Sync = true },
			new Animation() { Frames = new int[][] { _shallowBackGreen }, Fps = _waterSpeed, Sync = true },
			new Animation() { Frames = new int[][] { _deepBackGreen }, Fps = _waterSpeed, Sync = true },
			new Animation() { Frames = new int[][] { _shallowFrontGreen }, Fps = _waterSpeed, Sync = true },
			new Animation() { Frames = new int[][] { _deepFrontGreen }, Fps = _waterSpeed, Sync = true },
			new Animation() { Frames = new int[][] { _fogHeavyPurpleFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogHeavyPurpleBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogHeavyRedFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogHeavyRedBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogHeavyBlueFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogHeavyBlueBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogHeavyGreenFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogHeavyGreenBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogHeavyBlackFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogHeavyBlackBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightPurpleFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightPurpleBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightRedFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightRedBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightBlueFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightBlueBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightGreenFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightGreenBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightBlackFore }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _fogLightBlackBack }, Fps = _fogSpeed },
			new Animation() { Frames = new int[][] { _portal }, Fps = _waterSpeed },
			new Animation() { Frames = new int[][] { _bugs }, Fps = _waterSpeed },
			new Animation() { Frames = new int[][] { _fountain }, Fps = _waterSpeed },
			new Animation() { Frames = _lamps, Fps = _lampSpeed },
			new Animation() { Frames = new int[][] { _outhouseBird0 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _outhouseBird1 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _homeSmoke0 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _homeSmoke1 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _homeSmoke2 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _homeSmoke3 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _rockSmoke0 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _rockSmoke1 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _shopSmoke0 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _shopSmoke1 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _shopSmoke2 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _tavernSmoke0 }, Fps = _smokeSpeed },
			new Animation() { Frames = new int[][] { _tavernSmoke1 }, Fps = _smokeSpeed }
		};
		return animations;
	}
	public override bool Blocked(int index)
	{
		return Blocked(index, false);
	}
	private bool Blocked(int x, int y, bool light)
	{
		return Blocked(TileIndex(x, y), light);
	}
	private bool Blocked(int index, bool light)
	{
		if (light) return IsTownBlocked(index, light);
		return IsWall(index) || IsDoorShut(index) || IsTownBlocked(index, light);
	}
	public bool IsTownBlocked(int index, bool light = false)
	{
		var fore = GetTile((int)Layer.Fore, index);
		var tree = GetTile((int)Layer.Tree, index);
		if (fore == -1 && tree == -1)
 			return false;
		if ((light && (tree == (int)Tile.TownOuthouse21)) ||
			(fore == (int)Tile.TownTavern11) || (fore == (int)Tile.TownTavern12) ||
			(light && (fore == (int)Tile.TownTavern21)) || (fore == (int)Tile.TownTavern22) ||
			(fore == (int)Tile.TownShop11) || (fore == (int)Tile.TownShop12) ||
			(light && (fore == (int)Tile.TownShop21)) || (fore == (int)Tile.TownShop22) ||
			(fore == (int)Tile.TownHome11) || (fore == (int)Tile.TownHome12) || (fore == (int)Tile.TownHome13) ||
			(fore == (int)Tile.TownHome21) || (light && (fore == (int)Tile.TownHome22)) || (fore == (int)Tile.TownHome23) ||
			(fore == (int)Tile.TownGrid16))
		{
			return true;
		}
		else if (!light && ((fore == (int)Tile.TownGrid00) || (fore == (int)Tile.TownGrid0C) ||
			(fore == (int)Tile.TownGrid10) || (fore == (int)Tile.TownGrid15) || (fore == (int)Tile.TownGrid17) || (fore == (int)Tile.TownGrid1C) ||
			(fore == (int)Tile.TownGrid20) || (fore == (int)Tile.TownGrid25) || (fore == (int)Tile.TownGrid27) || (fore == (int)Tile.TownGrid2C) ||
			(fore == (int)Tile.TownGrid30) || (fore == (int)Tile.TownGrid31) || (fore == (int)Tile.TownGrid32) || (fore == (int)Tile.TownGrid33) || (fore == (int)Tile.TownGrid34) || (fore == (int)Tile.TownGrid35) ||
			(fore == (int)Tile.TownGrid37) || (fore == (int)Tile.TownGrid38) || (fore == (int)Tile.TownGrid39) || (fore == (int)Tile.TownGrid3A) || (fore == (int)Tile.TownGrid3B) || (fore == (int)Tile.TownGrid3C) ||
			(fore == (int)Tile.TownGrid44) || (fore == (int)Tile.TownGrid45) || (fore == (int)Tile.TownGrid47) || (fore == (int)Tile.TownGrid48)))
		{
			return true;
		}
		else if (light &&
			((tree >= (int)Tile.PineTree0Bottom) && (tree <= (int)Tile.PineTree2Bottom)) ||
			((tree >= (int)Tile.TreeDead0Bottom) && (tree <= (int)Tile.TreeDead2Bottom)) ||
			((tree >= (int)Tile.TreeStone0Bottom) && (tree <= (int)Tile.TreeStone2Bottom)))
		{
			return true;
		}
		return false;
	}
	public override bool IsDoor(int index)
	{
		return IsDoorOpen(index) || IsDoorShut(index) || IsDoorTown(index);
	}
	public bool IsDoorTown(int index)
	{
		var fore = GetTile((int)Layer.Fore, index);
		return ((fore == (int)Tile.TownTavern21) || (fore == (int)Tile.TownShop21) || (fore == (int)Tile.TownHome22));
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
		if (IsDoorShut(index))
			SetTile((int)Layer.Fore, index, (int)Tile.DoorOpen);
		else if (IsDoorOpen(index))
			SetTile((int)Layer.Fore, index, (int)Tile.DoorOpen);
	}
	public override bool IsStairDown(int index)
	{
		var fore = GetTile((int)Layer.Fore, index);
		return ((fore == (int)Tile.StairsDown) ||
			(fore == (int)Tile.GrassStairsDown) || (fore == (int)Tile.DesertStairsDown) ||
			(fore == (int)Tile.NightGrassStairsDown) || (fore == (int)Tile.NightDesertStairsDown) ||
			(fore == (int)Tile.TownStairsDown));
	}
	public override bool IsStairUp(int index)
	{
		var fore = GetTile((int)Layer.Fore, index);
		return ((fore == (int)Tile.StairsUp) ||
			(fore == (int)Tile.GrassStairsUp) || (fore == (int)Tile.DesertStairsUp) ||
			(fore == (int)Tile.NightGrassStairsUp) || (fore == (int)Tile.NightDesertStairsUp));
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
		return IsTownBlocked(index) || (fore >= (int)Tile.WallTorch0) && (fore <= (int)Tile.Wall7);
	}
	public bool IsFloor(Vector2 p) { return IsFloor((int)p.x, (int)p.y); }
	public bool IsFloor(int x, int y) { return IsFloor(TileIndex(x, y)); }
	public bool IsFloor(int index)
	{
		var back = GetTile((int)Layer.Back, index);
		return IsGrass(index) || (back >= (int)Tile.Floor0) && (back <= (int)Tile.FloorRoom5);
	}
	public bool IsGrass(int index)
	{
		var back = GetTile((int)Layer.Back, index);
		if ((back >= (int)Tile.Grass0) && (back <= (int)Tile.GrassRoad2)) return true;
		if ((back >= (int)Tile.NightGrass0) && (back <= (int)Tile.NightGrassRoad2)) return true;
		if ((back == (int)Tile.TownGrass0) || (back == (int)Tile.TownGrass1) ||
			(back == (int)Tile.TownGrass2) || (back == (int)Tile.TownGrass3) ||
			(back == (int)Tile.TownOuthouseBack) || (back == (int)Tile.TownGraveBack))
		{
			return true;
		}
		if (((back >= (int)Tile.TownEdge00) && (back <= (int)Tile.TownEdge03)) ||
			(back == (int)Tile.TownEdge10) || (back == (int)Tile.TownEdge13) ||
			(back == (int)Tile.TownEdge20) || (back == (int)Tile.TownEdge23) ||
			((back >= (int)Tile.TownEdge30) && (back <= (int)Tile.TownEdge33)))
		{
			return true;
		}
		return false;
	}
	public bool IsEdge(int index)
	{
		var back = GetTile((int)Layer.Back, index);
		return ((back >= (int)Tile.EdgeNE) && (back <= (int)Tile.Edge27));
	}
	public bool IsWater(int index)
	{
		var back = GetTile((int)Layer.SplitBackWater, index);
		return (IsWaterShallow(back) || IsWaterDeep(back));
	}
	public bool IsWaterShallow(int tile)
	{
		return (((tile >= (int)Tile.WaterShallowBack0) && (tile <= (int)Tile.WaterShallowBack6)) ||
			((tile >= (int)Tile.WaterShallowFront0) && (tile <= (int)Tile.WaterShallowFront6)) ||
			((tile >= (int)Tile.GreenWaterShallowBack0) && (tile <= (int)Tile.GreenWaterShallowBack6)) ||
			((tile >= (int)Tile.GreenWaterShallowFront0) && (tile <= (int)Tile.GreenWaterShallowFront6)));
	}
	public bool IsWaterDeep(int tile)
	{
		return (((tile >= (int)Tile.WaterDeepBack0) && (tile <= (int)Tile.WaterDeepBack6)) ||
			((tile >= (int)Tile.WaterDeepFront0) && (tile <= (int)Tile.WaterDeepFront6)) ||
			((tile >= (int)Tile.GreenWaterDeepBack0) && (tile <= (int)Tile.GreenWaterDeepBack6)) ||
			((tile >= (int)Tile.GreenWaterDeepFront0) && (tile <= (int)Tile.GreenWaterDeepFront6)));
	}
	public TextAsset MapJsonToLoad;
	void Start()
	{
		if (MapJsonToLoad != null)
		{
			LoadJson(MapJsonToLoad.text);
			DrawEdge();
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
				SetTile((int)Layer.Back, x, y, (int)Tile.Floor0 + Utility.Random.Next(6));
	}
	void Wall()
	{
		for (var y = 0; y < State.Height; y++)
			for (var x = 0; x < State.Width; x++)
				if (x == 0 || x == State.Width - 1 || y == 0 || y == State.Height - 1)
					SetTile((int)Layer.Fore, x, y, RandomWall());
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
		SetTile((int)Layer.Fore, 2, 5, (int)Tile.BannerA0);
		SetTile((int)Layer.Fore, 3, 5, (int)Tile.Bed);
		SetTile((int)Layer.Fore, 4, 5, (int)Tile.BannerB0);
		SetTile((int)Layer.Fore, 2, 4, (int)Tile.Table);
		SetTile((int)Layer.Fore, 3, 4, (int)Tile.Rug2);
		SetTile((int)Layer.Fore, 4, 4, (int)Tile.Chair0);
		SetTile((int)Layer.Fore, 1, 2, (int)Tile.DoorShut);
		var maxX = State.Width - 2;
		for (var i = 2; i <= maxX; i++)
			SetTile((int)Layer.Fore, i, 2, RandomWall());
		SetTile((int)Layer.Fore, maxX, 1, (int)Tile.StairsDown);
	}
	const int TorchRadius = 5;
	int RandomTorchRadius()
	{
		return Utility.Random.Next(TorchRadius) + 1;
	}
	public const int LightRadius = 7;
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
		if (InsideEdge(x, y))
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
					if (!InsideEdge((int)mx, (int)my))
						continue;
					if (isBlocked)
					{
						if (Blocked((int)mx, (int)my, true))
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
					else if (Blocked((int)mx, (int)my, true) && (radius < maxRadius))
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
	static Tile RandomEdge()
	{
		return Tile.Edge0 + Utility.Random.Next(28);
	}
	static Tile RandomEdgeInside()
	{
		Tile edge;
		var random = (float)Utility.Random.NextDouble();
		if (random < .333f)
		{
			edge = Tile.InsideEdge0;
		}
		else if (random < .777f)
		{
			edge = Tile.InsideEdge1 + Utility.Random.Next(7);
		}
		else
		{
			edge = Tile.InsideEdge8 + Utility.Random.Next(28);
		}
		return edge;
	}
	static Tile RandomEdgeInsideCorner()
	{
		return Tile.InsideEdgeCorner0 + Utility.Random.Next(22);
	}
	void DrawEdge()
	{
		for (var y = 0; y < State.Height; y++)
		{
			for (var x = 0; x < State.Width; x++)
			{
				if ((x == 0) || (x == State.Width - 1) || (y == 0) || (y == State.Height - 1))
				{
					if (x == 0 && y == 0) // sw
					{
						SetTile((int)Layer.Back, x, y, (int)Tile.EdgeSW);
					}
					else if (x == 0 && y == State.Height - 1) // nw
					{
						SetTile((int)Layer.Back, x, y, (int)Tile.EdgeNW);
					}
					else if (x == State.Width - 1 && y == 0) // se
					{
						SetTile((int)Layer.Back, x, y, (int)Tile.EdgeSE);
					}
					else if (x == State.Width - 1 && y == State.Height - 1) // ne
					{
						SetTile((int)Layer.Back, x, y, (int)Tile.EdgeNE);
					}
					else
					{
						SetTile((int)Layer.Back, x, y, (int)RandomEdge(), RandomFlipX());
					}
					if ((x == 0) && (y >= 1) && (y <= State.Height - 2)) // w
					{
						SetTileFlags((int)Layer.Back, x, y, RandomFlipY() | TileFlags.Rot90);
					}
					else if ((x == State.Width - 1) && (y >= 1) && (y <= State.Height - 2)) // e
					{
						SetTileFlags((int)Layer.Back, x, y, TileFlags.FlipX | RandomFlipY() | TileFlags.Rot90);
					}
					else if ((y == 0) && (x >= 1) && (x <= State.Width - 2)) // s
					{
						SetTileFlags((int)Layer.Back, x, y, RandomFlipX() | TileFlags.FlipY);
					}
				}
				else
				{
					if ((x == 1) || (x == State.Width - 2) || (y == 1) || (y == State.Height - 2))
					{
						if (x == 1 && y == 1) // sw
						{
							SetTile((int)Layer.Edge, x, y, (int)RandomEdgeInsideCorner(), TileFlags.FlipY);
						}
						else if (x == 1 && y == State.Height - 2) // nw
						{
							SetTile((int)Layer.Edge, x, y, (int)RandomEdgeInsideCorner());
						}
						else if (x == State.Width - 2 && y == 1) // se
						{
							SetTile((int)Layer.Edge, x, y, (int)RandomEdgeInsideCorner(), TileFlags.FlipX | TileFlags.FlipY);
						}
						else if (x == State.Width - 2 && y == State.Height - 2) // ne
						{
							SetTile((int)Layer.Edge, x, y, (int)RandomEdgeInsideCorner(), TileFlags.FlipX | TileFlags.FlipY | TileFlags.Rot90);
						}
						else
						{
							if ((x == 1) && (y >= 2) && (y <= State.Height - 3)) // w
							{
								SetTile((int)Layer.Edge, x, y, (int)RandomEdgeInside(), RandomFlipY() | TileFlags.Rot90);
							}
							else if ((x == State.Width - 2) && (y >= 2) && (y <= State.Height - 3)) // e
							{
								SetTile((int)Layer.Edge, x, y, (int)RandomEdgeInside(), TileFlags.FlipX | RandomFlipY() | TileFlags.Rot90);
							}
							else if ((y == 1) && (x >= 2) && (x <= State.Width - 3)) // s
							{
								SetTile((int)Layer.Edge, x, y, (int)RandomEdgeInside(), RandomFlipX() | TileFlags.FlipY);
							}
							else
							{
								SetTile((int)Layer.Edge, x, y, (int)RandomEdgeInside(), RandomFlipX());
							}
						}
					}
				}
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
		var edge = IsEdge(index);
		var water = IsWater(index);
		var color = screen ? Color.magenta.SetAlpha(.5f) : Color.clear;
		if (explored || lit)
		{
			if (stairs)
				color = Colors.YellowLight.SetAlpha(_bright);
			else if (door)
				color = Colors.BlueLight.SetAlpha(_bright);
			else if (wall && !screen)
				color = Colors.GreyDark.SetAlpha(_dim);
			else if (edge && !screen)
				color = Colors.ButtonBlue;
			else if (water)
				color = Colors.MapWaterLight.SetAlpha(_dim);
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
