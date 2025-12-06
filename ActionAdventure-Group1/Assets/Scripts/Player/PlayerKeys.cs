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
*
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
    /// <param name="other"> Wheat the player interacts with.
    ///</param>
    void OnTriggerEnter(Collider other)
    {
        // Check if it has the "Keys" tag
        if (other.CompareTag("Keys"))
        {
            numKeys++;
            Debug.Log("Got Key");
            //audio.PlayOneShot();
            Destroy(other.gameObject);
        } //end if("Keys")
        else if (other.CompareTag("Doors"))
        {
            DoorMechanics doorData = other.gameObject.GetComponent<DoorMechanics>();
            if (doorData != null && doorData.keysRequired <= numKeys)
            {
                doorData.doorOpen = true;
            }

        } //end if("Doors")

    } //end OnTriggerEnter()
}
