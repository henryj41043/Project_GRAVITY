using UnityEngine;
using System.Collections;

public class BulletCleaner : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(gameObject, 2.0f);
	}
}
