using Union.Services.Stat;

namespace Union.Services.Charcater
{
    public class BaseStat
    {
        public Stat.Stat healthPoint;

        public Stat.Stat physicalPower;
        public Stat.Stat physicalDefense;

        public Stat.Stat walkingSpeed;
        public Stat.Stat runningSpeed;

        public Stat.Stat jumpingPower;

        public BaseStat()
        {
            this.healthPoint = new Stat.Stat();

            this.physicalPower = new Stat.Stat();
            this.physicalDefense = new Stat.Stat();

            this.walkingSpeed = new Stat.Stat();
            this.runningSpeed = new Stat.Stat();

            this.jumpingPower = new Stat.Stat();
        }
    }
}