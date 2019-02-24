using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBehaviour : MonoBehaviour
{
    public AudioManager aS;
    // Start is called before the first frame update
    void Start()
    {
        aS = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        aS.Play("thunder");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
