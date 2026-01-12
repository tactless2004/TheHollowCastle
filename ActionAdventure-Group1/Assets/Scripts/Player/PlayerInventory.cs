/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: PlayerInventory.cs
* DESCRIPTION: Manages the equipped weapons of the player.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/10 | Leyton McKinney | Add null checking for indicator UI text.
* 2025/11/15 | Leyton McKinney | Add PlayerHUD mutation.
* 2025/11/17 | Leyton McKinney | Move weapon indicator text logic to PlayerHUD.cs
*
************************************************************/

using System;
using TMPro;
using UnityEngine;
 

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private ScriptableObject weapon1SO;
    [SerializeField] private ScriptableObject weapon2SO;

    // TODO: Get rid of raycast system use a sphere cast or entirely get rid of it.
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float maxPickupDistance = 3.0f;

    private WeaponData weapon1;
    private WeaponData weapon2;
    private PlayerContext player;

    public event Action<Sprite, int> OnWeaponSlotChanged;
    private void Awake()
    {
        weapon1 = weapon1SO as WeaponData;
        weapon2 = weapon2SO as WeaponData;

        if(!TryGetComponent(out player))
        {
            Debug.LogError("PlayerInventory Component was unable to find Player Context component.");
        }
    }

    private void Start()
    {
        OnWeaponSlotChanged?.Invoke(weapon1.uiSprite, 1);
        OnWeaponSlotChanged?.Invoke(weapon2.uiSprite, 2);
    }

    public void pickupSlot(int slot)
    {
        if (Physics.Raycast(raycastOrigin.position, player.move.facing, out RaycastHit hit, maxPickupDistance))
        {
            // If is pickupWeapon
            if (hit.collider.TryGetComponent(out PickupItem pickupItem))
            {
                WeaponData weapon = pickupItem.GetWeapon();

                // swap PickupItem weapon and InventoryWeapon
                if (slot == 1)
                {
                    pickupItem.SetWeapon(weapon1);
                    weapon1 = weapon;
                }
                else
                {
                    pickupItem.SetWeapon(weapon2);
                    weapon2 = weapon;
                }

                OnWeaponSlotChanged?.Invoke(weapon.uiSprite, slot);
            }
        }
    }

    public WeaponData getWeaponData(int slot)
    {
        if      (slot == 1) return weapon1;
        else if (slot == 2) return weapon2;
        
        Debug.LogError($"PlayerInventory had an invalid get request for slot {slot}");
        // If they ask for a weapon we aint have just return null fr
        return null;
    }
}
