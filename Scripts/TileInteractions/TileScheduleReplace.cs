using Godot;
using System;

public partial class TileScheduleReplace : TileInteraction {

	[Export] private Tile newTile;

	public override bool OnScheduleTick(TileMap tileMap, Vector2I cell, int layer) {
		base.OnScheduleTick(tileMap, cell, layer);

		if (CanTickTile(tileMap, cell, layer)) {
			tileMap.SetCell(layer, cell, newTile.Source, newTile.AtlasCoords, newTile.Alternative);
		}

		return true;
	}

	protected virtual bool CanTickTile(TileMap tileMap, Vector2I cell, int layer) {
		return true;
	}

}
