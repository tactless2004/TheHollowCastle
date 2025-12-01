/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: PlayerKeys.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2000/01/01 | Your Name | Created class
* 2025/12/01 | Chase Cone | Created class
*
************************************************************/

using Unity.VisualScripting;
using UnityEngine;
 

public class PlayerKeys : MonoBehaviour
{
    [SerializeField] private int numKeys; 
 
 
    /// <summary>
    /// Detect when a player runs into something.
    /// </summary>
    /// <param name="other"> Wheat the player interacts with.
    ///</param>
    void OnTriggerEnter(Collider other)
        {
            // Check if it has the "Goal" tag
            if (other.CompareTag("Keys"))
            {
                numKeys++;
                Debug.Log("Got Key");
                //audio.PlayOneShot();
                Destroy(other.gameObject);
            }//end if("Goal")
    
        }//end OnTriggerEnter()
     
 
 
}//end PlayerKeys
