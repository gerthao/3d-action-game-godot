using Godot;

namespace dActionGame.Asset.Scripts.State;

public abstract class EntityStateHandler<T> where T: Node3D
{
    protected T              Entity;
    protected EntityState<T> CurrentState;

    public EntityStateHandler(T entity, EntityState<T> initialState)
    {
        Entity       = entity;
        CurrentState = initialState;
    }
    public abstract void Update(double delta);
}
