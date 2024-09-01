using System;
using dActionGame.Asset.Scripts.State;
using Godot;

namespace dActionGame.Asset.Characters.Player.States;

public class PlayerAttack : EntityState<Player, PlayerStateValue>
{
    public PlayerAttack(Player entity) : base(entity)
    {
        GD.Print("Player is attacking...");
        Entity.Velocity        = new Vector3(0, entity.Velocity.Y, 0);
        Entity.HitBox.Disabled = false;
    }

    public override PlayerStateValue Update(double delta)
    {
        if (Entity.AnimationPlayer.IsPlaying()) return PlayerStateValue.Attack;

        Entity.HitBox.Disabled = true;
        return PlayerStateValue.Idle;
    }

    public override void Reset(Player entity)
    {
        base.Reset(entity);
        Entity.Velocity        = new Vector3(0, entity.Velocity.Y, 0);
        Entity.HitBox.Disabled = false;
    }
}
