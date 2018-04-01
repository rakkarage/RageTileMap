PixelLevels
art: benmhenry@gmail.com
code: davidahenry@gmail.com

Description:
Includes dozens of dungeon tile sets and props, many animated, also includes village tile set.<br>
Includes a simple tile map system with mini map, path finding, line of sight lighting, Pan and snap, pinch and zoom.<br>
<a href="https://github.com/rakkarage/RageTileMap">github</a><br>
<a href="http://gfycat.com/ActualCrazyAtlanticblackgoby">gif</a>

Documentation:
0. Explore, use, and copy from examples and / or:
1. Create empty game object, add TileMap. Set sheet image and layers and matching material.
	a. If materials only has one element will use for all layers, else will match layers with materials. For example in town level water layer uses additive material.
	b. Maps based on Free.png use three layers: Back, Game and Fore.
	c. Maps based on Level.png use fifteen layers: Back, Blood, Fore, Flower, SplitBack, SplitBackWater, SplitBackFog, Tree, SplitFore, SplitForeWater, SplitForeFog, Top, Effect, Light, Edge.
2. Run.
3. Use simple example map editor to draw map or do it with code like example.
3. Click Load map button on tile map or do it from code like example. Data/Test0.json and Data/Test1.json are made for Free with 3 layers. Data/Town.json is made for Levels.png with 15 layers.

Files:
PixelLevels/Audio/PinDrop.wav : used for example scene intro
PixelLevels/Data/Test0.json : example map
PixelLevels/Data/Test0.tmx.txt : example map in tmx format
PixelLevels/Data/Test1.json : example map
PixelLevels/Data/Test1.tmx.txt : example map in tmx format
PixelLevels/Data/Town.json : town map
PixelLevels/Prefab/Mob.prefab : used in example
PixelLevels/Prefab/Path.prefab : used in example
PixelLevels/Scene/Test.unity : example scene
PixelLevels/Scene/TestFree.unity : example scene
PixelLevels/Scene/TestTown.unity : example scene
PixelLevels/Scene/Intro.unity : example scene intro
PixelLevels/Script/Rage/Editor/TileMapEditor.cs : simple tile map editor
PixelLevels/Script/Rage/TileMap.cs (etc) : files used to setup example tile map and pathfinding and lighting etc
PixelLevels/Script/FreeTileMap.cs : code for free example tile map
PixelLevels/Script/Intro.cs : used for example scene intro
PixelLevels/Script/Mob.cs : used in example
PixelLevels/Script/TownTileMap.cs : code for town tile map
PixelLevels/Visual/Animation/Path.controller : path animation controller
PixelLevels/Visual/Animation/Slime.controller : slime animation controller used in example
PixelLevels/Visual/Animation/TargetBack.controller : target selection background, drawn at end of path
PixelLevels/Visual/Animation/TargetFore.controller : target selection foreground, drawn at end of path
PixelLevels/Visual/Sprite/Free.png : free tiles, all sprites on single sprite sheet
PixelLevels/Visual/Sprite/Henry.png : used in intro
PixelLevels/Visual/Sprite/Level.png : paid tiles, all sprites on single sprite sheet
PixelLevels/Visual/Sprite/Paper.png : stretched to background
PixelLevels/Visual/Sprite/Path.png : path sprites, used in example
PixelLevels/Visual/Sprite/SlimeA.png : slime used in example
PixelLevels/Visual/Sprite/TargetBack.png : target selection background
PixelLevels/Visual/Sprite/TargetFore.png : target selection foreground
PixelLevels/Visual/Additive.mat : additive blend mode sometimes looks better with transparent sprites
PixelLevels/Visual/Additive.shader : this is just the built in sprite shader copied and changed to "Blend SrcAlpha One"
PixelLevels/Visual/Free.mat : material for use with Free levels
PixelLevels/Visual/Level.mat : material for use with Level levels
PixelLevels/ReadMe.txt : this
