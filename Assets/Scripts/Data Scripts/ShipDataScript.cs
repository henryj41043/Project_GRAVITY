using UnityEngine;
using System.Collections;

public class ShipDataScript : MonoBehaviour
{
    // Script References
    ShipGraphics shipGraphics;
    ShipPhysicsScript shipPhysicsScript;

    // Component Variables
    Camera cam;
    MeshFilter reticle;
    GameObject playerShip;
    GameObject enemyCam;
    Rigidbody body;
    GameObject model;
    Collider capsuleCollider;
    Collider boxCollider;
    AudioSource lockSound;

    // Control Variables
    float mouseSensitivity;
    //float mouseThreshold;
    //float buttonSensitivity;
    //float buttonThreshold;

    Material enemyNoLock;
    Material enemyLock;

    float shootWaitTime;
    float targetTime;
    public int hits;

    GameObject[] signatures;
    GameObject target;

    float horTranslation;
    float verTranslation;

    GameObject trackingResults;
    GameObject trackedObject;

    // Use this for initialization
    void Start()
    {
        // Script References
        shipGraphics = GetComponent<ShipGraphics>();
        shipPhysicsScript = GetComponent<ShipPhysicsScript>();

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
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
