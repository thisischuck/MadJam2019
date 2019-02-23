using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class HazardObjects : ScriptableObject
{
    // For the Thunder Object
    public ParticleSystem particleSystem;

    public List<Sprite> spriteList;

    public float Mass;

    public float LifeTime;
}
