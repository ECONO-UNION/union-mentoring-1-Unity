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
            _flowChart = _flowChart.Clone();
        }

        private void Update()
        {
            _flowChart.Run();
        }
    }
}