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

    private void WaitForMap(Rid rid)
    {
        SetPhysicsProcess(true);
        NavigationServer3D.MapChanged -= WaitForMap;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetPhysicsProcess(false);
        _visual          = GetNode<Node3D>("Visual");
        _animationPlayer = GetNode<AnimationPlayer>("Visual/AnimationPlayer");
        _navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
        _player          = GetTree().GetFirstNodeInGroup("player") as Node3D;

        NavigationServer3D.MapChanged += WaitForMap;
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

        if (IsMoving())
        {
            var lookDirection = new Vector2(Velocity.Z, Velocity.X);
            _visual.Rotation = new Vector3(_visual.Rotation.X, lookDirection.Angle(), _visual.Rotation.Z);
        }

        MoveAndSlide();
    }

    private bool IsCloseToPlayer() => _navigationAgent.DistanceToTarget() < _stopDistance;

    private bool IsMoving() => Velocity.Length() > 0.2;
}
