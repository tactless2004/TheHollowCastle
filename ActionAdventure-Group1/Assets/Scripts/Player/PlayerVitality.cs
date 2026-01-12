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
* 2025/11/17 | Leyton McKinney | Override parent Die() method to go to GameOver scene.
* 2025/12/08 | Peyton Lenard   | Added animation player for TakeDamage and Die
* 2026/01/11 | Leyton McKinney | Use PlayerContext Paradigm.
************************************************************/

using System;
using UnityEngine;
 

public class PlayerVitality : CombatEntity
{
    [SerializeField] private PlayerHUD hud;
    private PlayerContext player;

    private void Start()
    {
       if(!TryGetComponent(out player))
        {
            Debug.LogError("PlayerVitality could not find PlayerContext Component!");
        } 
    }
    public override void TakeDamage(WeaponData attack)
    {
        base.TakeDamage(attack);
        hud.SetHealth(health, MAXHEALTH);
        player.animation.TryPlayDamage();
    }

    protected override void Die()
    {
        player.animation.PlayDeath();
        try
        {
            
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().ChangeGameState(GameState.GameOver);
        }

        catch (NullReferenceException)
        {
            Debug.LogError("Player died, thus invoking a GameState change to GameOver, however the GameManager was not found.");
        }
    }
}
