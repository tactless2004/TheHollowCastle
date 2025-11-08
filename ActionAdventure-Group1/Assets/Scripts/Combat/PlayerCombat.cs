/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: PlayerCombat.cs
* DESCRIPTION: Describes the Combat Behaviors of the player.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/07 | Leyton McKinney | Switch from (Range, Melee) scheme to (Slot1, Slot2 Scheme), change cooldown to be weapon specific.
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

    public void Slot1_Attack()
    {
        Debug.Log("Slot 1 Attack");
        // Bail if the player is attacking too fast
        if (Time.time - lastMeleeAttackTime < meleeCooldown) return;

        Vector3 dir = playerMove.facing;
        meleeAttack.Attack(meleeAttackOrigin.position, dir, gameObject);
        lastMeleeAttackTime = Time.time;
    }

    public void Slot2_Attack()
    {
        Debug.Log("Slot 2 Attack");
        // If player is spamming ranged, bail
        if (Time.time - lastRangedAttackTime < rangedCooldown) return;

        Vector3 dir = playerMove.facing;
        rangedAttack.Attack(rangedAttackOrigin.position, dir, gameObject);
        lastRangedAttackTime = Time.time;
    }
}
