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
    private PlayerContext player;

    public event Action<float, float> OnHealthChange;
    public event Action<float, float> OnManaChange;

    protected override void Awake()
    {
        base.Awake();
       if(!TryGetComponent(out player))
        {
            Debug.LogError("PlayerVitality could not find PlayerContext Component!");
        } 
    }
    public override void TakeDamage(WeaponData attack)
    {
        base.TakeDamage(attack);
        OnHealthChange?.Invoke(GetHealth(), MAXHEALTH);
        player.animation.TryPlayDamage();
    }

    public override void Heal(float healAmount)
    {
        base.Heal(healAmount);
        OnHealthChange?.Invoke(GetHealth(), MAXHEALTH);
    }

    public override void ExertMain(WeaponData attack)
    {
        base.ExertMain(attack);
        OnManaChange?.Invoke(GetMana(), MAXMANA);
    }

    public override void GainMana(float manaAmount)
    {
        base.GainMana(manaAmount);
        OnManaChange?.Invoke(GetMana(), MAXMANA);
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
