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
    public int dificultyIncrease_hell = 60;

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
    public float mapHeight = 1.5f;

    //Objects to Spawn
    [Header("Objects to Spawn")]
    [Tooltip("Vertical hazard. Start with shadow.")]
    public GameObject template_vertical;
    [Tooltip("Horizontal hazard. Start with Warning")]
    public GameObject nerd, tree, warning_template;
	public List<int> enemyList;

    private float fallingTime = 2;
    private float mass = 0.1f;

    private float minSpeed = 10;
    private float maxSpeed = 12;

    public float numberOfTimes = 1;

    //Chances
    [Header("Chances")]
    public float chance_toSpawnInFront = 20.0f;
    public float chance_toSpawnSemiClose = 50.0f;
    public float chance_toSpawnRandomPosition = 70.0f;
    public float chance_toSpawnFarAway = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<int>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count == 0)
        {
            RandomizeList();
        }

        timeCounter += 1 * Time.deltaTime;
        if (timeCounter >= TimeToSpawn)
        {
            GenerateLocation();
            timeCounter = 0;
        }

        TimeController();
    }

    private void RandomizeList()
    {
        int[] list = new int[3] { 0, 1, 2 };
        enemyList = new List<int>(list);
        Shuffle();
    }

    private void Shuffle()
    {
        var count = enemyList.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = enemyList[i];
            enemyList[i] = enemyList[r];
            enemyList[r] = tmp;
        }
    }

    private void TimeController()
    {
        timePassed += 1 * Time.deltaTime;

        if (timePassed >= dificultyIncrease_1)
        {
            chance_toSpawnInFront = 40.0f;
            chance_toSpawnSemiClose = 70.0f;
            chance_toSpawnRandomPosition = 75.0f;
            TimeToSpawn = 2.5f;
            numberOfTimes = 1;

            fallingTime = 1.5f;
            mass = 1f;

            minSpeed = 12;
            maxSpeed = 14;
        }

        if (timePassed >= dificultyIncrease_2)
        {
            chance_toSpawnInFront = 50.0f;
            chance_toSpawnSemiClose = 70.0f;
            chance_toSpawnRandomPosition = 80.0f;
            TimeToSpawn = 2;
            numberOfTimes = 2;

            fallingTime = 1.2f;
            mass = 1.2f;

            minSpeed = 15;
            maxSpeed = 17;
        }

        if (timePassed >= dificultyIncrease_3)
        {
            chance_toSpawnInFront = 60.0f;
            chance_toSpawnSemiClose = 80.0f;
            chance_toSpawnRandomPosition = 90.0f;
            TimeToSpawn = 1;
            numberOfTimes = 3;

            fallingTime = 0.8f;
            mass = 1.5f;

            minSpeed = 18;
            maxSpeed = 20;
        }

        if (timePassed >= dificultyIncrease_hell)
        {
            chance_toSpawnInFront = 80.0f;
            chance_toSpawnSemiClose = 95.0f;
            chance_toSpawnRandomPosition = 95.0f;
            TimeToSpawn = 0.5f;
            numberOfTimes = 4;

            fallingTime = 0.8f;
            mass = 2f;

            minSpeed = 22;
            maxSpeed = 25;
        }
    }

    private void GenerateLocation()
    {
        float chanceToSpawn = 0.0f;

        //Close to player
        chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn <= chance_toSpawnInFront)
        {
            for (int i = 0; i < numberOfTimes; i++)
            {
                SpawningInFront();
            }
        }

        //Semi Close
        chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn <= chance_toSpawnSemiClose)
        {
            for (int i = 0; i < numberOfTimes; i++)
            {
                SpawningSemiClose();
            }
        }

        //Random Position
        chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn <= chance_toSpawnRandomPosition)
        {
            for (int i = 0; i < numberOfTimes; i++)
            {
                SpawningRandomPosition();
            }
        }

        //Far Away
        chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn <= chance_toSpawnFarAway)
        {
            for (int i = 0; i < numberOfTimes; i++)
            {
                SpawningFarAway();
            }
        }
    }

    private void SpawningFarAway()
    {
        float x = spawnerFarAway.transform.position.x;
        float z = Random.Range(terrainHeightBot, terrainHeightTop);

        SpawnObjectHorizontally(x, mapHeight, z);
    }

    private void SpawningRandomPosition()
    {
        float x = Random.Range(spawnerBack.transform.position.x, spawnerFront.transform.position.x);
        float z = Random.Range(terrainHeightBot, terrainHeightTop);

        SpawnObject(x, mapHeight, z);
    }

    private void SpawningSemiClose()
    {
        float x = Random.Range(player.transform.position.x - (semiClosePlayerDistance / 2), player.transform.position.x + semiClosePlayerDistance + 1);
        float z = Random.Range(player.transform.position.z - semiClosePlayerDistance, player.transform.position.z + semiClosePlayerDistance);

        while ((player.transform.position.z + semiClosePlayerDistance < terrainHeightTop && player.transform.position.z - semiClosePlayerDistance > terrainHeightBot) && (z > terrainHeightTop || z < terrainHeightBot))
        {
            z = Random.Range(player.transform.position.z - semiClosePlayerDistance, player.transform.position.z + semiClosePlayerDistance);
        }

        SpawnObject(x, mapHeight, z);
    }

    private void SpawningInFront()
    {
        float x = 0, z = 0;

        if (Input.GetKey(PlayerSettings.Instance.Forward)) { x = 5; }
        if (Input.GetKey(PlayerSettings.Instance.Back)) { x = -5; }
        if (Input.GetKey(PlayerSettings.Instance.Right)) { z = 5; }
        if (Input.GetKey(PlayerSettings.Instance.Left)) { z = -5; }

        SpawnObject(player.transform.position.x + x, mapHeight, player.transform.position.z + z);

    }

    private void SpawnObject(float x, float y, float z)
    {
        int randomEnemy = enemyList[Random.Range(0, enemyList.Count)];
        enemyList.Remove(randomEnemy);

        GameObject newObj = Instantiate(template_vertical, new Vector3(x, y, z), Quaternion.identity);
        newObj.GetComponent<ObjectShadow>().StartFalling(fallingTime, mass, randomEnemy);
    }

    private void SpawnObjectHorizontally(float x, float y, float z)
    {
        int random = Random.Range(0, 2);
		GameObject newObj;

		if (random == 0)
        {
            newObj = Instantiate(nerd, new Vector3(x, y, z), Quaternion.identity);
            newObj.GetComponent<HorizontalObjectBehaviour>().Move(Random.Range(minSpeed, maxSpeed), "skater");
        }
        else
        if (random == 1)
        {
            newObj = Instantiate(tree, new Vector3(x, y, z), Quaternion.identity);
            newObj.GetComponent<HorizontalObjectBehaviour>().Move(minSpeed, "skater");
        }
        else
        {
            newObj = Instantiate(nerd, new Vector3(x, y, z), Quaternion.identity);
            newObj.GetComponent<HorizontalObjectBehaviour>().Move(Random.Range(minSpeed, maxSpeed), "skater");
        }

		GameObject warning = Instantiate(warning_template, new Vector3(spawnerFront.transform.position.x, spawnerFront.transform.position.y, newObj.transform.position.z), Quaternion.identity);
		//warning.GetComponent<ObjectShadow>().StartFalling(fallingTime, mass, );
	}
}
