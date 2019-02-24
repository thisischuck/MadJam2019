﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShadow : MonoBehaviour
{
    public GameObject template;
    public float timeToFall = 2;
    public float auxTimer = 0;
    public float speed;
    public bool start = false;
    public bool thunder = false;
    private float startingScaleX;
    private float startingScaleZ;
    public List<GameObject> fallingEnemies;
    private int position = 0;
    private float mass = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        startingScaleX = transform.localScale.x;
        startingScaleZ = transform.localScale.z;
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            auxTimer += 1 * Time.deltaTime;
            float scale = ScalePercentage(timeToFall, auxTimer);
            transform.localScale = new Vector3(startingScaleX * scale, transform.localScale.y, startingScaleZ * scale);

            if (auxTimer >= timeToFall)
            {
                LetObjectFall();
            }
        }
    }

    private float ScalePercentage(float maxTime, float currentTime)
    {
        float x = 1 - (currentTime / maxTime);
        Debug.Log("Current Time: " + currentTime + "Current Scale: " + x);
        return 1 - (currentTime / maxTime);
    }

    private void LetObjectFall()
    {
        if (!thunder)
        {
            GameObject objectToFall = Instantiate(fallingEnemies[position], new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
            objectToFall.GetComponent<Rigidbody>().mass = 10 * mass;
            Destroy(this.gameObject);
        }
        else
            if (thunder)
        {
            GameObject objectToFall = Instantiate(fallingEnemies[position], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void StartFalling(float timeToFall, float mass, int pos)
    {
        if (pos != 2)
        {
            this.timeToFall = timeToFall;
            this.mass = mass;
            start = true;
            position = pos;
        }
        else
            if (pos == 2)
        {
            this.timeToFall = timeToFall;
            this.mass = 0;
            start = true;
            thunder = true;
            position = pos;
        }
    }
}
