using Godot;

namespace dActionGame.Asset.Scripts.Interfaces;

public interface IHasAnimation
{
    public AnimationPlayer AnimationPlayer { get; protected set; }
}
