using UnityEngine;
using System.Collections;

class ShipController : MonoBehaviour
{
    // Component Variables
    // These are variables used to access different components of the Game Object.
    Camera cam;
    Rigidbody body;
    ConstantForce force;
    UnityEngine.Light[] lights;

    // Control Variables
    // These are used to define controller and keyboard/mouse parameters.
    float mouseSensitivity;
    float buttonThreshold;
    float Torque;
    float Propulsion;

    // Movement Variables
    // These variables dictate the maximum speed and acceleration of the ship.
    float shipMass;

    float maxPropulsionForce;
    float minPropulsionForce;
    float maxPropulsionAccel;
    float maxPropulsionVelocity;
    float propulsionDrag;

    float maxTorqueForce;
    float minTorqueForce;
    float maxTorqueAccel;
    float maxTorqueVelocity;
    float torqueDrag;

    // Orientation Variables
    // These are used to control body orientation towards a desired rotation similar to the camera.
    float targetHorRotation;
    float targetVerRotation;

    Quaternion bodyRot;
    Quaternion camRot;

    // Gravity Well Object
    GameObject gravityWell;
    Vector3 gravityForce;

	// Use this for initialization
	void Start ()
    {
        // Component Variables
        cam = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody>();
        force = GetComponent<ConstantForce>();
        lights = GetComponentsInChildren<Light>();

        // Control Variables
        mouseSensitivity = 8.0f;
        buttonThreshold = 0.05f;
        Torque = 0;
        Propulsion = 0;

        // Movement Variables
        shipMass = 1.0f;

        maxPropulsionForce = 50.0f * shipMass;
        minPropulsionForce = 5.0f;
        maxPropulsionAccel = maxPropulsionForce / shipMass;
        maxPropulsionVelocity = 100.0f;
        propulsionDrag = maxPropulsionAccel / maxPropulsionVelocity;

        maxTorqueForce = 0.25f * shipMass;
        maxTorqueAccel = maxTorqueForce / shipMass;
        maxTorqueVelocity = 0.5f;
        torqueDrag = maxTorqueAccel / maxTorqueVelocity;

        body.drag = 2 * propulsionDrag / (propulsionDrag * Time.fixedDeltaTime + 1);
        body.angularDrag = 6 * torqueDrag / (torqueDrag * Time.fixedDeltaTime + 1);

        // Orientation Variables
        targetHorRotation = 0.0f;
        targetVerRotation = 0.0f;

        // Gravity Well Object
        gravityWell = GameObject.Find("Gravity Orb");
        gravityForce = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        float verRotation = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cam.transform.Rotate(-verRotation, horRotation, 0);
        Quaternion camRot = cam.transform.rotation;

        bodyRot = Quaternion.Slerp(body.transform.rotation, cam.transform.rotation, 4.0f * Time.deltaTime);

        body.transform.rotation = bodyRot;
        cam.transform.rotation = camRot;
	}

    // FixedUpdate is called once every 0.02 seconds.
    void FixedUpdate ()
    {
        foreach (Light l in lights)
        { l.intensity = 8 * (body.velocity.magnitude / maxPropulsionVelocity); }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > buttonThreshold)
        { Torque = Input.GetAxis("Horizontal") / Mathf.Abs(Input.GetAxis("Horizontal")); }
        else { Torque = 0; }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > buttonThreshold)
        { Propulsion = Input.GetAxis("Vertical") / Mathf.Abs(Input.GetAxis("Vertical")); }
        else { Propulsion = 0; }

        force.relativeForce = new Vector3(0, 0, ((maxPropulsionForce - minPropulsionForce) * Propulsion) + minPropulsionForce);
        force.relativeTorque = new Vector3(0, 0, -(maxTorqueForce * Torque));

        if (gravityWell != null)
        {
            gravityForce = gravityWell.transform.position - body.transform.position;
            gravityForce = (gravityForce / gravityForce.magnitude) * (9.8f + ((Mathf.Floor(50.0f / gravityForce.magnitude) / (50.0f / gravityForce.magnitude)) * maxPropulsionForce) + (maxPropulsionForce - 9.8f) * Mathf.Pow((1.0f - ((gravityForce.magnitude - 50.0f) / GravityWellData.mapRadius)), 11.0f));
            force.force = gravityForce;
        }
    }
}
