using UnityEngine;
using System.Collections;

public class WellLightFXScript : MonoBehaviour
{
    Light mapLight;
    ParticleSystem mapAura;

	// Use this for initialization
	void Start ()
    {
        mapLight = GameObject.Find("Light - Map Light").GetComponent<Light>();
        mapAura = GameObject.Find("FX - Map Aura").GetComponent<ParticleSystem>();
	}

    public void resizeRange(float range)
    {
        mapLight.range = range;
        mapAura.transform.localScale = Vector3.one * (range);
        mapAura.startSize = (range * 50);
    }
}
