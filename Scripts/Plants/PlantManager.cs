using Godot;
using Godot.Collections;
using System;

public partial class PlantManager : TileMap {

	private Dictionary<Vector2I, PlantInstance> plants = new Dictionary<Vector2I, PlantInstance>();

	public override void _Process(double delta) {
		UpdateTiles();

		base._Process(delta);
	}

	public void AddPlant(PlantData plantData, Vector2I position) {
		if (plants.ContainsKey(position)) return;

		PlantInstance newPlant = new PlantInstance(this, plantData, position);
		plants.Add(position, newPlant);


		newPlant.IsDirty = true;
	}

	public void RemovePlant(Vector2I position) {
		EraseCell(2, position);
		plants.Remove(position);
	}

	public void UpdateTiles() {
		foreach (PlantInstance plant in plants.Values) {
			if (plant.IsDirty) {
				PlantStage stage = plant.PlantData.stages[plant.CurrentStage];
				SetCell(2, plant.Position, stage.TileSource, stage.TileAtlasCoords, stage.TileAlternative);
				plant.IsDirty = false;
			}
		}
	}

	public bool IsPlantAt(Vector2I position) {
		return plants.ContainsKey(position);
	}

	public PlayerAnimator.AnimationEvent InteractWithPlant(Vector2I position) {
		if (plants.ContainsKey(position)) {

			PlantData plantData = plants[position].PlantData;

			string animName = plantData.stages[plants[position].CurrentStage].playerActionAnim;
			PlayerAnimator.AnimationEvent anim = string.IsNullOrEmpty(animName) ? null : new PlayerAnimator.AnimationEvent(0.5f, animName);

			plantData.OnInteract(plants[position]);

			return anim;
		}

		return null;
	}

	public void GrowPlant(Vector2I position) {
		if (plants.ContainsKey(position)) {
			plants[position].PlantData.OnGrow(plants[position]);
		}
	}

	public void GrowAll() {
		foreach (PlantInstance plant in plants.Values) {
			plant.PlantData.OnGrow(plant);
		}
	}

}
