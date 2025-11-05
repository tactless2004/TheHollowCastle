/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: TestEnemyDamage.cs
* DESCRIPTION: Example implementation of IDamageable.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
 

public class NewMonoBehaviourScript : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100.0f;
    
    public void TakeDamage(AttackData attack)
    {
        // For this example ignore damage types
        health -= attack.damage;

        Debug.Log(gameObject.name + " took " + attack.damage + " " + attack.damageType + " damage from " + attack.source.name);
        if (health <= 0.0f) Die();
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }
}
