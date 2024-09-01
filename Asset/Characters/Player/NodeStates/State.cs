using Godot;

namespace dActionGame.Asset.Characters.Player.NodeStates;

public abstract partial class State : Node
{
    public AnimationPlayer AnimationPlayer;
    public Player          Player;

    public StateMachine StateMachine { get; internal set; }

    public abstract StateType StateType { get; }

    public abstract void Update(double delta);
    public abstract void Enter();
    public abstract void Exit();
}
