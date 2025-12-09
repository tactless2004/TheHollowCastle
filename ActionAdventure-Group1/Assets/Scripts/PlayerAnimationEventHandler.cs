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
*
*
************************************************************/
 
using UnityEngine;
 

public class PlayerAnimationEventHandler : MonoBehaviour
{
    private PlayerController playerController;
    public Animator PlayerAnimator;


    public void SetStateIdle()
    {
        playerController.animationState = PlayerController.AnimationState.Idle;
        playerController.animLock = false;
    }

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PlayerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        // if (stateInfo.IsName("Idle"))
        // {
        //     SetStateIdle();
        // }
    }
 
 
    /// <summary>
    /// A custom method example.
    /// </summary>
    /// <param name="exampleParameter"> A parameter that demonstrates passing data to the method.
    ///</param>
    private void CustomMethod(int exampleParameter)
    {
        
        
    }//end CustomMethod(int)
 
 
}//end PlayerAnimationEventHandler
