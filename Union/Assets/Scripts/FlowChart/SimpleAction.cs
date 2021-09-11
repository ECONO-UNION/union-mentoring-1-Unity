using JuicyFlowChart;
using UnityEngine;

public class SimpleAction : Action
{
    public int TEST = 3;
    public GameObject go1;
    [SerializeField]
    private GameObject go;

    protected override void Start()
    {
        Debug.Log("START" + TEST);
    }

    protected override void Update()
    {
        Debug.Log(gameObject.transform.position);
    }
}
