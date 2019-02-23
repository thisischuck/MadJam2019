using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public float lifeSpan = 5;
    private float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += 1 * Time.deltaTime;

        if (counter >= lifeSpan)
            Destroy(this.gameObject);
    }
}
