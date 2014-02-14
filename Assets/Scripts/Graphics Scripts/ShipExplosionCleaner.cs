using UnityEngine;
using System.Collections;

public class ShipExplosionCleaner : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(gameObject, 4.0f);
	}
}
