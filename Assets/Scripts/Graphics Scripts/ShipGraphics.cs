using UnityEngine;
using System.Collections;

public class ShipGraphics : MonoBehaviour
{
    // Component Variables
    //Light LeftLight;
    //Light RightLight;
    //Light MainLight;
    ParticleSystem LeftBooster;
    ParticleSystem RightBooster;
    ParticleSystem MainThruster;

	// Use this for initialization
	void Start ()
    {
        //LeftLight = GameObject.Find("Left Booster").GetComponent<Light>();
        //RightLight = GameObject.Find("Right Booster").GetComponent<Light>();
        //MainLight = GameObject.Find("Main Thruster").GetComponent<Light>();
        LeftBooster = GameObject.Find("Left Booster").GetComponent<ParticleSystem>();
        RightBooster = GameObject.Find("Right Booster").GetComponent<ParticleSystem>();
        MainThruster = GameObject.Find("Main Thruster").GetComponent<ParticleSystem>();
	}
	
	// Draw is used for the Special FX of the Ship
    public void Draw(Quaternion preRotation, Quaternion postRotation, float propulsion)
    {
        float hOrientation = Vector3.Dot(preRotation * Vector3.right, postRotation * Vector3.forward);
        float vOrientation = Vector3.Dot(preRotation * Vector3.up, postRotation * Vector3.forward);

        //LeftLight.intensity = Mathf.Clamp(Mathf.Abs(vOrientation) * 4, 0.0f, 1.0f);
        LeftBooster.startLifetime = Mathf.Clamp(Mathf.Abs(vOrientation) * 4, 0.0f, 1.0f);
        //RightLight.intensity = Mathf.Clamp(Mathf.Abs(vOrientation) * 4, 0.0f, 1.0f);
        RightBooster.startLifetime = Mathf.Clamp(Mathf.Abs(vOrientation) * 4, 0.0f, 1.0f);

        //LeftLight.intensity = Mathf.Max(LeftLight.intensity, Mathf.Clamp(hOrientation * 4, 0.0f, 1.0f));
        LeftBooster.startLifetime = Mathf.Max(LeftBooster.startLifetime, Mathf.Clamp(hOrientation * 4, 0.0f, 1.0f));
        //RightLight.intensity = Mathf.Max(RightLight.intensity, Mathf.Clamp(-hOrientation * 4, 0.0f, 1.0f));
        RightBooster.startLifetime = Mathf.Max(RightBooster.startLifetime, Mathf.Clamp(-hOrientation * 4, 0.0f, 1.0f));

        //LeftLight.intensity = 8.0f * Mathf.Clamp(LeftLight.intensity, 0.0f, 1.0f);
        LeftBooster.startLifetime = 1.0f * Mathf.Clamp(LeftBooster.startLifetime, 0.0f, 1.0f);
        //RightLight.intensity = 8.0f * Mathf.Clamp(RightLight.intensity, 0.0f, 1.0f);
        RightBooster.startLifetime = 1.0f * Mathf.Clamp(RightBooster.startLifetime, 0.0f, 1.0f);
    
        //MainLight.intensity = 8.0f * propulsion;
        MainThruster.startLifetime = 1.0f * propulsion;
	}
}
