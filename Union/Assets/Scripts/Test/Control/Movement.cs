using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform objectTransfrom;
    private Rigidbody movementRigidbody;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        this.objectTransfrom = this.gameObject.transform;
        this.movementRigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Translate(Vector3 directionVector, float speed)
    {
        this.objectTransfrom.Translate(directionVector * speed * Time.deltaTime);
    }

    public void AddForce(Vector3 force)
    {
        this.movementRigidbody.AddForce(force);
    }
}
