/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventure
* FILE NAME: AppearAnimation.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/07 | Issai Gutierrez | Created class
* 2025/11/07 | Issai Gutierrez | Added basic animation functionality
*
************************************************************/


using UnityEngine;
using System.Collections; 
using UnityEngine.UI; 


public class AppearAnimation : MonoBehaviour
{

    // Public variables to control the animation from the Unity Inspector
    public Vector3 startScale = Vector3.zero;
    public Vector3 endScale = Vector3.one;

    // How long the animation takes (in seconds)
    public float animationDuration = 0.5f; 


    void Awake()
    {
        transform.localScale = startScale;
    }//End Awake()

    void Start()
    {
        // Start the animation when scene opens
        StartCoroutine(PopUpAnimation());

    }//End Start()

    // Coroutine to handle the transition
    private IEnumerator PopUpAnimation()
    {
        float timeElapsed = 0f;

        // Loop while the animation is still running
        while (timeElapsed < animationDuration)
        {

            float t = timeElapsed / animationDuration;

            // Use Lerp to move the button's position
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            timeElapsed += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }//End while(timeElapsed < animationDuration)

        transform.localScale = endScale;
    }//end PopUpAnimation()


}//end AppearAnimation


