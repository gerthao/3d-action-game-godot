using dActionGame.Asset.Characters.Player;
using Godot;

namespace dActionGame.Asset.GameObjects.CollectableCoin;

public partial class CollectableCoin : Node3D
{
    public const int             Value = 1;
    public       AnimationPlayer Animation;
    public       Area3D          Area3D;
    public       GpuParticles3D  PickupVfx;
    public       double          RotationSpeed = 1;
    public       Node3D          Visual;

    public void OnArea3dBodyEntered(Node3D body)
    {
        if (body is Player player)
        {
            GD.Print("Dafuck you think you doin");
            PickupVfx.Emitting = true;
            Animation.Play("collected");
            RotationSpeed = 10.0;
            player.AddCoin(Value);
        }
    }

    public override void _Ready()
    {
        Visual    = GetNode<Node3D>("Visual");
        Animation = GetNode<AnimationPlayer>("Visual/AnimationPlayer");
        PickupVfx = GetNode<GpuParticles3D>("VFXNode");
        Area3D    = GetNode<Area3D>("Area3D");

        Area3D.BodyEntered += OnArea3dBodyEntered;
    }

    public override void _Process(double delta)
    {
        Visual.RotateY((float)(RotationSpeed * delta));

        if (!Visual.Visible && !PickupVfx.Emitting) QueueFree();
    }
}
