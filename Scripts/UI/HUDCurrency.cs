using Godot;
using System;

public partial class HUDCurrency : Label {

	[Export] private int currencyDisplayMinLength = 2;

	public override void _Process(double delta) {
		base._Process(delta);


		Text = Wallet.Instance.Currency.ToString($"D{currencyDisplayMinLength}");
	}

}
