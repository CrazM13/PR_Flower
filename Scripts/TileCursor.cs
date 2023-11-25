using Godot;
using System;

public partial class TileCursor : AnimatedSprite2D {

	[Export] private PlayerMovement player;
	[Export] private PlantManager tileMap;

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
		}

		bool canInteract = CanInteract();

		this.Modulate = Color.FromHsv(0, 1, 1, canInteract ? 1 : 0.5f);

		if (canInteract && Input.IsActionJustPressed("action_interact")) {
			Vector2I cell = tileMap.LocalToMap(this.Position);

			if (tileMap.IsPlantAt(cell)) tileMap.InteractWithPlant(cell);
			else {
				tileMap.AddPlant(Wallet.Instance.SeedPouch.GetNextSeed(), cell);
			}
		}

	}

	public bool CanInteract() {
		Vector2I cell = tileMap.LocalToMap(this.Position);

		if (tileMap.IsPlantAt(cell)) {
			return true;
		} else {
			TileData data = tileMap.GetCellTileData(1, cell);
			if (data != null) {
				bool canPlant = data.GetCustomData("CanPlant").AsBool();

				return canPlant;
			}
		}

		return false;
 
	}

}
