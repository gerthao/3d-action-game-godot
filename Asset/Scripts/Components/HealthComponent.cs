using Godot;

namespace dActionGame.Asset.Scripts.Components;

public partial class HealthComponent : Node
{
    [Signal]
    public delegate void DiedEventHandler();

    [Signal]
    public delegate void HealthChangedEventHandler();

    [Export] protected int MaxHealth = 10;
    private            int CurrentHealth { get; set; }

    public bool IsDead => CurrentHealth == 0;

    public double GetHealthPercent => MaxHealth <= 0 ? 0 : Mathf.Min(CurrentHealth / MaxHealth, 1);

    public override void _Ready()
    {
        base._Ready();
        CurrentHealth = MaxHealth;
    }

    private void CheckDeath()
    {
        if (!IsDead) return;

        Owner.QueueFree();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        EmitSignal(SignalName.HealthChanged);
        CallDeferred(MethodName.CheckDeath);
    }
}
