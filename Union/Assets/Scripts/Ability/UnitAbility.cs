using Union.Services.Ability;

public class UnitAbility
{
    public HealthPoint healthPoint;

    public PhysicalPower physicalPower;
    public PhysicalDefense physicalDefense;

    public WalkingSpeed walkingSpeed;
    public RunningSpeed runningSpeed;

    public JumpingPower jumpingPower;

    public UnitAbility()
    {
        this.healthPoint = new HealthPoint();

        this.physicalPower = new PhysicalPower();
        this.physicalDefense = new PhysicalDefense();

        this.walkingSpeed = new WalkingSpeed();
        this.runningSpeed = new RunningSpeed();

        this.jumpingPower = new JumpingPower();
    }
}
