using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Object : ScriptableObject
{
    // For the Thunder Object
    public ParticleSystem particleSystem;

    public List<Sprite> spriteList;

    public float Velocity;

    public float LifeTime;
}
