/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventure
* FILE NAME: LightFlicker.cs
* DESCRIPTION: Script takes in light source and uses lerp in order
* to produce the effect of flickering
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/08 | Issai Gutierrez | Created class
* 2025/11/08 | Issai Gutierrez | Created basic flickering effect
*
************************************************************/

using UnityEngine;
 

public class LightFlicker : MonoBehaviour
{
    // Reference to the light component
    private Light torchLight;

    [Header("Flicker Settings")]
    [Tooltip("The minimum light intensity")]
    [SerializeField]
    private float minIntensity = 0.8f;


    [Tooltip("The maximum light intensity")]
    [SerializeField]
    private float maxIntensity = 1.2f;

    [Tooltip("How fast the light flickers")]
    [SerializeField]
    private float flickerSpeed = 10.0f;

    // Random value to offset the noise calculation
    private float noiseOffset;

    void Start()
    {
        // Get the Light component attached to this same GameObject
        torchLight = GetComponent<Light>();

        noiseOffset = Random.Range(0f, 1000f);
    }//end start


    /// <summary>
    /// Every update will use the max intensity,min intensity, and noise in order to produce a flickering effect.
    /// </summary>
    void Update()
    {
        // Use Perlin Noise for a smooth, natural-looking "random" flicker
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, noiseOffset);

        // Lerp (linearly interpolate) between min and max intensity based on the noise
        torchLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }//end Update


}//end LightFlicker
