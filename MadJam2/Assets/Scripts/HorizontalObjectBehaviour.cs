using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObjectBehaviour : MonoBehaviour
{
    public string name;
    private bool start = false;
    public float vel;
    Rigidbody rB;
    public AudioManager aS;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        aS = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (name == "skater")
        {
            aS.Play("skate");
        }
        if (name == "cow")
        {
            aS.Play("cow");
        }
    }

    // Update is called once per frame
    void Update()
    {
        rB.AddForce(Vector3.left * vel, ForceMode.Force);
    }

    public void Move(float velocity, string n)
    {
        start = true;
        vel = velocity;
        name = n;
    }
}
