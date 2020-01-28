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
    public float timeBetweenAttacks;
    public float attackTime;
    private float timeCounter;
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
        timeCounter = timeBetweenAttacks; // Initialize our counter with attackTime value

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
        timeCounter -= Time.deltaTime; // Decrease timeCounter in each FPS

        if(timeCounter < 0) // When timeCounter is over
        {

            if (attackEffect.isPlaying)
            {
                timeCounter = timeBetweenAttacks; // Re-initialize timeCounter with the timeBetweenAttacks value
                attackCollider.enabled = false;
                attackEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
            else
            {
                timeCounter = attackTime; // Re-initialize timeCounter with the attackTime value
                attackCollider.enabled = true;
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
