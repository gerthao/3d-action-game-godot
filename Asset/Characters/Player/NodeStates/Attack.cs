using Godot;

namespace dActionGame.Asset.Characters.Player.NodeStates;

public partial class Attack : State
{
    [Export] private string          _animation;
    [Export] private AnimationPlayer _vfxAnimationPlayer;
    [Export] private Node3D          _vfxEffect;

    private int _damage = 25;

    public override StateType StateType => StateType.Attack;

    public override void Update(double delta)
    {
        if (Player.AnimationPlayer.CurrentAnimation == _animation
            && Player.AnimationPlayer.IsPlaying()) return;

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

        _vfxEffect.Visible = true;
        _vfxAnimationPlayer.Stop();
        _vfxAnimationPlayer.Play("play_blade_vfx");
    }

    public override void Exit()
    {
        DisableHitBox();
    }

    public void EnableHitBox()
    {
        Player.HitBox.Disabled = false;
    }

    public void DisableHitBox()
    {
        Player.HitBox.Disabled = true;
    }

    private void OnHitBoxBodyEntered(Node3D body)
    {
        if (body.IsInGroup("enemy") && body is Enemy.Enemy enemy)
        {
            enemy.ApplyDamage(_damage);
        }
    }
}
