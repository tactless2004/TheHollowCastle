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
* 2025/11/17 | Leyton McKinney | Move weapon indicator text logic to PlayerHUD.cs.
* 2025/01/11 | Leyton McKinney | Use PlayerContext paradigm.
* 2026/01/12 | Leyton McKinney | Use events to signal when the weapon has changed.
*
************************************************************/

using System;
using UnityEngine;
 

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private ScriptableObject weapon1SO;
    [SerializeField] private ScriptableObject weapon2SO;

    private WeaponData weapon1;
    private WeaponData weapon2;
    private PickupItem currentPickupSeen;

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

        player.pickup.OnPickupLost += OnPickupItemLose;
        player.pickup.OnPickupSeen += OnPickupItemSeen;
    }

    private void Start()
    {
        OnWeaponSlotChanged?.Invoke(weapon1.uiSprite, 1);
        OnWeaponSlotChanged?.Invoke(weapon2.uiSprite, 2);
    }

    public void pickupSlot(int slot)
    {
        // If there is no weapon to pickup then this input should return immediately
        if (currentPickupSeen == null) return;

        // Perform the swap
        WeaponData tempWeapon;
        if (slot == 1)
        {
            tempWeapon = weapon1;
            weapon1 = currentPickupSeen.GetWeapon();
            OnWeaponSlotChanged?.Invoke(weapon1.uiSprite, slot);
        }
        else
        {
            tempWeapon = weapon2;
            weapon2 = currentPickupSeen.GetWeapon();
            OnWeaponSlotChanged?.Invoke(weapon2.uiSprite, slot);
        }

        currentPickupSeen.SetWeapon(tempWeapon);
    }

    public void OnPickupItemSeen(PickupItem item)
    {
        currentPickupSeen = item;
    }

    public void OnPickupItemLose()
    {
        currentPickupSeen = null;
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
