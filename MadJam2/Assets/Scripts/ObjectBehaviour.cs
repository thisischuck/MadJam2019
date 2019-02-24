using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public string name;
    public Rigidbody rigidbody;
    public float lifeSpan = 5;
    private float counter = 0;
    public bool useGravity = true;
    public AudioManager aS;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        aS = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (name == "seagull")
        {
            aS.Play("seagul");
        }
        if (name == "cow")
        {
            aS.Play("cow");
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter += 1 * Time.deltaTime;

        if (counter >= lifeSpan)
            Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        rigidbody.useGravity = false;
        if (useGravity) rigidbody.AddForce(Physics.gravity * (rigidbody.mass * rigidbody.mass));
    }
}
