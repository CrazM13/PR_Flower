using Godot;
using System;

public partial class TileScheduleIfWatered : TileScheduleReplace {

	protected override bool CanTickTile(TileMap tileMap, Vector2I cell, int layer) {
		
		TileData tileData = tileMap.GetCellTileData(layer, cell);

		if (tileData != null && !tileData.GetCustomData("CanWater").AsBool()) {
			return true;
		}
		
		return false;
	}

}
