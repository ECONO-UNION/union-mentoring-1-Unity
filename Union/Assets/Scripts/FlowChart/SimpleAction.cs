using JuicyFlowChart;
using UnityEngine;

public class SimpleAction : Action
{
    public int TEST;

    protected override void OnStart()
    {
        Debug.Log("START" + TEST);
    }

    protected override void OnUpdate()
    {
        Debug.Log("Update : " + TEST);
    }
}
