/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: PlayerHUD.cs
* DESCRIPTION: Mutates the PlayerHUD state.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/15 | Leyton McKinney | Init
* 2025/11/17 | Leyton McKinney | Account for PlayerHUD scene loading on top of level scene.
* 2025/11/17 | Leyton McKinney | Move pickup help text logic to this script.
* 2025/12/08 | Noah Zimmerman  | Fixes path for ui elements.
* 2026/01/11 | Leyton MCKinney | Subscribe to events for values to be changed.
************************************************************/

using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerHUD : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image slot1Image;
    [SerializeField] private Image slot2Image;
    [SerializeField] private TMP_Text pickupHelpText;
    [SerializeField] private TMP_Text keyCounter;

    private PlayerContext player;

    private void Awake()
    {
        if(!TryGetComponent(out player))
        {
            Debug.LogError("PlayerHUD component could not find PlayerContext component.");
        }

        pickupHelpText.text = "";
        keyCounter.text = "0";

        player.vitality.OnHealthChange += UpdateHealth;
        player.vitality.OnManaChange += UpdateMana;
        player.inventory.OnWeaponSlotChanged += UpdateWeaponSlot;
        player.keys.OnKeysChanged += UpdateKeys;
        player.pickup.OnPickupSeen += PickupSeen;
        player.pickup.OnPickupLost += PickupLost;
    }

    private void UpdateHealth(float current, float max)
    {
        healthBar.fillAmount = current / max;
    }
    private void UpdateMana(float current, float max)
    {
        manaBar.fillAmount = current / max;
    }

    private void UpdateWeaponSlot(Sprite sprite, int slot)
    {
        if (slot == 1)
            slot1Image.sprite = sprite;
        else
            slot2Image.sprite = sprite;
    }

    private  void UpdateKeys(int keys)
    {
        keyCounter.text = keys.ToString();
    }

    private void PickupSeen(PickupItem item)
    {
        pickupHelpText.text = $"Equip {item.GetWeapon().weaponName}? (Q/E)";
    }

    private void PickupLost()
    {
        pickupHelpText.text = "";
    }
}
