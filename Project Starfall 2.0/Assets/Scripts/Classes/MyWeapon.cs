using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWeapon : MonoBehaviour {

    [Header("Weapon Properties")]
    public float fireRate;
    public int ammo;
    public GameObject bullet;

    [Header("UI Properties")]
    public string color;
    public Sprite sprite;
}
