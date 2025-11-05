/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: PlayerCombat.cs
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


public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform rangedAttackOrigin;
    [SerializeField] private Transform meleeAttackOrigin;
    [SerializeField] private float meleeRange = 4.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10.0f;
    [SerializeField] private float rangedCooldown = 0.5f;
    [SerializeField] private float meleeCooldown = 0.5f;

    private PlayerMove playerMove;
    private IAttack meleeAttack;
    private IAttack rangedAttack;
    private float lastRangedAttackTime;
    private float lastMeleeAttackTime;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();

        meleeAttack = new MeleeAttack(meleeRange, 10.0f);
        rangedAttack = new RangedAttack(projectilePrefab, projectileSpeed);
    }

    public void Melee()
    {
        // Bail if the player is attacking too fast
        if (Time.time - lastMeleeAttackTime < meleeCooldown) return;

        Debug.Log("MELEE!");

        Vector3 dir = playerMove.facing;
        meleeAttack.Attack(meleeAttackOrigin.position, dir, gameObject);
        lastMeleeAttackTime = Time.time;
    }

    public void Ranged()
    {
        // If player is spamming ranged, bail
        if (Time.time - lastRangedAttackTime < rangedCooldown) return;

        Debug.Log("RANGED!");

        Vector3 dir = playerMove.facing;
        rangedAttack.Attack(rangedAttackOrigin.position, dir, gameObject);
        lastRangedAttackTime = Time.time;
    }
}
