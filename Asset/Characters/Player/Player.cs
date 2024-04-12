using dActionGame.Asset.Characters.Player.StateHandlers;
using dActionGame.Asset.Characters.Player.States;
using dActionGame.Asset.Characters.Player.Visuals;
using dActionGame.Asset.Scripts.Interfaces;
using dActionGame.Asset.Scripts.State;
using Godot;

namespace dActionGame.Asset.Characters.Player;

public partial class Player : CharacterBody3D, IHasAnimation
{
	[Signal]
	public delegate void CoinNumberUpdatedEventHandler(int value);
	public AnimationPlayer            AnimationPlayer { get; set; }
	public EntityStateHandler<Player> PlayerMovement    { get; set; }
	public PlayerVisualComponent      PlayerVisualComponent { get; set; }

	private int            _coins;
	public GpuParticles3D FootStepVfx;

	private double _stopDistance = 2.2;
	public  bool   IsStanding => Direction.IsZeroApprox();
	public  bool   IsMoving   => !Direction.IsZeroApprox();
	public  bool   CanAttack  => Input.IsActionJustPressed("attack");
	public  bool   CanSlide   => Input.IsActionJustPressed("slide");


	[Signal]
	public delegate void PlayerIdleEventHandler();

	[Signal]
	public delegate void PlayerRunEventHandler();

	[Signal]
	public delegate void PlayerSlideEventHandler();

	private void OnIdle()
	{
		GD.Print("on idle");
		AnimationPlayer.Play("LittleAdventurerAndie_Idel");
		FootStepVfx.Emitting = false;
	}

	private void OnRun()
	{
		GD.Print("on run");
		AnimationPlayer.Play("LittleAdventurerAndie_Run");
		FootStepVfx.Emitting = true;
	}

	private void OnSlide()
	{
		GD.Print("on slide");
		AnimationPlayer.Play("LittleAdventurerAndie_Roll");
		FootStepVfx.Emitting = true;
	}

	public  Vector3 Direction;
	public  Node3D  Visual;

	public double Speed        => 5.0;
	public double AngularSpeed => 7.0;

	private int Coins
	{
		get => _coins;
		set
		{
			_coins = value;
			EmitSignal(SignalName.CoinNumberUpdated, _coins);
		}
	}

	public override void _Ready()
	{
		Visual                = GetNode<Node3D>("Visual");
		FootStepVfx           = GetNode<GpuParticles3D>("Visual/VFX/Footstep_GPUParticles3D");
		AnimationPlayer       = GetNode<AnimationPlayer>("Visual/AnimationPlayer");

		PlayerIdle            += OnIdle;
		PlayerRun             += OnRun;
		PlayerSlide           += OnSlide;

		PlayerMovement        =  new PlayerMovement(this, new PlayerIdle(this));
		PlayerVisualComponent =  new PlayerVisualComponent(this);
	}

	public override void _PhysicsProcess(double delta)
	{
		Direction = GetInputDirection();
		PlayerMovement.Update(delta);
		MoveAndSlide();
	}

	public Vector3 GetInputDirection()
	{
		var inputDirection = Input.GetVector(
			"move_left",
			"move_right",
			"move_up",
			"move_down"
		).Rotated(-45);

		return (Transform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y))
			.Normalized();
	}

	private void AddCoin(int value)
	{
		Coins += value;
	}
}
