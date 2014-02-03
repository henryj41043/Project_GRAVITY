using UnityEngine;
using System.Collections;

public class SignatureScript : MonoBehaviour {

    public Camera playerCam;

    void Start()
    {
        playerCam = GameObject.Find("Player").GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate Decal to face the Player Camera.
        if (playerCam != null)
        {
            transform.localScale = Vector3.one / 500 * ((transform.position - playerCam.transform.position).magnitude) * Mathf.Tan(Mathf.Deg2Rad * 40.0f);
        }
    }
}
