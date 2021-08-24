namespace Union.Services.Game
{
    public class Logic : Singleton<Logic>
    {
        public Time Time { get; private set; }

        private FiniteStateMachineController _finiteStateMachine;

        private void Awake()
        {
            this.Time = new Time();
            this._finiteStateMachine = new FiniteStateMachineController();
        }

        private void Start()
        {
            this._finiteStateMachine.Initialize();
        }

        private void Update()
        {
            this._finiteStateMachine.Run();
        }

        public void UpdatePlayTime(float addTime)
        {
            this.Time.UpdatePlayTime(addTime);
        }

        public bool IsPlaying()
        {
            if (this._finiteStateMachine.CurrentStates != States.Playing)
                return false;            

            return true;
        }
    }
}