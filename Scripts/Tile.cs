using Godot;
using System;

public partial class Tile : Resource {

	[Export] public int Source { get; set; }
	[Export] public Vector2I AtlasCoords { get; set; }
	[Export] public int Alternative { get; set; }


	public bool MatchesTileMap(TileMap tileMap, int layer, Vector2I cell) {
		return tileMap.GetCellSourceId(layer, cell) == Source && tileMap.GetCellAtlasCoords(layer, cell) == AtlasCoords && tileMap.GetCellAlternativeTile(layer, cell) == Alternative;
	}

}
