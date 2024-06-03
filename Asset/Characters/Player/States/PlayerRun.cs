using dActionGame.Asset.Scripts.State;
using Godot;

namespace dActionGame.Asset.Characters.Player.States;

public class PlayerRun : EntityState<Player, PlayerStateValue>
{

    public PlayerRun(Player entity) : base(entity)
    {
        GD.Print("Player is running...");
    }
    private void HandlePlayer(double delta)
    {
        Entity.Velocity = Entity.IsMoving
            ? new Vector3(
                (float)(Entity.Direction.X * Entity.Speed),
                Entity.Direction.Y,
                (float)(Entity.Direction.Z * Entity.Speed)
            )
            : new Vector3(
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
    }

    public override PlayerStateValue Update(double delta)
    {
        HandlePlayer(delta);

        if (!Entity.IsMoving) return PlayerStateValue.Idle;
        if (Entity.CanSlide) return PlayerStateValue.Slide;

        return PlayerStateValue.Run;
    }
}
