/************************************************************
* COPYRIGHT: 2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: GenericEnemyAI.cs
* DESCRIPTION: Generic enemy AI used for the Move, Attack pattern.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/07 | Noah Zimmerman | Added auto aquiring target
* 2025/11/11 | Leyton McKinney | Awake() -> Start(), because we're finding GameObjects that aren't guarenteed to exist at Awake() time.
* 2025/11/11 | Leyton McKinney | Add attackSource for IEnemyAttackBehavior Attack() changes.
* 2025/11/11 | Leyton McKinney | Move Attack logic into GenericEnemyAI.
* 2025/11/12 | Leyton McKinney | Change Attack() calls to use targetTag field.
* 2025/11/12 | Leyton McKinney | Reduce projectile spawn distance.
* 2025/11/16 | Leyton McKinney | IEnemyMovementBehavior uses Rigidbody now.
************************************************************/
 
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class GenericEnemyAI : MonoBehaviour
{
    [Header("AI Behaviors")]
    [SerializeField] private ScriptableObject movementBehaviorSO;

    private Transform target;

    [Header("WeaponData")]
    [SerializeField] private ScriptableObject weaponSO;
    private Weapon weapon;

    private IEnemyMovementBehavior movementBehavior;
    private Rigidbody rb;


    private void Start()
    {
        movementBehavior = movementBehaviorSO as IEnemyMovementBehavior;
        if(weaponSO != null)
        {
            weapon = new Weapon(weaponSO as WeaponData);
        }
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null)
        {
            Debug.Log("Player does not exist in scene!");
        }

        if (!TryGetComponent(out rb)) {
            Debug.LogError("Enemy does not have Rigidbody component.");
        }
    }

    private void Update()
    {
        // If there is no target, there is nothing to do
        if (target == null) return;
        Vector3 targetDirection = (target.position - transform.position).normalized;

        // Temporary melee debug
        Debug.DrawRay(
            transform.position + 0.2f * targetDirection,
            targetDirection * weapon.getWeaponData().range,
            Color.cyan
        );
        movementBehavior?.Move(transform, rb, target);

        // Weapon Logic
        if(weapon != null)
        {
            // Vector3 targetDirection = (target.position - transform.position).normalized;
            if (weapon.getWeaponData().category == WeaponCategory.Melee)
            {
                weapon.Attack(
                    transform.position + 0.2f * targetDirection, // Origin of the enemy plus a small offset
                    targetDirection,
                    gameObject,
                    "Player"
                );
            }

            else
            {
                weapon.Attack(
                    transform.position + 1.0f * targetDirection,
                    targetDirection,
                    gameObject,
                    "Player"
                );
            }
        }
    }
}
