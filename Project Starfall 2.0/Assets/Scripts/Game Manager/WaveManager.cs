using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*WAVE AND ENEMY SPAWNER MANAGER CLASS*/
public class WaveManager: MonoBehaviour
{
    [Header("Planet Info.")]
    public Transform planet;

    [Header("Wave Settings")]
    public int waveNumber;
    private int enemyAmmount;

    [Header("Enemy Spawn Points")]
    private EnemySpawn[] enemySpawns = new EnemySpawn[4];
    private Position[] movingPositions = new Position[8];

    [Header("Enemy Types")]
    public GameObject[] enemiesToSpawn;

    [Header("Wave Enemies")]
    private List<GameObject> waveEnemies;

    [Header("Random enemy percentage variance")]
    public int variance;

    // Start is called before the first frame update
    void Start()
    {
        InitEnemySpawns();
        waveNumber = 1;
        SetWave();
    }

    // Update is called once per frame
    void Update()
    {
        NextWave();
    }

    /*SET WAVE METHOD*/
    public void SetWave()
    {
        enemyAmmount = waveNumber / 2 + 1;

        if(enemyAmmount > 8)
        {
            enemyAmmount = 8;
            enemyAmmount = enemyAmmount / 2 + Random.Range(1, 4);
        }
        waveEnemies = new List<GameObject>(enemyAmmount);
        SpawnEnemies();
    }

    /*SPAWN ENEMY METHOD*/
    public void SpawnEnemies()
    {
        for(int i=0; i<enemyAmmount; i++)
        {
            int randomSpawnPoint = Random.Range(0, enemySpawns.Length -1);

            GameObject enemy = Instantiate(RandomEnemy(), enemySpawns[randomSpawnPoint].getSpawnPoint(), Quaternion.identity);
            enemy.GetComponent<MyEnemy>().movingPositions = enemySpawns[randomSpawnPoint].getPositions();

            waveEnemies.Add(enemy);
        }
    }

    public GameObject RandomEnemy()
    {
        GameObject randomEnemy = null;

        int random = Random.Range(1, 101);

        int porPercentage = waveNumber * variance;

        if(porPercentage > 95)
        {
            porPercentage = 95;
        }

        // Debug.Log(random);

        // Green or blue
        if(random <= (100 - (porPercentage)))
        {
            // Green
            if(random <= ((100 - (porPercentage))*0.6f))
            {
                randomEnemy = enemiesToSpawn[0];
            }

            // Blue
            else
            {
                randomEnemy = enemiesToSpawn[1];
            }
        }

        // Purple, orange or red
        else
        {
            // Purple
            if(random <= (100 - (porPercentage)) + (porPercentage)*0.4f)
            {
                randomEnemy = enemiesToSpawn[2];
            }

            // Orange
            else if(random <= (100 - (porPercentage)) + (porPercentage)*0.7f)
            {
                randomEnemy = enemiesToSpawn[3];

            }

            // Red
            else if(random <= (100 - (porPercentage)) + (porPercentage))
            {
                randomEnemy = enemiesToSpawn[4];
            }
        }
         // randomEnemy = enemiesToSpawn[2]; // Debug
        return randomEnemy;
    }

    /*GET INTO NEXT WAVE MEHTOD*/
    public void NextWave()
    {
        bool nextWave = true;

        for(int i=0; i< waveEnemies.Count; i++) // Check if every enemy of the wave has been destroyed
        {
            if(waveEnemies[i] != null) // If any of the enemies is not destroyed
            {
                nextWave = false; // Not get into next wave
                break;
            }
        }

        if (nextWave) // If every enemy is destroyed 
        {
            waveNumber++; // Get into next wave
            SetWave();
        }         
    }


    /*INIT ENEMY SPAWNS AND POSITIONS*/
    public void InitEnemySpawns()
    {
        // Init moving positions
        movingPositions[0] = new Position(new Vector2(-14, 14));
        movingPositions[1] = new Position(new Vector2(0, 18));
        movingPositions[2] = new Position(new Vector2(14, 14));
        movingPositions[3] = new Position(new Vector2(18, 0));
        movingPositions[4] = new Position(new Vector2(14, -14));
        movingPositions[5] = new Position(new Vector2(0, -18));
        movingPositions[6] = new Position(new Vector2(-14, -14));
        movingPositions[7] = new Position(new Vector2(-18, 0));


        // Init Enemy Spawns
        enemySpawns[0] = new EnemySpawn(new Vector2(0, 30), movingPositions[0], movingPositions[1], movingPositions[2]);
        enemySpawns[1] = new EnemySpawn(new Vector2(50, 0), movingPositions[2], movingPositions[3], movingPositions[4]);
        enemySpawns[2] = new EnemySpawn(new Vector2(0, -30), movingPositions[4], movingPositions[5], movingPositions[6]);
        enemySpawns[3] = new EnemySpawn(new Vector2(-50, 0), movingPositions[6], movingPositions[7], movingPositions[0]);

    }
}
