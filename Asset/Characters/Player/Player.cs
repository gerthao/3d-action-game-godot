using dActionGame.Asset.Scripts.Interfaces;
using dActionGame.Asset.Scripts.State;
using Godot;

namespace dActionGame.Asset.Characters.Player;

public partial class Player : CharacterBody3D, IHasAnimation
{
	[Signal]
	public delegate void CoinNumberUpdatedEventHandler(int value);
	public AnimationPlayer            AnimationPlayer { get; set; }
	public EntityStateHandler<Player> StateHandler    { get; set; }

	private int            _coins;
	private GpuParticles3D _footstepVfx;

	private double _stopDistance = 2.2;
	public  bool   IsStanding => Direction.IsZeroApprox();
	public  bool   IsMoving   => !Direction.IsZeroApprox();
	public  bool   CanAttack  => Input.IsActionJustPressed("attack");
	public  bool   CanSlide   => Input.IsActionJustPressed("slide");

	[Signal]
	public delegate void PlayerIdleEventHandler();

	[Signal]
	public delegate void PlayerRunEventHandler();

	private void OnIdle()
	{
		GD.Print("on idle");
		AnimationPlayer.Play("LittleAdventurerAndie_Idel");
		_footstepVfx.Emitting = false;
	}

	private void OnRun()
	{
		GD.Print("on run");
		AnimationPlayer.Play("LittleAdventurerAndie_Run");
		_footstepVfx.Emitting = true;
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
		Visual          = GetNode<Node3D>("Visual");
		_footstepVfx    = GetNode<GpuParticles3D>("Visual/VFX/Footstep_GPUParticles3D");
		AnimationPlayer = GetNode<AnimationPlayer>("Visual/AnimationPlayer");
		StateHandler    = new PlayerEntityStateHandler(new PlayerIdle());

		PlayerIdle += OnIdle;
		PlayerRun += OnRun;
	}

	public override void _PhysicsProcess(double delta)
	{
		var inputDirection = Input.GetVector(
			"move_left",
			"move_right",
			"move_up",
			"move_down"
		).Rotated(-45);

		Direction = (Transform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y))
			.Normalized();

		StateHandler.Handle(delta, this);

		MoveAndSlide();
	}

	private void AddCoin(int value)
	{
		Coins += value;
	}
}
