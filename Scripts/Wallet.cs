using Godot;
using System;

public class Wallet {

	private static Wallet _instance;

	public static Wallet Instance {
		get {
			if (_instance != null) return _instance;
			_instance = new Wallet();
			return _instance;
		}
	}

	public int Currency { get; set; }
	public SeedPouch SeedPouch { get; set; } = new SeedPouch();

}
