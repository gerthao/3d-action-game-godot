using dActionGame.Asset.Scripts.Components;
using Godot;

namespace dActionGame.Asset.Characters.Player.Visuals;

public class PlayerVisualComponent: VisualComponent<Player>
{
    public PlayerVisualComponent(Player entity) : base(entity)
    {
    }

    [Signal]
    public delegate void IdleEventHandler();

    [Signal]
    public delegate void RunEventHandler();

    [Signal]
    public delegate void SlideEventHandler();

    private void OnIdle()
    {
        GD.Print("on idle");
        Entity.AnimationPlayer.Play("LittleAdventurerAndie_Idel");
        Entity.FootStepVfx.Emitting = false;
    }

    private void OnRun()
    {
        GD.Print("on run");
        Entity.AnimationPlayer.Play("LittleAdventurerAndie_Run");
        Entity.FootStepVfx.Emitting = true;
    }

    private void OnSlide()
    {
        GD.Print("on slide");
        Entity.AnimationPlayer.Play("LittleAdventurerAndie_Roll");
        Entity.FootStepVfx.Emitting = true;
    }


    public void Register()
    {
        /*Idle  += OnIdle;
        Run   += OnRun;
        Slide += OnSlide;*/
    }
}
