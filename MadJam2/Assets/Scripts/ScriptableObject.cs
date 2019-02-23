using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class HazardObjects : ScriptableObject
{
    // For the Thunder Object
    public List<Sprite> spriteList;

    [Tooltip("Fill only with 0 and 1")]
    public List<uint> direction;

    public List<ParticleSystem> particleSystem;

    public List<int> Size;

    public List<float> Mass;

    public List<float> LifeTime;
}
