using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAsteroid : MonoBehaviour {

    [Header("Particle Settings")]
    public GameObject effect;

    [Header("Asteroid Settings")]
    float speed;
    Vector2 directionNormalized;
    bool isReady;

    [Header("Reward Settings")]
    public GameObject crystal;

    void Awake()
    {
        speed = 25f;
        isReady = false;
    }

    void Start()
    {
        if(transform.tag == "Asteroid_1")
        {
            speed = 25;
        }

        if (transform.tag == "Asteroid_2")
        {
            speed = 30;
        }

        if (transform.tag == "Asteroid_3")
        {
            speed = 37.5f;
        }

        if (transform.tag == "Asteroid_4")
        {
            speed = 40f;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        directionNormalized = direction.normalized;
        isReady = true;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isReady)
        {
            Vector2 position = transform.position;
            position += directionNormalized * speed * Time.deltaTime;

            transform.position = position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Camera.main.transform.parent.GetComponent<Animator>().SetTrigger("shake");

            /*if(gameObject.tag == "Asteroid_1")
            {
                sm.orangeScore++;
            }

            if(gameObject.tag == "Asteroid_2")
            {
                sm.purpleScore++;
            }

            if(gameObject.tag == "Asteroid_3")
            {
                sm.greenScore++;
            }

            if(gameObject.tag == "Asteroid_4")
            {
                sm.redScore++;                
            }*/

            SpawnCrystal(crystal);
            Destroy(gameObject);
        }
    }


    public void SpawnCrystal(GameObject crystal)
    {
        int chance = Random.Range(0, 3);

        if(chance < 3)
        {
            Instantiate(crystal, this.transform.position, Quaternion.Euler(transform.eulerAngles));
        }
    }
}
