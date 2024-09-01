using Godot;

namespace dActionGame.Asset.Characters.Player.NodeStates;

public partial class Slide : State
{
    [Export] private string    _animation;
    private          Vector3   _direction;
    private          double    _duration = 0.4;
    private          double    _remainingDuration;
    private          int       _speed = 700;
    public override  StateType StateType  => StateType.Slide;
    private          bool      IsFinished => _remainingDuration < 0;

    public override void Update(double delta)
    {
        _remainingDuration -= delta;
        _direction         =  Player.Visual.Transform.Basis.Z;

        Player.Velocity = IsFinished
            ? Vector3.Zero
            : Player.Velocity with
            {
                X = (float)(_direction.X * _speed * delta),
                Z = (float)(_direction.Z * _speed * delta),
            };

        if (IsFinished) StateMachine.ChangeState(StateType.Idle);
    }

    public override void Enter()
    {
        AnimationPlayer.Play(_animation);
        Player.Velocity    = Vector3.Zero;
        _remainingDuration = _duration;
    }

    public override void Exit()
    {
    }
}
