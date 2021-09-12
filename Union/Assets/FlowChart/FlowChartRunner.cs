using UnityEngine;

namespace JuicyFlowChart
{
    public class FlowChartRunner : MonoBehaviour
    {
        [SerializeField]
        private FlowChart _flowChart;
        public FlowChart FlowChart { get => _flowChart; }

        private Task _root;

        private void Start()
        {
            _root = _flowChart.Clone(gameObject);
        }

        private void Update()
        {
            if (_root == null)
                return;

            _root.Tick();
        }
    }
}