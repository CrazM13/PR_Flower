using Godot;
using System;

public partial class TileInteraction : Resource {

	[Export] private Tile effectedTile;

	public virtual bool OnIntereact(TileMap tileMap, Vector2I cell, int layer) { return false; }

	public virtual bool OnScheduleTick(TileMap tileMap, Vector2I cell, int layer) { return false; }

	public bool IsTileEffected(TileMap tileMap, Vector2I cell, int layer) {
		return effectedTile.MatchesTileMap(tileMap, layer, cell);
	}

	public Tile GetEffectedTile() { return effectedTile; }

}
