using Godot;
using System;

public abstract partial class PlantData : Resource {

	[Export] public PlantStage[] stages;


	public abstract void OnInteract(PlantInstance instance);
	public abstract void OnGrow(PlantInstance instance);

}
