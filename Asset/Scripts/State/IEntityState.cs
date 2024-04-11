using dActionGame.Asset.Characters.Player;
using Godot;

namespace dActionGame.Asset.Scripts.State;

public interface IEntityState<in T> where T: Node3D
{
    public IEntityState<T> Next(T entity);
}

public class PlayerIdle : IEntityState<Player>
{
    public IEntityState<Player> Next(Player entity)
    {
        if (!entity.IsStanding) return new PlayerRun();

        return this;
    }
}

public class PlayerRun : IEntityState<Player>
{
    public IEntityState<Player> Next(Player entity)
    {
        if (entity.IsStanding) return new PlayerIdle();

        return this;
    }
}
