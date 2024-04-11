using Godot;

namespace dActionGame.Asset.Characters.Enemy;

public partial class Enemy : CharacterBody3D
{
    private const double Speed = 2.0;

    private AnimationPlayer   _animationPlayer;
    private Vector3           _direction;
    private NavigationAgent3D _navigationAgent;
    private Node3D            _player;
    private double            _stopDistance = 2.2;
    private Node3D            _visual;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _visual          = GetNode<Node3D>("Visual");
        _animationPlayer = GetNode<AnimationPlayer>("Visual/AnimationPlayer");
        _navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
        _player          = GetTree().GetFirstNodeInGroup("player") as Node3D;
    }

    public override void _PhysicsProcess(double delta)
    {
        _navigationAgent.TargetPosition = _player.GlobalPosition;
        _direction                      = (_navigationAgent.GetNextPathPosition() - GlobalPosition).Normalized();

        if (IsCloseToPlayer())
        {
            _animationPlayer.Play("NPC_01_IDLE");
            return;
        }

        Velocity = Velocity.Lerp(_direction * (float)Speed, (float)delta);
        _animationPlayer.Play("NPC_01_WALK");

        if (!IsMoving()) return;

        var lookDirection = new Vector2(Velocity.Z, Velocity.X);
        _visual.RotateY(lookDirection.Angle());
    }

    private bool IsCloseToPlayer() => _navigationAgent.DistanceToTarget() < _stopDistance;

    private bool IsMoving() => Velocity.Length() > 0.2;
}