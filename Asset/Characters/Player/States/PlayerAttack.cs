using System;
using dActionGame.Asset.Scripts.State;

namespace dActionGame.Asset.Characters.Player.States;

public class PlayerAttack : EntityState<Player, PlayerStateValue>
{
    public PlayerAttack(Player entity) : base(entity)
    {
    }

    public override PlayerStateValue Update(double delta) => throw new NotImplementedException();
}
