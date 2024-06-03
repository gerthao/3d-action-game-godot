using dActionGame.Asset.Characters.Player.States;
using dActionGame.Asset.Scripts.State;

namespace dActionGame.Asset.Characters.Player.StateHandlers;

public class PlayerMovement : EntityStateHandler<Player, PlayerStateValue>
{
    private readonly IStatePool<Player, PlayerStateValue> _statePool = new PlayerStatePool();

    public PlayerMovement(Player character, EntityState<Player, PlayerStateValue> initialState) : base(
        character,
        initialState
    )
    {
        EmitStateSignal();
    }

    public override void Update(double delta)
    {
        var nextState = CurrentState.Update(delta);
        CurrentState = _statePool.GetState(Entity, nextState);

        EmitStateSignal();
    }

    private void EmitStateSignal()
    {
        switch (CurrentState)
        {
            case PlayerIdle:
                Entity.EmitSignal(Player.SignalName.PlayerIdle);
                break;
            case PlayerRun:
                Entity.EmitSignal(Player.SignalName.PlayerRun);
                break;
            case PlayerSlide:
                Entity.EmitSignal(Player.SignalName.PlayerSlide);
                break;
        }
    }
}
