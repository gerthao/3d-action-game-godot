using Godot;

namespace dActionGame.Asset.Scripts.Components;

public partial class HurtBoxComponent : Area3D
{
    [Signal]
    public delegate void HitEventHandler();

    [Export] protected HealthComponent HealthComponent;

    public override void _Ready()
    {
        base._Ready();
        AreaEntered += OnAreaEntered;
    }

    public void OnAreaEntered(Area3D other)
    {
    }
}
