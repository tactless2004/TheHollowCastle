/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: PlayerAnimationEventHandler.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/12/08 | Peyton Lenard | Created class
* 2025/12/18 | Leyton McKinney | Improved Null Reference safety and added weapon spawning.
*
************************************************************/
 
using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerWeaponSpawner playerWeaponSpawner;
    public Animator PlayerAnimator;

    public void SetStateIdle()
    {
        playerController.animationState = PlayerController.AnimationState.Idle;
        playerController.animLock = false;
        playerWeaponSpawner.DeleteAllWeapons();
    }

    public void HideWeapon()
    {
        
    }

    private void Awake()
    {
        GameObject tmpPlayerReference = GameObject.FindGameObjectWithTag("Player"); 
        if (tmpPlayerReference != null)
        {
            playerController = tmpPlayerReference.GetComponent<PlayerController>();
            playerWeaponSpawner = tmpPlayerReference.GetComponent<PlayerWeaponSpawner>();
        }
        PlayerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(0);
    }
}
