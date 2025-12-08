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
 * 2025/12/01 | Chase Cone | Added door opening mechanics
* 2025/12/08 | Noah Zimmerman | Switched to using collision and not trigger
************************************************************/

using System;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerKeys : MonoBehaviour
{
    [SerializeField] private int numKeys;

    /// <summary>
    /// Detect when a player runs into something.
    /// </summary>
    /// <param name="collision"> Wheat the player interacts with.
    ///</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Check if it has the "Keys" tag
        if (collision.gameObject.CompareTag("Keys"))
        {
            numKeys++;
            Debug.Log("Got Key");
            //audio.PlayOneShot();
            Destroy(collision.gameObject);
        } //end if("Keys")
        else if (collision.gameObject.CompareTag("Doors"))
        {
            DoorMechanics doorData = collision.gameObject.GetComponent<DoorMechanics>();
            if (doorData != null && doorData.keysRequired <= numKeys)
            {
                doorData.doorOpen = true;
            }

        } //end if("Doors")

    }
}
