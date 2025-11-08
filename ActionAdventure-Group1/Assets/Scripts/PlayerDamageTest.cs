/************************************************************
* COPYRIGHT: 2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: PlayerDamageTest.cs
* DESCRIPTION: Trivial IDamageable implementation to test enemy AI (probably replace with something more comprehensive in the future).
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/07 | Leyton McKinney | AttackData -> WeaponData
*
************************************************************/
 
using UnityEngine;


public class PlayerDamageTest : MonoBehaviour, IDamageable
{
    public float health = 100.0f;

    public void TakeDamage(WeaponData weapon)
    {
        health -= weapon.damage;

        Debug.Log($"{name} took {weapon.damage} {weapon.damageType} damage.");

        if (health <= 0.0f) Die();
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
