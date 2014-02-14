using UnityEngine;
using System.Collections;

class EnemyShipController : MonoBehaviour
{
    // Script References
    ShipGraphics shipGraphics;
    ShipPhysicsScript shipPhysicsScript;

    // Component Variables
    GameObject playerShip;
    GameObject enemyCam;
    Rigidbody body;
    GameObject model;
    Collider capsuleCollider;
    Collider boxCollider;
    AudioSource lockSound;

    // Control Variables
    //float mouseSensitivity;
    //float mouseThreshold;
    //float buttonSensitivity;
    //float buttonThreshold;

    GameObject[] signatures;
    GameObject target;

    float horTranslation;
    float verTranslation;

    float shootWaitTime;
    float targetTime;

    GameObject trackingResults;
    GameObject trackedObject;

    public int health;

	// Use this for initialization
	void Start ()
    {
        Screen.lockCursor = true;

        // Script References
        shipGraphics = GetComponent<ShipGraphics>();
        shipPhysicsScript = GetComponent<ShipPhysicsScript>();

        // Component Variables
        playerShip = GameObject.Find("Player");
        enemyCam = transform.Find("EnemyCamera").gameObject;
        body = GetComponent<Rigidbody>();
        model = transform.Find("Model").gameObject;
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        lockSound = GetComponent<AudioSource>();

        // Control Variables
        //mouseSensitivity = 8.0f;
        //mouseThreshold = 8.0f;
        //buttonSensitivity = 0.05f;
        //buttonThreshold = 0.05f;

        signatures = null;
        target = null;

        horTranslation = 0.0f;
        verTranslation = 1.0f;

        trackedObject = null;
        trackingResults = null;
        shootWaitTime = 0.0f;
        targetTime = 0.0f;

        health = 10;
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

        if (target == null)
        {
            model.tag = "Untagged";

            GameObject[] signatures = GameObject.FindGameObjectsWithTag("Signature");
            float bestAngle = -1.0f;

            foreach (GameObject s in signatures)
            {
                float curAngle = Vector3.Angle(body.transform.forward.normalized, (s.transform.position - body.transform.position).normalized);

                if (curAngle <= 60)
                {
                    if (target == null)
                    {
                        target = s;
                        bestAngle = curAngle;
                    }
                    else if (curAngle < bestAngle)
                    {
                        target = s;
                        bestAngle = curAngle;
                    }
                }
            }

            if (target == null)
            {
                if (signatures.Length > 0)
                {
                    target = signatures[Random.Range(0, signatures.Length)];
                }
                else
                {
                    target = playerShip;
                }
            }

            model.tag = "Signature";
        }

        // Rotate Enemy Target Direction
        Vector3 Fn = (target.transform.position - body.transform.position).normalized * 100.0f;
        //if (GameObject.Find("Gravity Orb") != null)
        //{
        //    Vector3 Fg = (GameObject.Find("Gravity Orb").transform.position - body.transform.position);
        //    Fg = Fg.normalized * (2.0f * (GravityWellData.gravityConstant * GravityWellData.gravityWellMass * (float)GravityWellData.gravityPolarity) / Mathf.Pow(Fg.magnitude, 2.0f));
        //
        //    enemyCam.transform.rotation = Quaternion.LookRotation(Fn - Fg);
        //}
        //else
        //{
              enemyCam.transform.rotation = Quaternion.LookRotation(Fn);
        //}

        // Rotate Body
        Quaternion bodyPreRot = body.transform.rotation;
        float angle = Vector3.Angle(body.transform.forward.normalized, (enemyCam.transform.rotation * body.transform.forward).normalized);
        Quaternion bodyRot = Quaternion.Lerp(body.transform.rotation, enemyCam.transform.rotation, Mathf.Clamp((360.0f / angle) * Time.deltaTime, 0.0f, 1.0f));
        body.transform.rotation = bodyRot;

        if (Vector3.Angle(body.transform.forward.normalized, (target.transform.position - body.transform.position).normalized) > 60.0f)
        { target = null; }

        // Draw Special FX
        shipGraphics.Draw(bodyPreRot, enemyCam.transform.rotation, verTranslation);

        // Track for Potential Targets
        trackingResults = TargetLockScript.GetTargets(body, model);
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
        shipPhysicsScript.ApplyForces(horTranslation, verTranslation);
    }
}
