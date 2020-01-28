using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Movement Settings")]
    public int direction;
    public float rotationSpeed;
    bool isRunning;

    [Header("Shoot Settings")]
    PlayerShoot ps;

    [Header("Animation Settings")]
    public Animator ani;

    [Header("Particle System Settings")]
    public ParticleSystem runParticles;



    // Use this for initialization
    void Start () {

        direction = 0;
        ps = ani.transform.GetComponent<PlayerShoot>();
	}
	
	// Update is called once per frame
	void Update () {

        Movement();
	}

    void Movement()
    {
        Vector3 currentRotation = transform.eulerAngles;
        Vector3 targetRotation = transform.eulerAngles;

        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !ps.isShooting)
        {
            if(!runParticles || !ani) { return; }

            runParticles.Play();         

            // Left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                targetRotation = Vector3.Lerp(transform.eulerAngles, transform.eulerAngles + new Vector3(0, 0, 45), Time.deltaTime * rotationSpeed);
                direction = -1;
                ani.SetBool("isRunningLeft", true);
                ani.SetBool("isRunningRight", false);
                
            }

            // Right
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                targetRotation = Vector3.Lerp(transform.eulerAngles, transform.eulerAngles + new Vector3(0, 0, -45), Time.deltaTime * rotationSpeed);
                direction = 1;
                ani.SetBool("isRunningLeft", false);
                ani.SetBool("isRunningRight", true);
            }
        }

        else
        {
            ani.SetBool("isRunningLeft", false);
            ani.SetBool("isRunningRight", false);
            direction = 0;
            runParticles.Stop();
        }

        transform.eulerAngles = targetRotation;
    }

    public void Die()
    {
        Debug.Log("You Died!");
    }
}
