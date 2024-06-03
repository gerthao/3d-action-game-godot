using dActionGame.Asset.Scripts.State;
using Godot;

namespace dActionGame.Asset.Characters.Player.States;

public class PlayerSlide : EntityState<Player, PlayerStateValue>
{
    private Vector3 _direction;
    private double  _remainingDuration;

    public PlayerSlide(Player entity) : base(entity)
    {
        GD.Print("Player is sliding...");
        entity.Velocity    = Vector3.Zero;
        _remainingDuration = Duration;
    }

    public int    Speed    => 700;
    public double Duration => 0.4;

    public void HandlePlayer(double delta)
    {
        _remainingDuration -= delta;
        _direction         =  Entity.Visual.Transform.Basis.Z;

        Entity.Velocity = IsFinished()
            ? new Vector3(
                (float)(_direction.X * Speed * delta),
                0,
                (float)(_direction.Z * Speed * delta)
            )
            : Vector3.Zero;
    }

    public override PlayerStateValue Update(double delta)
    {
        HandlePlayer(delta);

        return IsFinished() ? PlayerStateValue.Slide : PlayerStateValue.Idle;
    }

    public override void Reset(Player entity)
    {
        if (IsFinished()) return;

        Entity             = entity;
        Entity.Velocity    = Vector3.Zero;
        _remainingDuration = Duration;
    }

    private bool IsFinished() => _remainingDuration > 0;
}
