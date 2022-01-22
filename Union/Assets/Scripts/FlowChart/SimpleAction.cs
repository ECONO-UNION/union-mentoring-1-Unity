using JuicyFlowChart;
using UnityEngine;

public class SimpleAction : Action
{
    public string ABC;

    protected override void Start()
    {
        Debug.Log("START");
    }

    protected override void Update()
    {
        Debug.Log(ABC);
    }
}
