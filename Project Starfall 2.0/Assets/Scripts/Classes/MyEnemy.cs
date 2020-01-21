using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemy : MonoBehaviour {

    [Header("Health Settings")]
    public int health;

    [Header("Component Settings")]
    private Animator animator;

    [Header("Particles Settings")]
    public ParticleSystem deathParticles;

    [Header("Moving State Settings")]
    public float movingSpeed;
    public Position[] movingPositions;
    private Position chosenPosition;
    private bool isMoving = false;

    [Header("Attack State Settings")]
    public float attackTime;
    private float attackTimeCounter;
    private Collider2D attackCollider; // Attack collider info
    public ParticleSystem attackEffect; // Attack particle system info

    [Header("State Settings")]
    public EnemyState state = EnemyState.MOVING;
   
    public enum EnemyState
    {
        MOVING,
        READY
    };

	// Use this for initialization
	void Start ()
    {
        attackCollider = GetComponent<BoxCollider2D>(); // Initialize attack box collider
        attackTimeCounter = attackTime; // Initialize our counter with attackTime value

        animator = GetComponent<Animator>(); // Initialize animator component
	}
	
	// Update is called once per frame
	void Update ()
    {
        StateManagement();
        LookAt2D();
	}

    /*STATE MANAGEMENT METHOD*/
    public void StateManagement()
    {
        switch(state)
        {
            case EnemyState.MOVING:
                Moving();
                break;

            case EnemyState.READY:
                Ready();
                break;
        }
    }

    /*MOVING METHOD*/
    public void Moving()
    {
        if(chosenPosition == null)
        {
            int randomPosition = Random.Range(0, movingPositions.Length - 1);
            chosenPosition = movingPositions[randomPosition];
        }

        if (chosenPosition.getIsFree())
        {
            isMoving = true; // Set moving variable becuase the enemy is going to move towards the chosen position
            chosenPosition.setIsFree(false); // Set position as not free
        }

        else
        {
            // Debug.Log("Waiting for chosen postition to be free");
        }

        if(isMoving) // Is moving to the chosen position
        {
            transform.position = Vector3.MoveTowards(transform.position, chosenPosition.getPos(), movingSpeed * Time.deltaTime); // Execute movement towards the chosen position

            if (transform.position.Equals(chosenPosition.getPos())) // When the enemy gets to the chosen position its stage is READY
            {
                state = EnemyState.READY;
            }
        }
    }

    /*ATTACK METHOD*/
    public void Ready()
    {
        attackTimeCounter -= Time.deltaTime; // Decrease attackTimeCounter in each FPS

        if(attackTimeCounter < 0) // When attackTimeCounter is over
        {
            attackTimeCounter = attackTime; // Re-initialize attackTime counter with attackTime value

            if (attackEffect.isPlaying)
            {
                attackEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
            else
            {
                attackEffect.Play();
            }
        }
    }

    /*COLLISIONS*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject bullet = collision.gameObject;
        MyBullet mb = bullet.GetComponent<MyBullet>();

        health -= mb.strength;

        animator.SetTrigger("hit");

        // Debug.Log(health);

        if (health <= 0)
        {
            chosenPosition.setIsFree(true);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    /*TIRGGERS*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Player")
        {
            GameObject player = collision.gameObject;
            PlayerMovement pm = player.transform.parent.GetComponent<PlayerMovement>();

            pm.Die();
        }
    }

    /*MAKE ENEMIES LOOK AT THE PLANET*/
    public void LookAt2D()
    {
        Transform planet = GameObject.FindGameObjectWithTag("Planet").transform;

        Vector2 direction = planet.position - this.transform.position;

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }
}
