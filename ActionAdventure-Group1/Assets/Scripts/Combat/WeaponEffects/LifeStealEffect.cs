/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: LifeStealEffect.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/07 | Leyton McKinney | Add a boilerplate implementation to show the workflow
*
************************************************************/
 
using UnityEngine;
 

public class LifeStealEffect : WeaponEffect
{
    [Range(0f, 1f)] public float lifeStealPercent;

    public override void ApplyEffect(GameObject attacker, GameObject target, WeaponData attack)
    {
        throw new System.NotImplementedException();
    }
}
