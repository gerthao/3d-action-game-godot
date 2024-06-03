using dActionGame.Asset.Scripts.State;

namespace dActionGame.Asset.Characters.Player.States;

public class PlayerStatePool : IStatePool<Player, PlayerStateValue>
{
    private static PlayerIdle  _playerIdle;
    private static PlayerRun   _playerRun;
    private static PlayerSlide _playerSlide;

    public EntityState<Player, PlayerStateValue> GetState(Player entity, PlayerStateValue type) =>
        type switch
        {
            PlayerStateValue.Idle  => GetPlayerIdle(entity),
            PlayerStateValue.Run   => GetPlayerRun(entity),
            PlayerStateValue.Slide => GetPlayerSlide(entity),
            _                      => GetPlayerIdle(entity),
        };

    private PlayerIdle GetPlayerIdle(Player player)
    {
        if (_playerIdle == null) _playerIdle = new PlayerIdle(player);
        else _playerIdle.Reset(player);

        return _playerIdle;
    }

    private PlayerRun GetPlayerRun(Player player)
    {
        if (_playerRun == null) _playerRun = new PlayerRun(player);
        else _playerRun.Reset(player);

        return _playerRun;
    }

    private PlayerSlide GetPlayerSlide(Player player)
    {
        if (_playerSlide == null) _playerSlide = new PlayerSlide(player);
        else _playerSlide.Reset(player);

        return _playerSlide;
    }
}
