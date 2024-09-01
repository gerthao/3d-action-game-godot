using dActionGame.Asset.Scripts.Interfaces;
using Godot;

namespace dActionGame.Asset.Characters.Player;

public partial class Player : CharacterBody3D, IHasAnimation
{
    [Signal]
    public delegate void CoinNumberUpdatedEventHandler(int value);


    private int _coins;

    private double _stopDistance = 2.2;

    public Vector3        Direction;
    public GpuParticles3D FootStepVfx;

    public Node3D Visual;

    public bool             IsStanding   => Direction.IsZeroApprox();
    public bool             IsMoving     => !Direction.IsZeroApprox();
    public bool             CanAttack    => Input.IsActionJustPressed("attack");
    public bool             CanSlide     => Input.IsActionJustPressed("slide");
    public CollisionShape3D HitBox       { get; private set; }
    public double           Speed        => 5.0;
    public double           AngularSpeed => 7.0;

    private int Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            EmitSignal(SignalName.CoinNumberUpdated, _coins);
        }
    }

    public AnimationPlayer AnimationPlayer { get; set; }


    public override void _Ready()
    {
        Visual          = GetNode<Node3D>("Visual");
        FootStepVfx     = GetNode<GpuParticles3D>("Visual/VFX/Footstep_GPUParticles3D");
        AnimationPlayer = GetNode<AnimationPlayer>("Visual/AnimationPlayer");
        HitBox          = GetNode<CollisionShape3D>("HitBoxComponent/HitBox/CollisionShape3D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Direction = GetInputDirection();
        MoveAndSlide();
    }

    public Vector3 GetInputDirection()
    {
        var inputDirection = Input.GetVector(
            "move_left",
            "move_right",
            "move_up",
            "move_down"
        ).Rotated(-45);

        return (Transform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y))
            .Normalized();
    }

    public void AddCoin(int value)
    {
        Coins += value;
    }
}
