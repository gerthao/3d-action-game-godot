using dActionGame.Asset.Characters.Player;
using Godot;

namespace dActionGame.Asset.Scripts.State;

public abstract class EntityState<T> where T: Node3D
{
    protected       T               Entity;
    public abstract EntityState<T> Next(double delta);

    public EntityState(T entity)
    {
        Entity = entity;
    }
}
