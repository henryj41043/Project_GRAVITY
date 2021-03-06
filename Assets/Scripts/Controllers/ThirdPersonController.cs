﻿using UnityEngine;
using System.Collections;

class ThirdPersonController : MonoBehaviour
{
    // Script References
    ShipGraphics shipGraphics;
    ShipPhysicsScript shipPhysicsScript;
    TwirlEffect twirlEffect;

    // Component Variables
    Camera cam;
    Rigidbody body;
    GameObject model;
    Collider capsuleCollider;
    Collider boxCollider;
    AudioSource lockSound;
    public MeshFilter reticle;

    // Control Variables
    float mouseSensitivity;
    //float mouseThreshold;
    //float buttonSensitivity;
    //float buttonThreshold;

    float horTranslation;
    float verTranslation;

    Material enemyNoLock;
    Material enemyLock;

    float shootWaitTime;
    float targetTime;
    public int hits;
    public int crystals;

    GameObject trackingResults;
    GameObject trackedObject;

	// Use this for initialization
	void Start ()
    {
        // Script References
        shipGraphics = GetComponent<ShipGraphics>();
        shipPhysicsScript = GetComponent<ShipPhysicsScript>();
        twirlEffect = GetComponentInChildren<TwirlEffect>();

        // Component Variables
        cam = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody>();
        model = transform.Find("Model").gameObject;
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        lockSound = GetComponent<AudioSource>();

        // Control Variables
        mouseSensitivity = 8.0f;
        //mouseThreshold = 8.0f;
        //buttonSensitivity = 0.05f;
        //buttonThreshold = 0.05f;

        horTranslation = 0.0f;
        verTranslation = 0.0f;

        enemyNoLock = (Material)Resources.Load("Crosshair");
        enemyLock = (Material)Resources.Load("Crosshair_Enemy");

        trackedObject = null;
        trackingResults = null;
        shootWaitTime = 0.0f;
        targetTime = 0.0f;
        hits = 0;
        crystals = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (twirlEffect.angle < 0.0f)
        { twirlEffect.angle = 0.0f; }
        else if (twirlEffect.angle > 0.0f)
        { twirlEffect.angle = twirlEffect.angle - (400.0f * Time.deltaTime); }

        Screen.lockCursor = true;

        // Get Input
        float horRotation = Input.GetAxis("X") * mouseSensitivity;
        float verRotation = Input.GetAxis("Y") * mouseSensitivity;
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

        // Track for Potential Targets
        trackingResults = TargetLockScript.GetTargets(body, model);

        if (trackingResults != null)
        {
            if (trackedObject == null)
            { lockSound.Play(); }
            trackedObject = trackingResults;
            reticle.renderer.material = enemyLock;
            targetTime = 0.25f;
        }
        else if (targetTime > 0.0f)
        {
            targetTime = targetTime - Time.deltaTime;
        }
        else
        {
            trackedObject = null;
            reticle.renderer.material = enemyNoLock;
        }

        // Command to Shoot
        if (shootWaitTime > 0.0f)
        { shootWaitTime = shootWaitTime - Time.deltaTime; }
        else if (Input.GetButton("Fire") || (Input.GetAxis("Fire") < 0.0f))
        {
            ShootingScript.FireBullet(body, capsuleCollider, boxCollider, trackedObject);
            shootWaitTime = 0.05f;
        }
	}

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Project: GRAVITY\nWeb Build 5\n----------------------\nBullets Hit: " + hits + "\nCrystals Collected: " + crystals);
        if (trackedObject != null)
        {
            GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.normal.textColor = Color.red;
            centeredStyle.alignment = TextAnchor.UpperCenter;
            float angle = Vector3.Angle(body.transform.forward.normalized, (trackedObject.transform.position - body.transform.position).normalized);
            GUI.Label(new Rect(0, Screen.height / 2, Screen.width, Screen.height),"-- TARGET LOCKED --", centeredStyle);
        }
    }

    // FixedUpdate is called once every 0.02 seconds.
    void FixedUpdate()
    {
        //Apply Physics
        shipPhysicsScript.ApplyForces(horTranslation, Mathf.Max(0.5f, verTranslation));
    }
}
