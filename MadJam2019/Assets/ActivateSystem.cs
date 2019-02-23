using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSystem : MonoBehaviour
{
    // Start is called before the first frame update

    private ParticleSystem ps;
    public int nrPArticles;


    void Start()
    {
        this.ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ps.Emit(nrPArticles);
        }

    }
}
