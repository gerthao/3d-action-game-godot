using Godot;

namespace dActionGame.Asset.Scripts.Components;

public abstract class VisualComponent<T> where T : Node3D
{
    protected T Entity;

    public VisualComponent(T entity) => Entity = entity;
}
