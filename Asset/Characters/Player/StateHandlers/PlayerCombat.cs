using dActionGame.Asset.Characters.Player.States;
using dActionGame.Asset.Scripts.State;

namespace dActionGame.Asset.Characters.Player.StateHandlers;

public class PlayerCombat : EntityStateHandler<Player, PlayerStateValue>
{
    private readonly IStatePool<Player, PlayerStateValue> _statePool = new PlayerStatePool();

    public PlayerCombat(Player entity, EntityState<Player, PlayerStateValue> initialState) : base(entity, initialState)
    {
    }

    public override void Update(double delta)
    {
        /*var nextState = CurrentState.Update(delta);

        if (CurrentState.GetType() != nextState.GetType()) EmitStateSignal(nextState);

        CurrentState = nextState;*/
    }

    private void EmitStateSignal(EntityState<Player, PlayerStateValue> state)
    {
        switch (state)
        {
            case PlayerAttack:
                Entity.EmitSignal(Player.SignalName.PlayerAttack);
                break;
        }
    }
}
