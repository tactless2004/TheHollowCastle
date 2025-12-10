/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: PickupItem.cs
* DESCRIPTION: Allows an item to be picked up by PlayerInventory.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/08 | Leyton McKinney | Init
* 2025/11/09 | Leyton McKinney | use prefab rotation instead of previous model rotation on SetWeapon() call.
*
************************************************************/
 
using UnityEngine;
using TMPro;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private ScriptableObject weaponSO;

    public WeaponData GetWeapon()
    {
        return weaponSO as WeaponData;
    }

    public void SetWeapon(WeaponData weapon)
    {
        Transform trans = transform;
        // So, if we instantiate the new pickup weapon at the same position as the old one that seems to work
        // as far as current testing goes, but the rotation of the prefab should probably be maintained, strictly as a convenience for now, although
        // it's worth considering what might be a better long term solution - LGM
        Instantiate(weapon.pickupModelPrefab, trans.position, weapon.pickupModelPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
