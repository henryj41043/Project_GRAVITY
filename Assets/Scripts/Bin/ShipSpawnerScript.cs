using UnityEngine;
using System.Collections;

public class ShipSpawnerScript : MonoBehaviour
{
    // Internal Counter
    public static int shipCurCount;
    public static int shipMaxCount;

    // Use this for initialization
    void Start()
    {
        shipCurCount = 0;
        shipMaxCount = 20;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (shipCurCount < shipMaxCount)
        {
            shipCurCount = shipCurCount + 1;
            Instantiate(Resources.Load("Enemy"), Random.rotation * Vector3.one * WellDataScript.gameWorldRadius, Random.rotation);
        }
	}
}
