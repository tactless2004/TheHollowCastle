/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: CombatEntity.cs
* DESCRIPTION: Interface for enemies to take damage.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/07 | Leyton McKinney | Change IDamegable to take a WeaponData, instead of AttackData.
* 2025/11/08 | Leyton McKinney | IDamageable -> CombatEntity (Interface to Abstract).
*
************************************************************/
 
using UnityEngine;

// This has to be a monobehavior for unity to find this component, it otherwise does not need to be one :(
public abstract class CombatEntity : MonoBehaviour
{
    [SerializeField] protected float health = 20.0f;
    [SerializeField] protected float mana = 100.0f;

    public virtual void Heal(float healAmount) => health += healAmount;
    public virtual void TakeDamage(WeaponData attack) => health -= attack.damage;
    public virtual void GainMana(float manaAmount) => mana += manaAmount;
    public virtual void ExertMain(WeaponData attack) => mana -= attack.manaCost;
}
