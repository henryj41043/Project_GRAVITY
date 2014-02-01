using UnityEngine;
using System.Collections;

public class TargetLockScript : MonoBehaviour {

    private static RaycastHit targets;
    private static GameObject trackedObject;
	
	// Update is called once per frame
	public static GameObject GetTargets (Rigidbody body)
    {
        if (Physics.Raycast(body.transform.position, body.transform.forward, out targets, 2000.0f) == true)
        {
            if (targets.collider.tag == "TargetableObject")
            {
                trackedObject = targets.transform.gameObject;

                Transform model = trackedObject.transform.Find("Model");

                if (model != null)
                {
                    trackedObject = model.gameObject;
                }

                return trackedObject;
            }
        }
        return null;
	}
}
