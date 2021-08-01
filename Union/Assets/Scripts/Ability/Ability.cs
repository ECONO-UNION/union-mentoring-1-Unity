namespace Union.Services.Ability
{
    public class Ability
    {
        protected int amount;

        public Ability()
        {
            this.amount = 0;
        }

        public void Reset()
        {
            this.amount = 0;
        }

        public void Set(int amount)
        {
            this.amount = amount;
        }

        public int Get()
        {
            return this.amount;
        }

        public void Increase(int amount)
        {
            this.amount += amount;
        }

        public void Decrease(int amount)
        {
            this.amount -= amount;
        }
    }

    public class HealthPoint : Ability { }
    public class PhysicalPower : Ability { }
    public class PhysicalDefense : Ability { }
    public class WalkingSpeed : Ability { }
    public class RunningSpeed : Ability { }
    public class JumpingPower : Ability { }
}