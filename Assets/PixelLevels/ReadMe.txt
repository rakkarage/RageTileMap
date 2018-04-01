PixelEffects
art: benmhenry@gmail.com
code: davidahenry@gmail.com

Description:


Dozens of dungeon tilesets and props, also includes 1 overworld village tileset!

Includes a simple tile map system with:
- Mini map.
- Path finding.
- Line of sight lighting.
- Pan and snap, pinch and zoom.

https://github.com/rakkarage/RageTileMap

http://gfycat.com/ActualCrazyAtlanticblackgoby


Includes thirty five animated pixel effects.<br>
Includes: Flame, Selection, Sick, Sleep, Path, Block, Box, Bubble, Circle, Claw, Consume, Dark, Earth, Electric, Explode, Fire, Footprints, Glint, Heal, Ice, Lightning, Nuclear, Poison, Puff, Shield, Slash, Sparks, Splatter, Square.<br>
Also includes simple animated slime.

Documentation:
0. Use Effect.prefab or:
1. Create empty game object, add Animator, select an effect animation.
2. Create child (sprite or image) Offset with two children (sprite or image) Fore and Back.
3. Set Additive mat on sprite renderer if want.
4. You can read the recommended offset positions and additive mode from included data files as done in example.

Files:
PixelEffects/Audio/PinDrop.wav : used for example scene intro
PixelEffects/Data/Block.asset (etc) : positioning and blending data for all animations (ya probably a better way)
PixelEffects/Prefab/Effect.prefab : can be used to instantiate any effect
PixelEffects/Prefab/Path.prefab : can be used to instantiate any simple looping effect
PixelEffects/Scene/Effect.unity : example scene
PixelEffects/Scene/Effects.unity : example scene
PixelEffects/Scene/Intro.unity : example scene intro
PixelEffects/Script/Ease.cs : simple ease system used for bounce in example scene intro
PixelEffects/Script/Effects.cs : used to manage and pool and spawn effects
PixelEffects/Script/Effect1.cs (etc) : groups a few similar effects together on each mob for example scene
PixelEffects/Script/Intro.cs : used for example scene intro
PixelEffects/Script/ModelEffectAnimation.ca : ScriptableObject used to hold some position and blend data about animations
PixelEffects/Script/Pool.cs : used to pool effects or anything
PixelEffects/Script/StateFlip.cs : used in animation controllers to flip some effects randomly sometimes
PixelEffects/Script/StateRandom.cs : used in animation controllers to select random animation
PixelEffects/Script/StateRemove.cs : used to pool objects when done
PixelEffects/Visual/Animation/Effects/Block.controller (etc) : effect animation controllers
PixelEffects/Visual/Animation/Slime.controller: slime animation controller used in example
PixelEffects/Visual/Font/SuperBlack.fontsettings (etc) : fonts used in example
PixelEffects/Visual/Sprite/Effects/Block.png (etc) : all sprites on single sprite sheet
PixelEffects/Visual/Sprite/Henry.png : used in intro
PixelEffects/Visual/Sprite/SlimeA.png : slime used in example
PixelEffects/ReadMe.txt : this
