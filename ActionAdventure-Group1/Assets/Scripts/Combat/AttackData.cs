/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: AttackData.cs
* DESCRIPTION: AttackData struct that defines an attack.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;

public struct AttackData
{
    public float damage;
    public DamageType damageType;
    public GameObject source;
    public Vector3 direction;
    public float knockbackForce;

    // Constructor
    public AttackData(float damage, DamageType type, GameObject source, Vector3 direction, float knockback = 0.0f)
    {
        this.damage = damage;
        this.damageType = type;
        this.source = source;
        this.direction = direction;
        this.knockbackForce = knockback;
    }
}

public enum DamageType
{
    Normal,
    Fire,
    Holy,
    Ice
}
