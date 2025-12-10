/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: ChestAnimationEventAdapter.cs
* DESCRIPTION: Event Handler for Chest Animations.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/12/08 | Leyton McKinney | Init
************************************************************/
 
using UnityEngine;
 

public class ChestAnimationEventAdapter : MonoBehaviour
{
    [SerializeField] Chest chest;

    // There's probably a better way to do this lmao
    public void SpawnWeapon() {
        chest.SpawnWeapon();
    }
}
