using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class SeedPouch {

	private Queue<PlantData> seedQueue;
	private PlantData defaultPlant;

	public SeedPouch() {
		seedQueue = new Queue<PlantData>();

		defaultPlant = ResourceLoader.Load<PlantData>("res://Data/Plants/BasicFlower.tres");
	}

	public PlantData GetNextSeed() {
		if (seedQueue.Count > 0) {
			return seedQueue.Dequeue();
		}

		return defaultPlant;
	}

}
