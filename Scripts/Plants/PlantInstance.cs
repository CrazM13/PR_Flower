using Godot;
using System;

public partial class PlantInstance : GodotObject {

	private PlantManager manager;
	public Vector2I Position { get; set; }
	public PlantData PlantData { get; set; }

	public int CurrentStage { get; set; } = 0;

	public bool IsDirty { get; set; }

	public PlantInstance(PlantManager manager, PlantData plant, Vector2I position) {
		this.manager = manager;
		Position = position;
		PlantData = plant;
	}

	public void HarvestPlant() {
		if (PlantData.stages[CurrentStage].canHarvest) {
			Wallet.Instance.Currency++;
		}

		RemovePlant();
	}

	public void RemovePlant() {
		manager.RemovePlant(Position);
	}

}
