using dActionGame.Asset.Scripts.State;
using Godot;

namespace dActionGame.Asset.Characters.Player.States;

public class PlayerSlide: EntityState<Player>
{
    public int     Speed    => 700;
    public double  Duration => 0.4;
    public double  RemainingDuration;
    public Vector3 Direction;

    public PlayerSlide(Player entity) : base(entity)
    {
        entity.Velocity   = Vector3.Zero;
        RemainingDuration = Duration;
    }

    public override EntityState<Player> Next(double delta)
    {
        RemainingDuration -= delta;
        Direction         =  Entity.Visual.Transform.Basis.Z;

        if (RemainingDuration > 0)
        {
            Entity.Velocity = new Vector3(
                (float) (Direction.X * Speed * delta),
                0,
                (float) (Direction.Z * Speed * delta)
            );

            return this;
        }

        Entity.Velocity = Vector3.Zero;

        return new PlayerIdle(Entity);
    }
}
