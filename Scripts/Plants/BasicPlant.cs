using Godot;
using System;

public partial class BasicPlant : PlantData {

	public override void OnGrow(PlantInstance instance) {
		if (instance.PlantData.stages[instance.CurrentStage].canGrow) {
			instance.CurrentStage++;
			instance.IsDirty = true;
		}
	}

	public override void OnInteract(PlantInstance instance) {
		if (instance.PlantData.stages[instance.CurrentStage].canWater) {
			instance.CurrentStage++;
			instance.IsDirty = true;
		} else if (instance.PlantData.stages[instance.CurrentStage].canHarvest) {
			instance.HarvestPlant();
		}
	}
}
