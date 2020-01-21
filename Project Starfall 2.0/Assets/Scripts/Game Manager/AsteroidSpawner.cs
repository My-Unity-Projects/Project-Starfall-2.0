using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {


    [Header("Ateroids")]
    public GameObject[] asteroids;

    [Header("Asteroid Spawn Settings")]
    public GameObject asteroidSpawn;
    GameObject[] spawnPoints;
    float spawnTimer;
    int spawnCont;

	// Use this for initialization
	void Start () {
        InitializeSpawnPoints();
        spawnTimer = 2;
        spawnCont = 3;
	}
	
	// Update is called once per frame
	void Update () {

        SpawnAsteroids();
   	}

    void SpawnAsteroids()
    {
        GameObject planet = GameObject.Find("Planet");
        
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0)
        {
            for(int i=0; i<spawnPoints.Length; i++)
            {
                int chance = Random.Range(0, 39);

                if(chance == 0)
                {
                    GameObject asteroid = (GameObject)Instantiate(chooseAsteroid());
                    asteroid.transform.position = spawnPoints[i].transform.position;

                    Vector2 direction = planet.transform.position - asteroid.transform.position;
                    asteroid.GetComponent<MyAsteroid>().SetDirection(direction);

                    LookAt2D(asteroid, direction);

                    spawnCont--;
                }

                if(spawnCont == 0)
                {
                    spawnCont = 3;
                    break;
                }
            }
            spawnTimer = 2;
        }        
    }

    /*CHOOSE ASTEROID METHOD*/
    GameObject chooseAsteroid()
    {
        int a = Random.Range(0, 5);

        return asteroids[a];
    }

    /*MAKE ASTEROIDS LOOK AT THE PLANET*/
    public void LookAt2D(GameObject a, Vector2 d)
    {
        float rot_z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        a.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }

    /*INITIALIZE SPAWN POINTS*/
    public void InitializeSpawnPoints()
    {
        spawnPoints = new GameObject[asteroidSpawn.transform.childCount];

        for (int i=0; i<asteroidSpawn.transform.childCount; i++)
        {
            spawnPoints[i] = asteroidSpawn.transform.GetChild(i).gameObject;
        }
    }

   /*SPAWN TIMER METHOD*/
    public void ResetTimer()
    {
        spawnTimer = 2;
    }
}
