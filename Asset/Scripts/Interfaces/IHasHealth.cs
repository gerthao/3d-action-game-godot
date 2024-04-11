using dActionGame.Asset.Scripts.Components;

namespace dActionGame.Asset.Scripts.Interfaces;

public interface IHasHealth
{
    public HealthComponent HealthComponent { get; protected set; }
}
