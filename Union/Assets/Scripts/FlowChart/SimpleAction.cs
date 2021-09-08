using JuicyFlowChart;
using UnityEngine;

public class SimpleAction : Action
{
    public int TEST;

    protected override void OnStart()
    {
        Debug.Log("START" + Children.Count);
    }

    protected override void OnUpdate()
    {
        Debug.Log("Update : " + Children.Count);
    }
}
