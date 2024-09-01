using Godot;

namespace dActionGame.Asset.Characters.Player.NodeStates;

public partial class Idle : State
{
    [Export] private string       _animation;
    public override StateType StateType => StateType.Idle;

    public override void Update(double delta)
    {
        var nextState = Player switch
        {
            { IsMoving: true }  => StateType.Run,
            { CanSlide: true }  => StateType.Slide,
            { CanAttack: true } => StateType.Attack,
            _                   => StateType.Idle,
        };

        if (nextState != StateType.Idle)
            StateMachine.ChangeState(nextState);
    }
    public override void Enter()
    {
        AnimationPlayer.Play(_animation);
    }

    public override void Exit()
    {
    }
}
