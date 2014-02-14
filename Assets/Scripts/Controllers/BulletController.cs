using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour {

    // Component Variables
    Rigidbody body;

    public GameObject source;
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
            float angle = Vector3.Angle(body.transform.forward.normalized, (trackedObject.transform.position - body.transform.position).normalized);
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, rotation, (20.0f / angle) * Time.deltaTime);
            body.velocity = body.velocity.magnitude * body.transform.forward;
        }
	}

    void OnCollisionEnter(Collision col)
    {
        ThirdPersonController script = GameObject.Find("Player").GetComponent<ThirdPersonController>();
        script.hits = script.hits + 1;
        if ((col.gameObject.name == "Asteroid(Clone)"))
        {
            Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if ((col.gameObject.name == "Enemy") || (col.gameObject.name == "Enemy(Clone)"))
        {
            EnemyShipController enemyScript = col.gameObject.GetComponent<EnemyShipController>();
            if (enemyScript.health <= 1)
            {
                Destroy(col.gameObject);
                Instantiate(Resources.Load("Ship Explosion"), transform.position, Quaternion.identity);
                ShipSpawnerScript.shipCurCount = ShipSpawnerScript.shipCurCount - 1;
                CrystalController crystalScript = ((GameObject)Instantiate(Resources.Load("Crystal"), transform.position, Quaternion.identity)).GetComponent<CrystalController>();
                crystalScript.trackedObject = source;
            }
            else
            {
                enemyScript.health = enemyScript.health - 1;
                Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else if (col.gameObject.name == "Player")
        {
            Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        { trackedObject = null; }
    }
}
