using UnityEngine;
using System.Collections;

class EnemyShipController : MonoBehaviour
{
    // Script References
    ShipGraphics shipGraphics;
    ShipPropulsionPhysics shipPropulsionPhysics;

    // Component Variables
    GameObject enemyCam;
    Rigidbody body;
    Collider capsuleCollider;
    Collider boxCollider;
    AudioSource lockSound;

    // Control Variables
    //float mouseSensitivity;
    //float mouseThreshold;
    //float buttonSensitivity;
    //float buttonThreshold;

    float horTranslation;
    float verTranslation;

    float shootWaitTime;
    float targetTime;

    GameObject trackingResults;
    GameObject trackedObject;

	// Use this for initialization
	void Start ()
    {
        Screen.lockCursor = true;

        // Script References
        shipGraphics = GetComponent<ShipGraphics>();
        shipPropulsionPhysics = GetComponent<ShipPropulsionPhysics>();

        // Component Variables
        enemyCam = transform.Find("EnemyCamera").gameObject;
        body = GetComponent<Rigidbody>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        lockSound = GetComponent<AudioSource>();

        // Control Variables
        //mouseSensitivity = 8.0f;
        //mouseThreshold = 8.0f;
        //buttonSensitivity = 0.05f;
        //buttonThreshold = 0.05f;

        horTranslation = 0.0f;
        verTranslation = 1.0f;

        trackedObject = null;
        trackingResults = null;
        shootWaitTime = 0.0f;
        targetTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Get Input
        //float horRotation = Input.GetAxis("X"); // * mouseSensitivity;
        //float verRotation = Input.GetAxis("Y"); // * mouseSensitivity;
        //horTranslation = Input.GetAxis("Horizontal");
        //verTranslation = Input.GetAxis("Vertical");

        //if (verTranslation < 0.0f)
        //{ verTranslation = 0.0f; }

        // Rotate Enemy Target Direction
        Vector3 Fn = (GameObject.Find("Player").transform.position - body.transform.position).normalized * 100.0f;
        if (GameObject.Find("Gravity Orb") != null)
        {
            Vector3 Fg = (GameObject.Find("Gravity Orb").transform.position - body.transform.position);
            Fg = Fg.normalized * ((GravityWellData.gravityConstant * GravityWellData.gravityWellMass * (float)GravityWellData.gravityPolarity) / Mathf.Pow(Fg.magnitude, 2.0f));

            enemyCam.transform.rotation = Quaternion.LookRotation(Fn - Fg);
        }
        else
        {
            enemyCam.transform.rotation = Quaternion.LookRotation(Fn);
        }

        // Rotate Body
        Quaternion bodyPreRot = body.transform.rotation;
        Quaternion bodyRot = Quaternion.Lerp(body.transform.rotation, enemyCam.transform.rotation, 3.0f * Time.deltaTime);
        body.transform.rotation = bodyRot;

        // Draw Special FX
        shipGraphics.Draw(bodyPreRot, enemyCam.transform.rotation, verTranslation);

        // Track for Potential Targets
        trackingResults = TargetLockScript.GetTargets(body);
        if (trackingResults != null)
        {
            if (trackedObject == null)
            { lockSound.Play(); }
            trackedObject = trackingResults;
            targetTime = 0.25f;

            //Command to shoot
            if (shootWaitTime > 0.0f)
            { shootWaitTime = shootWaitTime - Time.deltaTime; }
            else
            {
                ShootingScript.FireBullet(body, capsuleCollider, boxCollider, trackedObject);
                shootWaitTime = 0.1f;
            }
        }
        else if (targetTime > 0.0f)
        { targetTime = targetTime - Time.deltaTime; }
        else
        { trackedObject = null; }
	}

    // FixedUpdate is called once every 0.02 seconds.
    void FixedUpdate()
    {
        //Apply Physics
        shipPropulsionPhysics.ApplyForces(horTranslation, verTranslation);
    }
}
