using Godot;
using System;

public partial class MapManager : TileMap {

	[Export] private TileInteraction[] tileInteractions;

	public void Intereact(Vector2I cell) {
		for (int i = 0; i < tileInteractions.Length; i++) {
			for (int layer = 0; layer <= this.GetLayersCount(); layer++) {
				if (tileInteractions[i].IsTileEffected(this, cell, layer)) {

					tileInteractions[i].OnIntereact(this, cell, layer);

					return;
				}
			}
		}
	}

	public void Tick(Vector2I cell) {
		for (int i = 0; i < tileInteractions.Length; i++) {
			for (int layer = 0; layer <= this.GetLayersCount(); layer++) {
				if (tileInteractions[i].IsTileEffected(this, cell, layer)) {

					tileInteractions[i].OnScheduleTick(this, cell, layer);

					return;
				}
			}
		}
	}

}
