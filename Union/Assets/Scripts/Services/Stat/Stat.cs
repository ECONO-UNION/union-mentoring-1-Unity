namespace Union.Services.Stat
{
    public class Stat
    {
        protected int _amount;
        protected int _maxAmount;

        public Stat()
        {
            this._amount = 0;
            this._maxAmount = 0;
        }

        public Stat(int amount)
        {
            this._amount = amount;
            this._maxAmount = amount;
        }

        public void Reset()
        {
            this._amount = 0;
            this._maxAmount = 0;
        }

        public void Set(int amount)
        {
            this._amount = amount;

            if (this._maxAmount == 0)
            {
                this._maxAmount = this._amount;
            }
        }

        public void SetMax(int maxAmount)
        {
            this._maxAmount = maxAmount;
        }

        public int Get()
        {
            return this._amount;
        }

        public int GetMax()
        {
            return this._maxAmount;
        }

        public void Increase(int amount)
        {
            this._amount += amount;
        }

        public void Decrease(int amount)
        {
            this._amount -= amount;
        }
    }
}