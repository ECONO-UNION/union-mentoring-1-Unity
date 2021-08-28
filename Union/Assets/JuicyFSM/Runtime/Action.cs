using System.Collections;

namespace JuicyFSM
{
    public abstract class Action
    {
        public abstract IEnumerator Act();
    }
}
