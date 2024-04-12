using dActionGame.Asset.Characters.Player.States;
using dActionGame.Asset.Scripts.State;

namespace dActionGame.Asset.Characters.Player.StateHandlers;

public class PlayerMovement : EntityStateHandler<Player>
{
    public PlayerMovement(Player character, EntityState<Player> initialState) : base(character, initialState)
    {
        EmitStateSignal(CurrentState);
    }

    public override void Update(double delta)
    {
        var nextState = CurrentState.Next(delta);

        if (CurrentState.GetType() != nextState.GetType())
        {
            EmitStateSignal(nextState);
        }

        CurrentState  = nextState;
    }

    private void EmitStateSignal(EntityState<Player> state)
    {
        switch (state)
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
