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
************************************************************/
 
using UnityEngine;
 

public class GenericEnemyAI : MonoBehaviour
{
    [Header("AI Behaviors")]
    [SerializeField] private ScriptableObject movementBehaviorSO;

    private Transform target;

    [Header("WeaponData")]
    [SerializeField] private ScriptableObject weaponSO;
    private Weapon weapon;

    private IEnemyMovementBehavior movementBehavior;
    

    

    private void Start()
    {
        movementBehavior = movementBehaviorSO as IEnemyMovementBehavior;
        if(weaponSO != null)
        {
            weapon = new Weapon(weaponSO as WeaponData);
        }
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // If there is no target, there is nothing to do
        if (target == null) return;

        movementBehavior?.Move(transform, target);

        // Weapon Logic
        if(weapon != null)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            if (weapon.getWeaponData().category == WeaponCategory.Melee)
            {
                weapon.Attack(
                    transform.position + 0.2f * targetDirection, // Origin of the enemy plus a small offset
                    targetDirection,
                    gameObject
                );
            }

            else
            {
                weapon.Attack(
                    transform.position + 1.75f * targetDirection,
                    targetDirection,
                    gameObject
                );
            }
        }
    }
}
