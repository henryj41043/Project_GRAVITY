using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour {
	
	// Update is called once per frame
    public static void FireBullet(Rigidbody body, Collider capsuleCollider, Collider boxCollider, GameObject trackedObject)
    {
        GameObject projectile = (GameObject)Instantiate(Resources.Load("Bullet"), body.transform.position + (body.transform.forward * 5), body.transform.rotation);
        Physics.IgnoreCollision(projectile.GetComponentInChildren<CapsuleCollider>(), capsuleCollider);
        Physics.IgnoreCollision(projectile.GetComponentInChildren<CapsuleCollider>(), boxCollider);
        if (trackedObject != null)
        {
            BulletController script = projectile.GetComponent<BulletController>();
            script.trackedObject = trackedObject;
            script.source = body.gameObject;
        }
        projectile.rigidbody.velocity = (projectile.rigidbody.transform.forward * (2000.0f));
	}
}
