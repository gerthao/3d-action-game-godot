using System;
using dActionGame.Asset.Characters.Player;
using Godot;

namespace dActionGame.Asset.Scripts.State;

public abstract class EntityStateHandler<T> where T: Node3D
{
    protected IEntityState<T> CurrentState;
    protected IEntityState<T> PreviousState;

    public EntityStateHandler(IEntityState<T> initialState)
    {
        CurrentState = initialState;
    }
    public abstract void Handle(double delta, T character);
}

public class PlayerEntityStateHandler : EntityStateHandler<Player>
{
    public PlayerEntityStateHandler(IEntityState<Player> initialState) : base(initialState)
    {
    }

    public override void Handle(double delta, Player character)
    {
        if (PreviousState == null)
        {
            EmitStateSignal(character, CurrentState);
        }

        var nextState = ManageState(delta, character);

        if (CurrentState.GetType() != nextState.GetType())
        {
            EmitStateSignal(character, nextState);
        }

        PreviousState = CurrentState;
        CurrentState  = nextState;
    }

    private void EmitStateSignal(Player character, IEntityState<Player> state)
    {
        switch (state)
        {
            case PlayerIdle _:
                character.EmitSignal(Player.SignalName.PlayerIdle);
                return;
            case PlayerRun _:
                character.EmitSignal(Player.SignalName.PlayerRun);
                return;
        }
    }

    private IEntityState<Player> ManageState(double delta, Player character) => CurrentState switch
    {
        PlayerIdle idle => HandlePlayerIdle(character, idle),
        PlayerRun run   => HandlePlayerRun(delta, character, run),
        _               => new PlayerIdle(),
    };

    private IEntityState<Player> HandlePlayerRun(double delta, Player character, PlayerRun run)
    {
        if (character.IsMoving)
            character.Velocity = new Vector3(
                (float)(character.Direction.X * character.Speed),
                character.Direction.Y,
                (float)(character.Direction.Z * character.Speed)
            );
        else
            character.Velocity = new Vector3(
                (float)Mathf.MoveToward(character.Velocity.X, 0, character.Speed),
                character.Direction.Y,
                (float)Mathf.MoveToward(character.Velocity.Z, 0, character.Speed)
            );

        if (character.Velocity.Length() > 0.2)
        {
            var (x, _, z) = character.Velocity;
            var lookDirection = new Vector2(z, x);
            (x, var y, z) = character.Visual.Rotation;
            character.Visual.Rotation = new Vector3(
                x,
                (float)Mathf.LerpAngle(y, lookDirection.Angle(), character.AngularSpeed * delta),
                z
            );
        }

        return run.Next(character);
    }

    private IEntityState<Player> HandlePlayerIdle(Player character, PlayerIdle idle)
    {
        return idle.Next(character);
    }
}
