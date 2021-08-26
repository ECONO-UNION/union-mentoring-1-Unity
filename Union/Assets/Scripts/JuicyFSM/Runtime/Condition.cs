namespace JuicyFSM
{
    public abstract class Condition
    {
        public abstract string DisplayName { get; }
        public abstract bool Check();
    }
}
