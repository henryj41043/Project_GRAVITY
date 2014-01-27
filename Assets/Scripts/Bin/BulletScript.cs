using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletScript : MonoBehaviour {

    // Component Variables
    Rigidbody body;

    // Use this for initialization
    void Start()
    {
        //body = GetComponent<Rigidbody>();
        //body.velocity = (body.transform.forward * 10.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
