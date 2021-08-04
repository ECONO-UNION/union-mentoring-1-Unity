namespace Union.Services.Stat
{
    public class UnitStat
    {
        public Stat healthPoint;

        public Stat physicalPower;
        public Stat physicalDefense;

        public Stat walkingSpeed;
        public Stat runningSpeed;

        public Stat jumpingPower;

        public UnitStat()
        {
            this.healthPoint = new Stat();

            this.physicalPower = new Stat();
            this.physicalDefense = new Stat();

            this.walkingSpeed = new Stat();
            this.runningSpeed = new Stat();

            this.jumpingPower = new Stat();
        }
    }
}