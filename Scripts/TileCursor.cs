using Godot;
using System;

public partial class TileCursor : AnimatedSprite2D {

	[Export] private PlayerMovement player;
	[Export] private MapManager tileMap;

	public override void _Ready() {
		base._Ready();

		this.Play("default");
	}

	public override void _Process(double delta) {

		Vector2I direction = player.Direction;
		if (direction != Vector2I.Zero) {
			Vector2 roughPos = player.Position;
			Vector2 position = tileMap.MapToLocal(tileMap.LocalToMap(roughPos) + direction);

			this.Position = position;

			this.Modulate = Color.FromHsv(0, 1, 1, CanInteract() ? 1 : 0.5f);
		}

		if (Input.IsActionJustPressed("action_interact")) {
			tileMap.Intereact(tileMap.LocalToMap(this.Position));
		}

	}

	public bool CanInteract() {
		Vector2I cell = tileMap.LocalToMap(this.Position);

		TileData data = tileMap.GetCellTileData(0, cell);
		if (data != null) {
			bool canPlant = data.GetCustomData("CanPlant").AsBool();
			bool canHarvest = data.GetCustomData("CanHarvest").AsBool();
			bool canWater = data.GetCustomData("CanWater").AsBool();

			return canPlant || canWater || canHarvest;
		}

		return false;
 
	}

}
