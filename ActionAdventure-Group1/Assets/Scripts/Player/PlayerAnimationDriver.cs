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
* 2026/01/12 | Leyton McKinney | Add explicit anim cancel behavior to prevent stun lock
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

    public bool CanCancelToMovement { get; private set; } = false;

    public event Action OnAttackCancelled;

    private void Awake()
    {
        GameObject tmpPlayerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        if (tmpPlayerModel != null)
            animator = tmpPlayerModel.GetComponent<Animator>();
    }

    public void TrySetMovement(bool isMoving)
    {
        if (IsMovementLocked || IsDead || IsInAttack) return;
        animator.Play(isMoving ? "Walk" : "Idle");
    }

    public bool TryPlayAttack(string animationName)
    {
        if (IsMovementLocked || IsDead || IsInAttack) return false;

        IsMovementLocked = true;
        IsInAttack = true;

        animator.Play(animationName);
        return true;
    }

    public bool TryPlayDamage()
    {
        if (IsDead) return false;

        if (IsInAttack)
            InterruptAttack();

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
        OnAttackCancelled?.Invoke();
    }

    public void EnableMovementCancel() => CanCancelToMovement = true;
    public void DisableCancel() => CanCancelToMovement = false;
    public void InterruptAttack()
    {
        if (!IsInAttack) return;

        IsInAttack = false;
        IsMovementLocked = false;
        CanCancelToMovement = false;

        OnAttackCancelled?.Invoke();
    }
}
