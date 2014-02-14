using UnityEngine;
using System.Collections;

public class PortalCamScript : MonoBehaviour
{
    GameObject body;

    void Start()
    { body = GameObject.Find("Player Camera"); }

    // Update is called once per frame
    void LateUpdate()
    { transform.rotation = body.transform.rotation; }
}
