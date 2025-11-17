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
*
************************************************************/
 
using UnityEngine;
using UnityEngine.UI;


public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image slot1Image;
    [SerializeField] private Image slot2Image;

    public void SetHealth(float current, float max)
    {
        healthBar.fillAmount = current / max;
    }

    public void SetMana(float current, float max)
    {
        manaBar.fillAmount = current / max;
    }

    public void SetWeaponSprite(Sprite sprite, int slot)
    {
        if (slot == 1)
            slot1Image.sprite = sprite;
        else
            slot2Image.sprite = sprite;
    }
}
