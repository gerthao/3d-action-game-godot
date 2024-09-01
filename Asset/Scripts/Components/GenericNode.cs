using Godot;

namespace dActionGame.Asset.Scripts.Components;

public partial class GenericNode<T>: Node where T: Node
{
    public override void _Ready()
    {
        base._Ready();
    }
}
