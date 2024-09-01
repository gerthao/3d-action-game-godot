using Godot;

namespace dActionGame.Asset.Characters.Components;

public partial class VelocityComponent<T> : Node where T: Node
{
    [Export] private double  _acceleration = 5.0;
    [Export] private int     _speed        = 40;
    private          Vector3 _velocity     = Vector3.Zero;

    public void AcclelerateToPlayer()
    {
        var player = GetTree().GetFirstNodeInGroup("player") as Node3D;
    }

    private void AccelerateInDirection(Vector3 direction)
    {
        var desiredVelocity = direction * _speed;
    }
}
