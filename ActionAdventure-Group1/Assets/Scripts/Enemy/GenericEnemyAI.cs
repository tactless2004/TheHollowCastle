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
************************************************************/
 
using UnityEngine;
 

public class GenericEnemyAI : MonoBehaviour
{
    [Header("AI Behaviors")]
    [SerializeField] private ScriptableObject movementBehaviorSO;
    [SerializeField] private ScriptableObject attackBehaviorSo;

    private IEnemyMovementBehavior movementBehavior;
    private IEnemyAttackBehavior attackBehavior;

    public Transform target;

    private void Awake()
    {
        movementBehavior = movementBehaviorSO as IEnemyMovementBehavior;
        attackBehavior = attackBehaviorSo as IEnemyAttackBehavior;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // If there is no target, there is nothing to do
        if (target == null) return;

        movementBehavior?.Move(transform, target);
        attackBehavior?.Attack(transform, target);
    }
}
