using dActionGame.Asset.Scripts.State;
using Godot;

namespace dActionGame.Asset.Characters.Player.States;

public class PlayerIdle : EntityState<Player, PlayerStateValue>
{
    public PlayerIdle(Player entity) : base(entity)
    {
        GD.Print("Player is idle...");
    }

    public override PlayerStateValue Update(double delta) =>
        Entity.IsMoving ? PlayerStateValue.Run :
        Entity.CanSlide ? PlayerStateValue.Slide :
        Entity.CanAttack ? PlayerStateValue.Attack :
        PlayerStateValue.Idle;
}
