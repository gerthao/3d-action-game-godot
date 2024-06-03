using Godot;

namespace dActionGame.Asset.Scripts.State;

public interface IStatePool<TEntity, TStateTypeEnum> where TEntity: Node3D
{
    EntityState<TEntity, TStateTypeEnum> GetState(TEntity entity, TStateTypeEnum type);
}
