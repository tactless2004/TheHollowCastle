/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: WeaponData.cs
* DESCRIPTION: WeaponData ScriptableObject that defines a weapon.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/07 | Leyton McKinney | Pivot from AttackData -> WeaponData for new weapon pickup based system.
* 2025/11/13 | Leyton McKinney | Forgot to add damage source.
*
************************************************************/
 
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("General")]
    public string weaponName;
    public WeaponCategory category;
    public DamageType damageType;
    public DamageSource damageSource;

    [Header("Stats")]
    public float damage = 10.0f;
    public float range = 2.0f;
    public float attackCooldown = 1.0f;
    public float manaCost = 0.0f;

    [Header("Visuals")]
    public GameObject pickupModelPrefab;
    public GameObject combatPrefab;
    public Sprite uiSprite;
    public string animationName;

    [Header("Audio (Optional)")]
    public AudioClip attackSound;
    public AudioClip pickupsound;

    [Header("Ranged Specfic")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 2.0f;

    [Header("Special Effects")]
    public WeaponEffect[] effects;
}

public enum WeaponCategory
{
    Melee,
    Ranged
}

public enum DamageSource
{
    Physical,
    Magic
}

public enum DamageType
{
    Slash,
    Strike,
    Pierce,
    Fire,
    Ice,
    Electric,
    Divine,
    Death
}
