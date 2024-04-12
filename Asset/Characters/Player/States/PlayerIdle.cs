using dActionGame.Asset.Scripts.State;

namespace dActionGame.Asset.Characters.Player.States;

public class PlayerIdle : EntityState<Player>
{
    public override EntityState<Player> Next(double delta)
    {
        if (!Entity.IsStanding) return new PlayerRun(Entity);
        if (Entity.CanSlide) return new PlayerSlide(Entity);

        return this;
    }

    public PlayerIdle(Player entity) : base(entity)
    {
    }
}
