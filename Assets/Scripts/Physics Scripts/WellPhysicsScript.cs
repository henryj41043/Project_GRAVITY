using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConstantForce))]
public class WellPhysicsScript : MonoBehaviour
{
    // Component Variables
    Rigidbody body;

    // Gravity Well Object
    GameObject gravityWell;
    Vector3 gravityForce;
    Vector3 distance;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();

        gravityWell = GameObject.Find("Gravity Orb");
        gravityForce = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (gravityWell != null)
        {
            distance = (gravityWell.transform.position - body.transform.position);
            gravityForce = distance.normalized * ((WellDataScript.gravityConstant * WellDataScript.gravityWellMass * (float)WellDataScript.gravityPolarity) / Mathf.Pow(distance.magnitude, 2.0f));
            if (distance.magnitude > WellDataScript.gameWorldRadius)
            {
                if ((gameObject.name == "Player") || (gameObject.name == "Enemy(Clone)"))
                {
                    ParticleSystem[] systems = gameObject.GetComponentsInChildren<ParticleSystem>();
                    foreach (ParticleSystem p in systems)
                    { p.enableEmission = false; }
                    body.transform.position = body.transform.position + (distance.normalized * 2 * WellDataScript.gameWorldRadius);
                    body.velocity = body.velocity.normalized * 1000.0f;
                    if (gameObject.name == "Player")
                    {
                        TwirlEffect twirlEffect = gameObject.GetComponentInChildren<TwirlEffect>();
                        twirlEffect.angle = 350.0f;
                    }
                    //body.transform.rotation = Quaternion.LookRotation(gravityWell.transform.position - body.transform.position);
                }
            }
            else
            {
                if ((gameObject.name == "Player") || (gameObject.name == "Enemy(Clone)"))
                {
                    ParticleSystem[] systems = gameObject.GetComponentsInChildren<ParticleSystem>();
                    foreach (ParticleSystem p in systems)
                    { p.enableEmission = true; }
                }

                if (gravityForce.magnitude > 100.0f)
                {
                    body.constantForce.force = gravityForce;
                }
                else
                {
                    body.constantForce.force = Vector3.zero;
                }
            }
        }
	}
}
