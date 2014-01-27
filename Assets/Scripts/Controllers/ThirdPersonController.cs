using UnityEngine;
using System.Collections;

class ThirdPersonController : MonoBehaviour
{
    // Script References
    ShipGraphics shipGraphics;
    ShipPropulsionPhysics shipPropulsionPhysics;

    // Component Variables
    Camera cam;
    Rigidbody body;
    Collider collider;

    // Control Variables
    float mouseSensitivity;
    float mouseThreshold;
    float buttonSensitivity;
    float buttonThreshold;

    float horTranslation;
    float verTranslation;

	// Use this for initialization
	void Start ()
    {
        // Script References
        shipGraphics = GetComponent<ShipGraphics>();
        shipPropulsionPhysics = GetComponent<ShipPropulsionPhysics>();

        // Component Variables
        cam = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<Collider>();

        // Control Variables
        mouseSensitivity = 8.0f;
        mouseThreshold = 8.0f;
        buttonSensitivity = 0.05f;
        buttonThreshold = 0.05f;

        horTranslation = 0.0f;
        verTranslation = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Get Input
        float horRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        float verRotation = Input.GetAxis("Mouse Y") * mouseSensitivity;
        horTranslation = Input.GetAxis("Horizontal");
        verTranslation = Input.GetAxis("Vertical");

        if (verTranslation < 0.0f)
        { verTranslation = 0.0f; }

        // Rotate Camera
        cam.transform.Rotate(-verRotation, horRotation, 0);
        Quaternion camRot = cam.transform.rotation;

        // Rotate Body
        Quaternion bodyPreRot = body.transform.rotation;
        Quaternion bodyRot = Quaternion.Slerp(body.transform.rotation, cam.transform.rotation, 2.0f * Time.deltaTime);
        body.transform.rotation = bodyRot;

        // Realign Camera
        cam.transform.rotation = camRot;

        // Draw Special FX
        shipGraphics.Draw(bodyPreRot, body.transform.rotation, verTranslation);

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = (GameObject)Instantiate(Resources.Load("Bullet"), body.transform.position, body.transform.rotation);
            Physics.IgnoreCollision(projectile.GetComponentInChildren<CapsuleCollider>(), collider);
            projectile.rigidbody.velocity = (projectile.rigidbody.transform.forward * (body.velocity.magnitude + 100));
        }
	}

    // FixedUpdate is called once every 0.02 seconds.
    void FixedUpdate()
    {
        //Apply Physics
        shipPropulsionPhysics.ApplyForces(horTranslation, verTranslation);
    }
}
