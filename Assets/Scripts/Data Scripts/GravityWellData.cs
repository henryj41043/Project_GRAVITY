using UnityEngine;
using System.Collections;

public enum polarity { Pull = 1, Push = -1 }

public class GravityWellData : MonoBehaviour
{
    // Spheres of Influence
    public static float gameWorldRadius;
    public static float sentryRadius;
    public static float ionRadius;

    // Gravity Variables
    public static polarity gravityPolarity;
    public static float gravityConstant;
    public static float gravityWellMass;

    void Start()
    {
        // Spheres of Influence
        gameWorldRadius = 2000;
        sentryRadius = 0.0f;
        ionRadius = 0.0f;

        // Gravity Variables
        gravityPolarity = polarity.Pull;
        gravityConstant = 6.673f * Mathf.Pow(10.0f, -11.0f);
        gravityWellMass = 5.058f * Mathf.Pow(10.0f, 18.0f);
    }
}
