using UnityEngine;
using System.Collections;

public class ExplosionCleaner : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(gameObject, 2.5f);
	}
}
