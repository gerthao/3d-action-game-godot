using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace dActionGame.Asset.Characters.Player.NodeStates;

public partial class StateMachine : Node
{
    [Signal]
    public delegate void PlayerStateChangedEventHandler(StateType stateType);

    [Export] private AnimationPlayer _animationPlayer;
    [Export] private State           _currentState;
    [Export] private Player          _player;

    private readonly Dictionary<StateType, State> _states = new();

    public override void _Ready()
    {
        foreach (StateType stateType in Enum.GetValues(typeof(StateType)))
        {
            var stateNode = GetNodeOrNull<State>(stateType.ToString());
            if (stateNode == null) continue;

            InitializeState(stateNode);

            _states.Add(stateType, stateNode);
        }

        _currentState.Enter();
    }
    private State InitializeState(State state)
    {
        state.Player          = _player;
        state.AnimationPlayer = _animationPlayer;
        state.StateMachine    = this;

        return state;
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState.Update(delta);
    }

    public void ChangeState(StateType stateType)
    {
        if (_currentState == null || _currentState.StateType == stateType) return;

        _currentState.Exit();
        _currentState = _states[stateType];
        _currentState.Enter();

        EmitSignal(SignalName.PlayerStateChanged, (int) stateType);
    }
}
