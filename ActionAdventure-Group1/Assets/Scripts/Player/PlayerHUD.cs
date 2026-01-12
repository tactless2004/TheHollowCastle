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
* 2025/12/08 | Noah Zimmerman  | Fixes path for ui elements
************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Pickup")]
    [SerializeField] private float maxPickupDistance = 3.0f;

    private PlayerContext player;

    private void Awake()
    {
        if(!TryGetComponent(out player))
        {
            Debug.LogError("PlayerHUD component could not find PlayerContext component.");
        }

        pickupHelpText.text = "";

        player.vitality.OnHealthChange += UpdateHealth;
        player.vitality.OnManaChange += UpdateMana;
        player.inventory.OnWeaponSlotChanged += UpdateWeaponSlot;
        player.keys.OnKeysChanged += UpdateKeys;
    }

    public void UpdateHealth(float current, float max)
    {
        healthBar.fillAmount = current / max;
    }
    public void UpdateMana(float current, float max)
    {
        manaBar.fillAmount = current / max;
    }

    public void UpdateWeaponSlot(Sprite sprite, int slot)
    {
        if (slot == 1)
            slot1Image.sprite = sprite;
        else
            slot2Image.sprite = sprite;
    }

    public void UpdateKeys(int keys)
    {
        keyCounter.text = keys.ToString();
    }

    private void Update()
    {
        // Pickup Indicator text logic
        RaycastHit[] hits = Physics.RaycastAll(transform.position, player.move.facing, maxPickupDistance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out PickupItem pickupItem) && pickupHelpText != null)
            {
                pickupHelpText.text = $"Equip {pickupItem.GetWeapon().name}?";
                break;
            }
            else if (pickupHelpText != null)
            {
                pickupHelpText.text = "";
            }
        }
    }

    private void Start()
    {
        // The Player HUD for testing lives under the player, however if this is the build
        // runtime we need to destroy it and use the one provided by the scene.
        Scene hudScene = SceneManager.GetSceneByName("UI_PlayerHUDScene");

        bool isInHudScene = gameObject.scene.name == "UI_PlayerHUDScene";
        bool hudSceneLoaded = hudScene.isLoaded;

        if (!isInHudScene && hudSceneLoaded)
            Destroy(playerHUD);

        // If the Player Hud Scene is loaded, we need to reselect the UI elements.
        if (hudSceneLoaded)
        {
            foreach (GameObject go in hudScene.GetRootGameObjects())
            {
                if (go.name == "UI")
                {
                    manaBar        = go.transform.Find("PlayerHUD/ManaBarEmpty/ManaBarFull").GetComponent<Image>();
                    healthBar      = go.transform.Find("PlayerHUD/HealthBarEmpty/HealthBarFull").GetComponent<Image>();
                    slot1Image     = go.transform.Find("PlayerHUD/Slot1Weapon/Slot1WeaponImage").GetComponent<Image>();
                    slot2Image     = go.transform.Find("PlayerHUD/Slot2Weapon/Slot2WeaponImage").GetComponent<Image>();
                    pickupHelpText = go.transform.Find("PlayerHUD/WeaponPickupHelpText").GetComponent<TextMeshProUGUI>();
                    keyCounter = go.transform.Find("PlayerHUD/KeyCounter/NumberOfKeys").GetComponent<TextMeshProUGUI>();
                    
                }
            }
        }
    }
}
