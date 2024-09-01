using Godot;

namespace dActionGame.Asset.Scripts.State;

public abstract class EntityStateHandler<T, TStateTypes> where T: Node3D
{
    protected          EntityState<T, TStateTypes> CurrentState;
    protected readonly T                           Entity;

    protected EntityStateHandler(T entity, EntityState<T, TStateTypes> initialState)
    {
        Entity       = entity;
        CurrentState = initialState;
    }
    public abstract void Update(double delta);
}
