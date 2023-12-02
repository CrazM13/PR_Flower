using Godot;
using System;

public partial class PlantStage : Resource {

	[Export] public bool canWater;
	[Export] public bool canGrow;
	[Export] public bool canHarvest;

	[Export] public string playerActionAnim;

	[Export] public int TileSource { get; set; }
	[Export] public Vector2I TileAtlasCoords { get; set; }
	[Export] public int TileAlternative { get; set; }

}
