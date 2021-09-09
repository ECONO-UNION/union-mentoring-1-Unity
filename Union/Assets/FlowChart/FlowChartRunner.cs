using UnityEngine;

namespace JuicyFlowChart
{
    public class FlowChartRunner : MonoBehaviour
    {
        [SerializeField]
        private FlowChart _flowChart;
        public FlowChart FlowChart { get => _flowChart; }

        private void Start()
        {
            _flowChart = _flowChart.Clone(gameObject);
        }

        private void Update()
        {
            if (_flowChart == null)
                return;

            _flowChart.Run();
        }
    }
}