using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBullet : MonoBehaviour {

    [Header("Properties Settings")]
    public float speed;
    public int strength;
    public float fireRate;

    [Header("Particle Effects Settings")]
    public ParticleSystem bulletParticles;

	// Use this for initialization
	void Start () {

        Instantiate(bulletParticles, transform.position, Quaternion.identity);

        InvokeRepeating("DestroyBullet", 0.0f, 0.3f);

    }
	
	// Update is called once per frame
	void Update () {
		DestroyBullet();
	}

    void DestroyBullet()
    {
        if(transform.position.y > 25 || transform.position.y < -25 || transform.position.x > 40 || transform.position.x < -40)
        {
            Debug.Log("Bullet out of screen!");
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem psTemporal = Instantiate(bulletParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // Destroy(psTemporal, 1);
    }
}
