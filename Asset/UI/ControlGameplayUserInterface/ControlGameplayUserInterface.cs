using dActionGame.Asset.Characters.Player;
using Godot;

namespace dActionGame.Asset.UI.ControlGameplayUserInterface;
public partial class ControlGameplayUserInterface : Control
{
	private          Label  _coinLabel;
	[Export] private Player _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_coinLabel = GetNode<Label>("CoinHBoxContainer/CoinLabel");
		_player.CoinNumberUpdated += UpdateCoinLabel;
	}

	private void UpdateCoinLabel(int value)
	{
		_coinLabel.Text = value.ToString();
	}
}
