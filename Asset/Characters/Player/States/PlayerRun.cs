using dActionGame.Asset.Scripts.State;
using Godot;

namespace dActionGame.Asset.Characters.Player.States;

public class PlayerRun : EntityState<Player>
{
    public override EntityState<Player> Next(double delta)
    {
        if (Entity.IsMoving)
            Entity.Velocity = new Vector3(
                (float)(Entity.Direction.X * Entity.Speed),
                Entity.Direction.Y,
                (float)(Entity.Direction.Z * Entity.Speed)
            );
        else
            Entity.Velocity = new Vector3(
                (float)Mathf.MoveToward(Entity.Velocity.X, 0, Entity.Speed),
                Entity.Direction.Y,
                (float)Mathf.MoveToward(Entity.Velocity.Z, 0, Entity.Speed)
            );

        if (Entity.Velocity.Length() > 0.2)
        {
            var (x, _, z) = Entity.Velocity;
            var lookDirection = new Vector2(z, x);
            (x, var y, z) = Entity.Visual.Rotation;
            Entity.Visual.Rotation = new Vector3(
                x,
                (float)Mathf.LerpAngle(y, lookDirection.Angle(), Entity.AngularSpeed * delta),
                z
            );
        }

        if (Entity.IsStanding) return new PlayerIdle(Entity);
        if (Entity.CanSlide) return new PlayerSlide(Entity);

        return this;
    }

    public PlayerRun(Player entity) : base(entity)
    {
    }
}
