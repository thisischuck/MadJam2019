using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RNGController : MonoBehaviour
{
    //Time Controllers
    [Header("Difficulty Spikes")]
    public float timePassed;
    public int dificultyIncrease_1 = 15;
    public int dificultyIncrease_2 = 30;
    public int dificultyIncrease_3 = 45;

    //Timer
    [Header("Timers")]
    [Tooltip("Time to spawn next set of hazards")]
    public float TimeToSpawn = 5;
    public float timeCounter = 0;

    //Player
    [Header("Spawn points and Player")]
    public GameObject spawnerBack;
    public GameObject spawnerFront;
    public GameObject spawnerFarAway;
    public GameObject player;

    //Terrain points
    [Header("Terrain parameters")]
    public float terrainHeightTop = 16;
    public float terrainHeightBot = -16;
    [Tooltip("Radius where semi close hazards will be spawned.")]
    public float semiClosePlayerDistance = 3;

    //Objects to Spawn
    [Header("Objects to Spawn")]
    [Tooltip("Vertical hazard. Start with shadow.")]
    public GameObject template_vertical;
    [Tooltip("Horizontal hazard.")]
    public GameObject template_horizontal;

    //Chances
    [Header("Chances")]
    public float chance_toSpawnInFront = 20.0f;
    public float chance_toSpawnSemiClose = 50.0f;
    public float chance_toSpawnRandomPosition = 70.0f;
    public float chance_toSpawnFarAway = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += 1 * Time.deltaTime;
        if (timeCounter >= TimeToSpawn)
        {
            GenerateLocation();
            timeCounter = 0;
        }

        TimeController();
    }

    private void TimeController()
    {
        timePassed += 1 * Time.deltaTime;

        if (timePassed >= dificultyIncrease_1)
        {
            chance_toSpawnInFront = 30.0f;
            chance_toSpawnSemiClose = 60.0f;
            chance_toSpawnRandomPosition = 75.0f;
            TimeToSpawn = 4;
        }

        if (timePassed >= dificultyIncrease_2)
        {
            chance_toSpawnInFront = 40.0f;
            chance_toSpawnSemiClose = 70.0f;
            chance_toSpawnRandomPosition = 80.0f;
            TimeToSpawn = 3;
        }

        if (timePassed >= dificultyIncrease_3)
        {
            chance_toSpawnInFront = 50.0f;
            chance_toSpawnSemiClose = 80.0f;
            chance_toSpawnRandomPosition = 90.0f;
            TimeToSpawn = 2;
        }
    }

    private void GenerateLocation()
    {
        float chanceToSpawn = 0.0f;

        //Close to player
        chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn <= chance_toSpawnInFront)
        {
            SpawningInFront();
        }

        //Semi Close
        chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn <= chance_toSpawnSemiClose)
        {
            SpawningSemiClose();
        }

        //Random Position
        chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn <= chance_toSpawnRandomPosition)
        {
            SpawningRandomPosition();
        }

        //Far Away
        chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn <= chance_toSpawnFarAway)
        {
            SpawningFarAway();
        }
    }

    private void SpawningFarAway()
    {
        float x = spawnerFarAway.transform.position.x;
        float z = Random.Range(terrainHeightBot, terrainHeightTop);

        SpawnObjectHorizontally(x, player.transform.position.y, z);
    }

    private void SpawningRandomPosition()
    {
        float x = Random.Range(spawnerBack.transform.position.x, spawnerFront.transform.position.x);
        float z = Random.Range(terrainHeightBot, terrainHeightTop);

        SpawnObject(x, player.transform.position.y, z);
    }

    private void SpawningSemiClose()
    {
        float x = Random.Range(player.transform.position.x - semiClosePlayerDistance, player.transform.position.x + semiClosePlayerDistance);
        float z = Random.Range(player.transform.position.z - semiClosePlayerDistance, player.transform.position.z + semiClosePlayerDistance);

        while (z > terrainHeightTop || z < terrainHeightBot)
        {
            z = Random.Range(player.transform.position.z - semiClosePlayerDistance, player.transform.position.z + semiClosePlayerDistance);
        }

        SpawnObject(x, player.transform.position.y, z);
    }

    private void SpawningInFront()
    {
        float x = 0, z = 0;

        if (Input.GetAxis("Horizontal") > 0) { x = 2; }
        if (Input.GetAxis("Horizontal") < 0) { x = -2; }
        if (Input.GetAxis("Vertical") > 0) { z = 2; }
        if (Input.GetAxis("Vertical") < 0) { z = -2; }

        SpawnObjectSpecial(player.transform.position.x + x, player.transform.position.y, player.transform.position.z + z);

    }

    private void SpawnObjectSpecial(float x, float y, float z)
    {
        GameObject newObj = Instantiate(template_vertical, new Vector3(x, y, z), Quaternion.identity);
        newObj.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void SpawnObject(float x, float y, float z)
    {
        GameObject newObj = Instantiate(template_vertical, new Vector3(x, y, z), Quaternion.identity);
        newObj.GetComponent<ObjectShadow>().StartFalling(2, 5);
    }

    private void SpawnObjectHorizontally(float x, float y, float z)
    {
        GameObject newObj = Instantiate(template_horizontal, new Vector3(x, y, z), Quaternion.identity);
        template_horizontal.GetComponent<HorizontalObjectBehaviour>().Move(10);
    }
}
