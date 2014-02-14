using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour
{
    GameObject gravityWell;
    GameObject body;
    Renderer[] renderers;
    Camera portalCam;
    ParticleSystem particles;

    void Start ()
    {
        gravityWell = GameObject.Find("Gravity Orb");
        body = transform.parent.gameObject;
        renderers = GetComponentsInChildren<Renderer>();
        portalCam = GetComponentInChildren<Camera>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(gravityWell.transform.position - body.transform.position);
        if ((WellDataScript.gameWorldRadius - (gravityWell.transform.position - body.transform.position).magnitude) > 500.0f)
        {
            foreach (Renderer r in renderers)
            { r.enabled = false; }
            portalCam.enabled = false;
            particles.enableEmission = false;
        }
        else
        {
            foreach (Renderer r in renderers)
            { r.enabled = true; }
            portalCam.enabled = true;
            particles.enableEmission = true;
        }
	}
}
