using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFowardThenDie : MonoBehaviour
{
    int i = 0;

    // Start is called before the first frame update
    void Update()
    {
        i++;
        this.transform.position += new Vector3(0.25f, 0, 0);

        if (i > 1000)
        {
            Destroy(this.gameObject);
        }
    }
}
