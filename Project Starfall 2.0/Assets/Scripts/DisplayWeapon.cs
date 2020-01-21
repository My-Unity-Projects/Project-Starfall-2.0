using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWeapon : MonoBehaviour {

    MyWeapon mw;

    [Header("UI Settings")]
    public Image sliderImg;
    public Text ammoTxt;
    public Image weaponImg;


	// Use this for initialization
	void Start () {

        /*Color newCol;
        ColorUtility.TryParseHtmlString(weapon.color, out newCol);
        sliderImg.color = newCol;

        ammoTxt.color = newCol;


        weaponImg.sprite = weapon.sprite;*/

    }
	
    public void Display(GameObject weapon)
    {
        mw = weapon.GetComponent<MyWeapon>();
        Color newCol;
        ColorUtility.TryParseHtmlString(mw.color, out newCol);
        sliderImg.color = newCol;

        ammoTxt.color = newCol;

        weaponImg.sprite =  mw.sprite;
    }
}
