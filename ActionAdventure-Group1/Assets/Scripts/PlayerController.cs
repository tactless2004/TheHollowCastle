/************************************************************
* COPYRIGHT:  2025
* PROJECT: AdventureGameNameTBA
* FILE NAME: PlayerController.cs
* DESCRIPTION: A player controller that uses the new input system to contol the movement of a player.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/10/01 | Noah Zimmerman | Created Class
* 2025/11/04 | Leyton McKinney | Modified to use PlayerMovement instead of MoveTransform
* 2025/11/04 | Leyton McKinney | Add PlayerCombat bindings.
*
************************************************************/
 
using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerController : MonoBehaviour
{
    private PlayerMove _playerMove;
    private PlayerCombat _playerCombat;

    private void Start()
    {
        // Check if MoveTransform component DOES NOT EXIST
        if (!TryGetComponent<PlayerMove>(out _playerMove))
        {
            Debug.LogError("PlayerMove component missing!");
        }

        if (!TryGetComponent<PlayerCombat>(out _playerCombat))
        {
            Debug.LogError("PlayerCombat component missing!");
        }
    }
 

    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();

        // Vec2 -> Vec3
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y);
        _playerMove.Direction = direction;
        
    }

    public void OnMelee(InputValue value)
    {
        if(value.isPressed) _playerCombat.Melee();
    }

    public void OnRanged(InputValue value)
    {
        if (value.isPressed) _playerCombat.Ranged();
    }


}
