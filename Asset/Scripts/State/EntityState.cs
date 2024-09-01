using Godot;

namespace dActionGame.Asset.Scripts.State;

public abstract class EntityState<T, TStateType> where T: Node3D
{
    protected T Entity;

    protected EntityState(T entity) => Entity = entity;
    public abstract TStateType Update(double delta);

    public virtual void Reset(T entity)
    {
        Entity = entity;
    }
}
