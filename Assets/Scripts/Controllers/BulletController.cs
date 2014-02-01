using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour {

    // Component Variables
    Rigidbody body;

    public GameObject trackedObject;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();

        if (trackedObject != null)
        {
            body.transform.rotation = Quaternion.LookRotation(trackedObject.transform.position - body.transform.position);
            body.velocity = body.velocity.magnitude * body.transform.forward;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (trackedObject != null)
        {
            Quaternion rotation = Quaternion.LookRotation(trackedObject.transform.position - body.transform.position);
            //body.transform.rotation = Quaternion.Lerp(body.transform.rotation, rotation, 0.2f * Time.deltaTime);
            body.transform.rotation = Quaternion.LookRotation(trackedObject.transform.position - body.transform.position);
            body.velocity = body.velocity.magnitude * body.transform.forward;
        }
	}

    void OnCollisionEnter(Collision col)
    {
        ThirdPersonController script = GameObject.Find("Player").GetComponent<ThirdPersonController>();
        script.hits = script.hits + 1;
        if ((col.gameObject.name == "Asteroid(Clone)"))
        {
            GameObject explosion = Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
        else if ((col.gameObject.name == "Enemy") || (col.gameObject.name == "Enemy(Clone)") || (col.gameObject.name == "Player"))
        {
            GameObject explosion = Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
        else
        { trackedObject = null; }
    }
}
