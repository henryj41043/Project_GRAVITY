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
            Debug.Log("Distance: " + gravityForce.magnitude + " m");
            gravityForce = gravityForce.normalized * ((GravityWellData.gravityConstant * GravityWellData.gravityWellMass * (float)GravityWellData.gravityPolarity) / Mathf.Pow(gravityForce.magnitude, 2.0f));
            body.constantForce.force = gravityForce;
        }
	}
}
