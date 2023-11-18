using Godot;
using Godot.Collections;
using System;

public partial class MapManager : TileMap {

	[Export] private TileInteraction[] tileInteractions;

	public void Intereact(Vector2I cell) {
		for (int i = 0; i < tileInteractions.Length; i++) {
			for (int layer = 0; layer < this.GetLayersCount(); layer++) {

				GD.Print($"{GetCellSourceId(layer, cell)}, {GetCellAtlasCoords(layer, cell)}, {GetCellAlternativeTile(layer, cell)}");


				if (tileInteractions[i].IsTileEffected(this, cell, layer)) {

					if (tileInteractions[i].OnIntereact(this, cell, layer)) {
						return;
					}
				}
			}
		}
	}

	public void Tick(Vector2I cell) {

		
		for (int i = 0; i < tileInteractions.Length; i++) {
			for (int layer = 0; layer < this.GetLayersCount(); layer++) {
				if (tileInteractions[i].IsTileEffected(this, cell, layer)) {

					if (tileInteractions[i].OnScheduleTick(this, cell, layer)) {
						return;
					}

					
				}
			}
		}
	}

	// Optimized for whole map use
	public void TickAll() {
		

		for (int layer = 0; layer < this.GetLayersCount(); layer++) {
			for (int i = 0; i < tileInteractions.Length; i++) {
				Tile tile = tileInteractions[i].GetEffectedTile();
				Array<Vector2I> results = this.GetUsedCellsById(layer, tile.Source, tile.AtlasCoords, tile.Alternative);

				foreach (Vector2I cell in results) {
					tileInteractions[i].OnScheduleTick(this, cell, layer);
				}
			}
		}
	}

}
