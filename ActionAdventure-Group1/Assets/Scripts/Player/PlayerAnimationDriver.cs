/************************************************************
* COPYRIGHT:  2026
* PROJECT: TheHollowCastle
* FILE NAME: PlayerAnimationDriver.cs
* DESCRIPTION: Animation Driver with minor internal logic.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/01/11 | Leyton McKinney | Init
*
************************************************************/

using System;
using UnityEngine;


public class PlayerAnimationDriver : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public bool IsMovementLocked { get; private set; }
    public bool IsInAttack { get; private set; }
    public bool IsDead { get; private set; }

    public event Action onAttackCancelled;

    private void Awake()
    {
        GameObject tmpPlayerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        if (tmpPlayerModel != null)
            animator = tmpPlayerModel.GetComponent<Animator>();
    }

    public void TrySetMovement(bool isMoving)
    {
        if (IsMovementLocked || IsDead) return;
        animator.Play(isMoving ? "Walk" : "Idle");
    }

    public bool TryPlayAttack(string animationName)
    {
        if (IsMovementLocked || IsDead) return false;

        IsMovementLocked = true;
        IsInAttack = true;

        animator.Play(animationName);
        return true;
    }

    public bool TryPlayDamage()
    {
        if (IsMovementLocked || IsDead) return false;

        if (IsInAttack)
        {
            IsInAttack = false;
            onAttackCancelled?.Invoke();
        }

        IsMovementLocked = true;

        animator.Play("PlayerDamage");
        return true;
    }

    public void PlayDeath()
    {
        IsDead = true;
        IsMovementLocked = true;
        animator.Play("PlayerDeath");
    }

    public void UnlockMovement()
    {
        IsMovementLocked = false;
        IsInAttack = false;
        onAttackCancelled?.Invoke();
    }
}
