using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConstantForce))]
public class ShipPropulsionPhysics : MonoBehaviour
{
    // Component Variables
    Rigidbody body;
    ConstantForce force;

    // Movement Variables
    // These variables dictate the maximum speed and acceleration of the ship.
    float maxPropulsionForce;
    float maxPropulsionAccel;
    float maxPropulsionVelocity;
    float propulsionDrag;

    float maxTorqueForce;
    float maxTorqueAccel;
    float maxTorqueVelocity;
    float torqueDrag;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
        force = GetComponent<ConstantForce>();

        // Movement Variables
        maxPropulsionForce = 100.0f;
        maxPropulsionAccel = maxPropulsionForce;
        maxPropulsionVelocity = 300.0f;
        propulsionDrag = maxPropulsionAccel / maxPropulsionVelocity;

        maxTorqueForce = 25f;
        maxTorqueAccel = maxTorqueForce;
        maxTorqueVelocity = 50f;
        torqueDrag = maxTorqueAccel / maxTorqueVelocity;

        body.drag = propulsionDrag / (propulsionDrag * Time.fixedDeltaTime + 1);
        body.angularDrag = 6 * torqueDrag / (torqueDrag * Time.fixedDeltaTime + 1);
	}

    // FixedUpdate is called once every 0.02 seconds.
    public void ApplyForces (float horizontal, float vertical)
    {
        float Torque = horizontal;
        float Propulsion = vertical;

        force.relativeForce = new Vector3(0, 0, (maxPropulsionForce * Propulsion));
        force.relativeTorque = new Vector3(0, 0, -(maxTorqueForce * Torque));
    }
}
