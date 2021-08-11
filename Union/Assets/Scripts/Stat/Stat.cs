namespace Union.Services.Stat
{
    public class Stat
    {
        protected int amount;
        protected int maxAmount;

        public Stat()
        {
            this.amount = 0;
            this.maxAmount = 0;
        }

        public void Reset()
        {
            this.amount = 0;
            this.maxAmount = 0;
        }

        public void Set(int amount)
        {
            this.amount = amount;

            if (this.maxAmount == 0)
            {
                this.maxAmount = this.amount;
            }
        }

        public void SetMax(int maxAmount)
        {
            this.maxAmount = maxAmount;
        }

        public int Get()
        {
            return this.amount;
        }

        public int GetMax()
        {
            return this.maxAmount;
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