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
*
************************************************************/

using TMPro;
using UnityEngine;
 

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private ScriptableObject weapon1SO;
    [SerializeField] private ScriptableObject weapon2SO;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float maxPickupDistance = 0.75f;
    [SerializeField] private TMP_Text pickupHelpText;

    private WeaponData weapon1;
    private WeaponData weapon2;
    private PlayerCombat playerCombat;
    private PlayerMove playerMove;
    private PlayerHUD hud;

    private void Awake()
    {
        weapon1 = weapon1SO as WeaponData;
        weapon2 = weapon2SO as WeaponData;
        pickupHelpText.text = "";

        if(!TryGetComponent(out playerCombat))
        {
            Debug.LogError("Player does not have PlayerCombat component.");
        }

        if(!TryGetComponent(out playerMove))
        {
            Debug.LogError("Player does not have PlayerMove component.");
        }

        if(!TryGetComponent(out hud))
        {
            Debug.LogError("Player does not have PlayerHUD component.");
        }
    }

    private void Start()
    {
        hud.SetWeaponSprite(weapon1.uiSprite, 1);
        hud.SetWeaponSprite(weapon2.uiSprite, 2);
    }

    public void pickupSlot(int slot)
    {
        if(Physics.Raycast(raycastOrigin.position, playerMove.facing, out RaycastHit hit, maxPickupDistance))
        {
            if(hit.collider.TryGetComponent(out PickupItem pickupItem))
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

                playerCombat.SetWeapon(slot, weapon);
                hud.SetWeaponSprite(weapon.uiSprite, slot);
                Debug.Log($"Player picked up {weapon.name} in slot {slot}.");
            }
        }
    }

    private void Update()
    {
        // Note:
        // This should probably be handled in an other script, but putting here for now for debug purposes - LGM

        // Debug Ray to show pickup distance
        // Debug.DrawRay(raycastOrigin.position, playerMove.facing * maxPickupDistance, Color.green);

        // Pickup Indicator text logic
        if(Physics.Raycast(raycastOrigin.position, playerMove.facing, out RaycastHit hit, maxPickupDistance))
        {
            if(hit.collider.TryGetComponent(out PickupItem pickupItem) && pickupHelpText != null)
            {

                pickupHelpText.text = $"Equip {pickupItem.GetWeapon().name}?";
            }
        }
        else if (pickupHelpText != null)
        {
            pickupHelpText.text = "";
        }
    }
}
