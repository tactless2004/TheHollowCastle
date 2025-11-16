/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: PlayerVitality.cs
* DESCRIPTION: CombatVitality implementation for player.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/10 | Leyton McKinney | Init
* 2025/11/15 | Leyton McKinney | Add PlayerHUD mutation.
************************************************************/
 
using UnityEngine;
 

public class PlayerVitality : CombatEntity
{
    [SerializeField] private PlayerHUD hud;

    public override void TakeDamage(WeaponData attack)
    {
        base.TakeDamage(attack);
        hud.SetHealth(health, MAXHEALTH);
    }
}
