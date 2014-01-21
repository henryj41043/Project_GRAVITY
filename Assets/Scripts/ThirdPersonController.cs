using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour
{
    // Movement Variables
    // These variables dictate the maximum speed and acceleration of the ship.
    public float shipMass;

    public float maxPropulsionForce;
    public float minPropulsionForce;
    public float maxPropulsionAccel;
    public float maxPropulsionVelocity;
    public float curPropulsionVelocity;
    public float propulsionDrag;

    public float maxTorqueForce;
    public float minTorqueForce;
    public float maxTorqueAccel;
    public float maxTorqueVelocity;
    public float curTorqueVelocity;
    public float torqueDrag;

    // Component Variables
    // These are variables used to access different components of the Game Object.
    public Camera cam;
    public Rigidbody body;
    public ConstantForce force;
    public UnityEngine.Light[] lights;

    // Control Variables
    // These are used to define controller and keyboard/mouse parameters.
    public float mouseSensitivity;
    public float buttonThreshold;
    public float TorqueX;
    public float TorqueY;
    public float TorqueZ;
    public float Propulsion;

    // Orientation Variables
    // These are used to control body orientation towards a desired rotation similar to the camera.
    public float targetHorRotation;
    public float targetVerRotation;

    public Quaternion bodyRot;
    public Quaternion camRot;

	// Use this for initialization
	void Start ()
    {
        // Movement Variables
        shipMass = 1.0f;

        maxPropulsionForce = 50.0f * shipMass;
        minPropulsionForce = 5.0f;
        maxPropulsionAccel = maxPropulsionForce / shipMass;
        maxPropulsionVelocity = 100.0f;
        curPropulsionVelocity = 0.0f;
        propulsionDrag = maxPropulsionAccel / maxPropulsionVelocity;

        maxTorqueForce = 0.25f * shipMass;
        maxTorqueAccel = maxTorqueForce / shipMass;
        maxTorqueVelocity = 0.5f;
        curTorqueVelocity = 0.0f;
        torqueDrag = maxTorqueAccel / maxTorqueVelocity;

        // Component Variables
        cam = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody>();
        force = GetComponent<ConstantForce>();
        lights = GetComponentsInChildren<Light>();

        // Control Variables
        mouseSensitivity = 8.0f;
        buttonThreshold = 0.05f;
        TorqueX = 0;
        TorqueY = 0;
        TorqueZ = 0;
        Propulsion = 0;

        // Orientation Variables
        targetHorRotation = 0.0f;
        targetVerRotation = 0.0f;
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
        { TorqueZ = Input.GetAxis("Horizontal") / Mathf.Abs(Input.GetAxis("Horizontal")); }
        else { TorqueZ = 0; }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > buttonThreshold)
        { Propulsion = Input.GetAxis("Vertical") / Mathf.Abs(Input.GetAxis("Vertical")); }
        else { Propulsion = 0; }

        force.relativeForce = new Vector3(0, 0, ((maxPropulsionForce - minPropulsionForce) * Propulsion) + minPropulsionForce);
        force.relativeTorque = new Vector3(0, 0, -(maxTorqueForce * TorqueZ));

        body.drag = 2 * propulsionDrag / (propulsionDrag * Time.fixedDeltaTime + 1);
        body.angularDrag = 6 * torqueDrag / (torqueDrag * Time.fixedDeltaTime + 1);

        curPropulsionVelocity = body.velocity.magnitude;
        curTorqueVelocity = body.angularVelocity.magnitude;
    }
}
