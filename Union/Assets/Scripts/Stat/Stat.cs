namespace Union.Services.Stat
{
    public class Stat
    {
        protected int amount;

        public Stat()
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
}