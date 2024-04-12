using dActionGame.Asset.Scripts.State;

namespace dActionGame.Asset.Characters.Player.StateHandlers;

public class PlayerCombat: EntityStateHandler<Player>
{
    public PlayerCombat(Player entity, EntityState<Player> initialState) : base(entity, initialState)
    {
    }

    public override void Update(double delta)
    {
        throw new System.NotImplementedException();
    }
}
