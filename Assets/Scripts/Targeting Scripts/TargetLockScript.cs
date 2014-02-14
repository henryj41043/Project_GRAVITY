using UnityEngine;
using System.Collections;

public class TargetLockScript : MonoBehaviour
{
	// Update is called once per frame
	public static GameObject GetTargets (Rigidbody body, GameObject localSignature)
    {
        float angle;
        float bestAngle = -1; // Unassigned

        localSignature.tag = "Untagged";

        RaycastHit targets;
        GameObject trackedObject = null;
        GameObject[] signatures = GameObject.FindGameObjectsWithTag("Signature");

        foreach (GameObject s in signatures)
        {
            //Debug.DrawLine(body.transform.position, body.transform.position + body.transform.forward * 2000.0f, Color.red);
            angle = Vector3.Angle(body.transform.forward.normalized, (s.transform.position - body.transform.position).normalized);

            if (angle < 1.5f)
            {
                if ((s.transform.position - body.transform.position).sqrMagnitude <= 49000000.0f)
                {
                    if (trackedObject == null)
                    {
                        trackedObject = s;
                        bestAngle = angle;
                    }
                    else if (angle < bestAngle)
                    {
                        trackedObject = s;
                        bestAngle = angle;
                    }
                }
            }
        }

        if (Physics.Raycast(body.transform.position, body.transform.forward, out targets, 7000.0f) == true)
        {
            if ((targets.collider.tag == "Signature") || (targets.collider.tag == "SecondarySignature"))
            { trackedObject = targets.transform.gameObject; }
            else
            { trackedObject = null; }
        }

        localSignature.tag = "Signature";

        return trackedObject;
	}
}
