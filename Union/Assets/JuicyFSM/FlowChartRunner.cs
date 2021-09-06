using UnityEngine;

namespace JuicyFSM
{
    public class FlowChartRunner : MonoBehaviour
    {
        [SerializeField]
        private FlowChart _flowChart;
        public FlowChart FlowChart { get => _flowChart; }
    }
}