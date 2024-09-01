using Godot;

namespace dActionGame.Asset.Characters.Player.NodeStates;

public partial class Attack : State
{
    [Export] private string _animation;

    public override StateType StateType => StateType.Attack;

    public override void Update(double delta)
    {
        if (Player.AnimationPlayer.CurrentAnimation == _animation
            && Player.AnimationPlayer.IsPlaying()) return;

        Player.HitBox.Disabled = true;
        StateMachine.ChangeState(StateType.Idle);
    }

    public override void Enter()
    {
        AnimationPlayer.Play(_animation);

        Player.Velocity = Player.Velocity with
        {
            X = 0,
            Z = 0,
        };

        Player.HitBox.Disabled = false;
    }

    public override void Exit()
    {
    }
}
