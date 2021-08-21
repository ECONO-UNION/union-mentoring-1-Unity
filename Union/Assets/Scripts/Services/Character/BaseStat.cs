namespace Union.Services.Charcater
{
    public class BaseStat
    {
        public Stat.Stat HealthPoint { get; set; }

        public Stat.Stat PhysicalPower { get; set; }
        public Stat.Stat PhysicalDefense { get; set; }

        public Stat.Stat WalkingSpeed { get; set; }
        public Stat.Stat RunningSpeed { get; set; }

        public Stat.Stat JumpingPower { get; set; }

        public BaseStat()
        {
            this.HealthPoint = new Stat.Stat();

            this.PhysicalPower = new Stat.Stat();
            this.PhysicalDefense = new Stat.Stat();

            this.WalkingSpeed = new Stat.Stat();
            this.RunningSpeed = new Stat.Stat();

            this.JumpingPower = new Stat.Stat();
        }

        public BaseStat(int healthPointAmount, int physicalPowerAmount, int physicalDefenseAmount,
                        int walkingSpeedAmount, int runningSpeedAmount, int jumpingPowerAmount)
        {
            this.HealthPoint = new Stat.Stat(healthPointAmount);

            this.PhysicalPower = new Stat.Stat(physicalPowerAmount);
            this.PhysicalDefense = new Stat.Stat(physicalDefenseAmount);

            this.WalkingSpeed = new Stat.Stat(walkingSpeedAmount);
            this.RunningSpeed = new Stat.Stat(runningSpeedAmount);

            this.JumpingPower = new Stat.Stat(jumpingPowerAmount);
        }
    }
}