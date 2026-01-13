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
* 2025/12/03 | Noah Zimmerman  | Added method to track amount of enemies through game manager.
* 2026/01/13 | Leyton McKinney | Add OnDamaged and OnDead events, remove unused GameManager.
************************************************************/

using System;
using UnityEngine;

public abstract class CombatEntity : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float health;
    [SerializeField] protected float healthRegen = 1.0f; // health per second;
    [SerializeField] protected float MAXHEALTH = 20.0f;

    [Header("Mana")]
    [SerializeField] protected float mana;
    [SerializeField] protected float manaRegen = 1.0f; // mana per second;
    [SerializeField] protected float MAXMANA = 100.0f;

    private float lastManaRegen;
    private float lastHealthRegen;

    public event Action<WeaponData> OnDamaged;
    public event Action OnDead;

    public virtual void Heal(float healAmount) => health += healAmount;
    public virtual void TakeDamage(WeaponData attack)
    {
        health -= attack.damage;
        OnDamaged?.Invoke(attack);
    }
    public float GetHealth() => health;
    public virtual void GainMana(float manaAmount) => mana += manaAmount;
    public virtual void ExertMain(WeaponData attack) => mana -= attack.manaCost;
    public float GetMana() => mana;

    protected virtual void Die()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }

    protected virtual void Awake()
    {
        health = MAXHEALTH;
        mana = MAXMANA;
    }
    private void Update()
    {
        if (Time.time - lastManaRegen > 1.0f / manaRegen)
        {
            mana = Mathf.Clamp(mana + 1, 0.0f, MAXMANA);
            lastManaRegen = Time.time;
        }

        if (Time.time - lastHealthRegen > 1.0f / healthRegen)
        {
            health = Mathf.Clamp(health + 1, 0.0f, MAXHEALTH);
            lastHealthRegen = Time.time;
        }

        if (health <= 0.0f) Die();
    }
}
