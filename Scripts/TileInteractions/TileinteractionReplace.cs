using Godot;
using System;

public partial class TileinteractionReplace : TileInteraction {

	[Export] private Tile newTile;

	public override bool OnIntereact(TileMap tileMap, Vector2I cell, int layer) {
		base.OnIntereact(tileMap, cell, layer);

		tileMap.SetCell(layer, cell, newTile.Source, newTile.AtlasCoords, newTile.Alternative);

		return true;
	}

}
