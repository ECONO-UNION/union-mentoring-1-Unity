namespace Union.Services.Game
{
    public class Logic : Singleton<Logic>
    {
        public Time Time { get; private set; }

        private FiniteStateMachineController _finiteStateMachineController;

        private void Awake()
        {
            this.Time = new Time();
            this._finiteStateMachineController = new FiniteStateMachineController();
        }

        private void Start()
        {
            this._finiteStateMachineController.Initialize();
        }

        private void Update()
        {
            this._finiteStateMachineController.Run();
        }

        public void UpdatePlayTime(float addTime)
        {
            this.Time.UpdatePlayTime(addTime);
        }

        public bool IsPlaying()
        {
            if (this._finiteStateMachineController.CurrentStateNumber != StateNumber.Playing)
                return false;            

            return true;
        }
    }
}