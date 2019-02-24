using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float lifeSpan = 5;
    private float counter = 0;
    public bool useGravity = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
