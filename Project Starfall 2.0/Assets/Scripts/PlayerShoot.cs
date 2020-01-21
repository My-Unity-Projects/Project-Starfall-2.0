using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {

    [Header("Weapons")]
    public GameObject[] guns;
    GameObject equippedWeapon;
    MyWeapon mw;

    [Header("Equipped Weapon Properties")]
    float fireRate;

    [Header("Equipped Bullet Properties")]
    GameObject bullet;
    MyBullet mb;
    float speed;
    // float strength;

    [Header("Spawn Position Settings")]
    public GameObject bulletSpawn;

    [Header("Transition Settings")]
    public bool isShooting;
    float rotationSpeedTemp;

    [Header("Animation Settings")]
    public Animator playerAni;

    [Header("Movement Settings")]
    PlayerMovement pm;

    [Header("UI Settings")]
    public Slider powerSld;
    DisplayWeapon dw;


    private int power = 0;
    private float powerDownTimeCounter;
    public float powerDownTime;
    public GameObject[] bullets;
    public Image fill;
    public Text powerTxt;
    public Text powerValueTxt;


    // Use this for initialization
    void Start () {

        // Setting up the player movement script to access to the movement variables
        pm = transform.parent.GetComponent<PlayerMovement>();

        // Setting up the display weapon script to update the interface
        dw = GameObject.Find("GameManager").GetComponent<DisplayWeapon>();

        // Setting up the default weapon
        // SetUpGun("Default Gun");

        // Save the default speed of the player in a variable
        rotationSpeedTemp = pm.rotationSpeed;

        // Initialize powerDownTimeCounter
        powerDownTimeCounter = powerDownTime;
    }
	
	// Update is called once per frame
	void Update () {

        Shoot();
        SetUpBullet();
        // SliderValueDown();
	}

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {          
            pm.rotationSpeed = 0;
            isShooting = true;
            playerAni.SetBool("isShooting", true);
            playerAni.speed = fireRate;
        }

        else
        {
            pm.rotationSpeed = rotationSpeedTemp;
            isShooting = false;
            playerAni.SetBool("isShooting", false);
            playerAni.speed = 1;
        }

        powerSld.value = power;
        powerValueTxt.text = (power / 10).ToString();
    }

    public void InstantiateBullet()
    {
        Rigidbody2D bulletInstance;
        bulletInstance = Instantiate(bullet.GetComponent<Rigidbody2D>(), bulletSpawn.transform.position, Quaternion.Euler(transform.parent.localEulerAngles));
        bulletInstance.name = "Bullet(Clone)";
        bulletInstance.velocity = transform.up * speed; 
        
        if(power > 0)
        {
            power-=20;
        }
    }

    // Set up new Weapon
    public void SetUpGun(string tag)
    {
        // Guns
        if(tag == "Default Gun")
        {
            equippedWeapon = guns[0];
        }

        if(tag == "Green Gun")
        {
            // Setting up the green gun
            equippedWeapon = guns[1];
        }

        if(tag == "Blue Gun")
        {
            // Setting up the blue gun
            equippedWeapon = guns[2];
        }

        if(tag == "Purple Gun")
        {
            // Setting up the purple gun
            equippedWeapon = guns[3];
        }

        if (tag == "Orange Gun")
        {
            // Setting up the orange gun
            equippedWeapon = guns[4];
        }

        if (tag == "Red Gun")
        {
            // Setting up the red gun
            equippedWeapon = guns[5];
        }

        // Setting up the script to access to the weapon properties
        mw = equippedWeapon.GetComponent<MyWeapon>();

        // Setting up the fireRate and ammo of the new weapon
        fireRate = mw.fireRate;

        // Setting up the bullet of the new weapon
        bullet = mw.bullet;
        mb = bullet.GetComponent<MyBullet>();

        // Setting up the speed and strength of new the bullet
        speed = mb.speed;
        // strength = mb.strength;

        // Update Interface
        powerSld.maxValue = mw.ammo;
        powerSld.value = mw.ammo;
        dw.Display(equippedWeapon);
    }

    // Set Up Bullet
    public void SetUpBullet()
    {
        Color newColor = Color.grey;
        // Red Bullet
        if(power >= 950)
        {
            bullet = bullets[0];
            newColor = new Color(1, 0, .1882353f, 1);
        }

        // Orange Bullet
        else if(power >= 800 && power < 950)
        {
            bullet = bullets[1];
            newColor = new Color(1, 0.509804f, 0, 1);
        }

        // Purple Bullet
        else if(power >= 600 && power < 800)
        {
            bullet = bullets[2];
            newColor = new Color(0.6352941f, 0, 1, 1);
        }

        // Blue Bullet
        else if(power >= 350 && power < 600)
        {
            bullet = bullets[3];
            newColor = new Color(0, 0.8666667f, 1, 1);
        }

        // Green Bullet
        else if(power >= 100 && power < 350)
        {
            bullet = bullets[4];
            newColor = new Color(0.01960784f, 1, 0, 1);
        }

        // Grey Bullet
        else if(power < 100)
        {
            bullet = bullets[5];
            newColor = Color.grey;
        }

        mb = bullet.GetComponent<MyBullet>();

        speed = mb.speed;
        fireRate = mb.fireRate;
        // strength = mb.strength;

        fill.color = newColor;
        powerTxt.color = newColor;
        powerValueTxt.color = newColor;
    }

    public void SliderValueDown()
    {
        if(power > 0)
        {
            powerDownTimeCounter -= Time.deltaTime;

            if(powerDownTimeCounter < 0)
            {
                power -= 1;
                // SetPowerDownTemp();
                powerDownTimeCounter = powerDownTime;
            }
        }

        else
        {
            powerDownTimeCounter = powerDownTime;
        }

        powerSld.value = power;
        powerValueTxt.text = (power/10).ToString();
    }

    public void SetPowerDownTemp()
    {
        if (power > 100)
        {
            powerDownTimeCounter = 0.01f;
        }

        else if (power < 100)
        {
            powerDownTimeCounter = 0.02f;
        }
    }


    // Pick up weapon
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;

        if(tag == "Green Gun" || tag == "Blue Gun" || tag == "Purple Gun" || tag == "Orange Gun" || tag == "Red Gun")
        {
            Destroy(collision.gameObject);
            SetUpGun(tag);
        }

        if(tag == "Crystal")
        {
            if(name == "Green Crystal(Clone)")
            {
                power += 60;
            }

            if (name == "Blue Crystal(Clone)")
            {
                power += 80;
            }

            if (name == "Purple Crystal(Clone)")
            { 
                power += 100;
            }

            if (name == "Orange Crystal(Clone)")
            {
                power += 120;
            }

            if (name == "Red Crystal(Clone)")
            {
                power += 160;
            }

            if(power > 1000)
            {
                power = 1000;
            }
        }
    }
}
