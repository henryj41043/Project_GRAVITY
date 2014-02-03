using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConstantForce))]
public class GravityWellPhysics : MonoBehaviour
{
    // Component Variables
    Rigidbody body;

    // Gravity Well Object
    GameObject gravityWell;
    Vector3 gravityForce;

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
            gravityForce = (gravityWell.transform.position - body.transform.position);
            gravityForce = gravityForce.normalized * ((WellDataScript.gravityConstant * WellDataScript.gravityWellMass * (float)WellDataScript.gravityPolarity) / Mathf.Pow(gravityForce.magnitude, 2.0f));
            if (gravityForce.magnitude > 100.0f)
            {
                body.constantForce.force = gravityForce;
            }
            else
            { body.constantForce.force = Vector3.zero; }
        }
	}
}
