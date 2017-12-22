using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

    public float activeTime;

	// Use this for initialization
	void Awake ()
    {
        // Destroy the gameObject that this script is attached to over this amount of time.
        Destroy(gameObject, activeTime);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
