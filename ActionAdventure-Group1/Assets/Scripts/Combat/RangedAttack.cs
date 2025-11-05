/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: RangedAttack.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2000/01/01 | Your Name | Created class
*
*
************************************************************/
 
using UnityEngine;
 

public class RangedAttack : IAttack
{
    private GameObject projectilePrefab;
    private float speed;
    private float baseDamage;
    private DamageType damageType;

    public RangedAttack(GameObject projectilePrefab, float speed, float baseDamage = 10.0f, DamageType damageType = DamageType.Normal)
    {
        this.projectilePrefab = projectilePrefab;
        this.speed = speed;
        this.baseDamage = baseDamage;
        this.damageType = damageType;
    }

    public void Attack(Vector3 origin, Vector3 direction, GameObject source)
    {
        // Create Projectile
        GameObject projectile = GameObject.Instantiate(
            projectilePrefab,
            origin,
            Quaternion.LookRotation(direction)
        );

        if (projectile.TryGetComponent(out Projectile proj))
        {
            AttackData attack = new AttackData(baseDamage, damageType, source, direction);
            proj.Launch(direction, speed, attack);
        }
    }
}
