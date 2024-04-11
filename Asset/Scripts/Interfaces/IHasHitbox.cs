using dActionGame.Asset.Scripts.Components;

namespace dActionGame.Asset.Scripts.Interfaces;

public interface IHasHitbox
{
    public HitboxComponent HitboxComponent { get; protected set; }
}
