using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(10, 11);
        Physics2D.IgnoreLayerCollision(9, 12);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
