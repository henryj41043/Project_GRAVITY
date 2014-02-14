using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CrystalController : MonoBehaviour {

    // Component Variables
    Rigidbody body;

    public GameObject trackedObject;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (trackedObject != null)
        {
            body.transform.rotation = Quaternion.LookRotation(trackedObject.transform.position - body.transform.position);
            body.velocity = 500.0f * body.transform.forward;
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject == trackedObject))
        {
            if (col.gameObject.name == "Player")
            {
                ThirdPersonController script = col.gameObject.GetComponent<ThirdPersonController>();
                script.crystals = script.crystals + 1;
            }
            Instantiate(Resources.Load("Crystal Capture"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
