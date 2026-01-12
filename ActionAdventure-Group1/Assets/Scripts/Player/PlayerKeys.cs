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
* 2025/12/8 | Chase Cone | Made keys public so UI and 
* 2025/12/08 | Noah Zimmerman | Switched to using collision and not trigger
* 2026/01/12 | Leyton McKinney | Switch voer to Event/Player Context Paradigm
************************************************************/

using System;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerKeys : MonoBehaviour
{
    
    [SerializeField] private int numKeys;

    public event Action<int> OnKeysChanged;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if it has the "Keys" tag
        if (collision.gameObject.CompareTag("Keys"))
        {
            numKeys++;
            OnKeysChanged?.Invoke(numKeys);   
            Destroy(collision.gameObject);
        }
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
