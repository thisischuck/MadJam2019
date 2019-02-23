using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObjectBehaviour : MonoBehaviour
{
    private bool start = false;
    public float vel;
    Rigidbody rB;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        rB.AddForce(Vector3.left * vel, ForceMode.Force);
    }

    public void Move(float velocity)
    {
        start = true;
        vel = velocity;
    }
}
