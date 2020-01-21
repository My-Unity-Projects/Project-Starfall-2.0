using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCrystal : MonoBehaviour
{
    Material shineMaterial;
    ParticleSystem ps;

    float shineTemp = 0;
    float shineDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        shineMaterial = this.gameObject.GetComponent<SpriteRenderer>().material;
        ps = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Shine();
    }

    // Shine Animation

    public void Shine()
    {
        shineDelay -= Time.deltaTime;

        if (shineDelay <= 0)
        {
            shineTemp += Time.deltaTime;

            if (shineTemp >= 1)
            {
                shineTemp = 0;
                shineDelay = 1;
            }

            else
            {
                shineMaterial.SetFloat("_ShineLocation", shineTemp);
            }
        }
    }

    // Collisions

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        // The crystal object is picked up by the player
        if(tag == "Player")
        {
            ps.Play();
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
