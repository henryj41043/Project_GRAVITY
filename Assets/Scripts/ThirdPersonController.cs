using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour
{
    // Directional Variables
    // These variables are used for ship rotation and camera control.
    float mouseSensitivity;

    // Force Vectors
    // These Variables are used to determine the net forces acting upon the ship.
    Vector3 GravityForce;
    Vector3 PropulsionForce;
    Vector3 NetForce;

    // Mass Scalar
    float mass;

    // Acceleration Vector
    Vector3 NetAcceleration;

    // Velocity Vector
    Vector3 NetVelocity;

	// Use this for initialization
	void Start ()
    {
        // Directional Variables
        mouseSensitivity = 1;

        // Force Vectors
        GravityForce = Vector3.zero;
        PropulsionForce = Vector3.zero;
        NetForce = Vector3.zero;

        // Mass Scalar
        mass = 0.0f;

        // Acceleration Vector
        NetAcceleration = Vector3.zero;

        // Velocity Vector
        NetVelocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Rigidbody body = GetComponent<Rigidbody>();

        float horRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        float verRotation = Input.GetAxis("Mouse Y") * mouseSensitivity;
        float rolRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        body.transform.Rotate(-rolRotation, -verRotation, horRotation);

        body.transform.Translate(3 * Time.deltaTime, 0, 0);
	}
}
