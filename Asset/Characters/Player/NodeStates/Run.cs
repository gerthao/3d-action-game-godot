using Godot;

namespace dActionGame.Asset.Characters.Player.NodeStates;

public partial class Run : State
{
    [Export] private string       _animation;
    private          StateMachine _stateMachine;

    public override StateType StateType              => StateType.Run;
    private         bool      MeetsVelocityThreshold => Player.Velocity.Length() > 0.2;


    public override void Update(double delta)
    {
        HandlePlayer(delta);

        var nextState = Player switch
        {
            { IsMoving: false } => StateType.Idle,
            { CanSlide: true }  => StateType.Slide,
            { CanAttack: true } => StateType.Attack,
            _                   => StateType.Run,
        };

        if (nextState != StateType.Run)
            StateMachine.ChangeState(nextState);
    }
    public override void Enter()
    {
        Player.AnimationPlayer.Play(_animation);
    }
    public override void Exit()
    {
    }

    private void HandlePlayer(double delta)
    {
        Player.Velocity = Player.IsMoving
            ? Player.Velocity with
            {
                X = (float)(Player.Direction.X * Player.Speed),
                Z = (float)(Player.Direction.Z * Player.Speed),
            }
            : Player.Velocity with
            {
                X = (float)Mathf.MoveToward(Player.Velocity.X, 0, Player.Speed),
                Z = (float)Mathf.MoveToward(Player.Velocity.Z, 0, Player.Speed),
            };


        if (!MeetsVelocityThreshold) return;

        var lookDirection = new Vector2(Player.Velocity.Z, Player.Velocity.X);

        Player.Visual.Rotation = Player.Visual.Rotation with
        {
            Y = (float)Mathf.LerpAngle(Player.Visual.Rotation.Y, lookDirection.Angle(), Player.AngularSpeed * delta),
        };
    }
}
