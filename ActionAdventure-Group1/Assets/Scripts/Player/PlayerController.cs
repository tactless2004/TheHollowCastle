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
* 2025/11/07 | Leyton McKinney | Change bindings from (Melee, Ranged), (Slot1Attack, Slot2Attack), Add weapon switch buttons (Q,E)
* 2025/11/08 | Leyton McKinney | Add inventory system controls.
* 2025/11/08 | Leyton McKinney | Implement inventory system controls.
*
************************************************************/
 
using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerController : MonoBehaviour
{
    private PlayerMove _playerMove;
    private PlayerCombat _playerCombat;
    private PlayerInventory _playerInventory;

    private void Awake()
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

        if(!TryGetComponent<PlayerInventory>(out _playerInventory))
        {
            Debug.LogError("PlayerInventory component missing!");
        }
    }
 

    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();

        // Vec2 -> Vec3
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y);
        _playerMove.Direction = direction;
        
    }

    public void OnSlot1Attack(InputValue value)
    {
        if(value.isPressed) _playerCombat.Slot1_Attack();
    }

    public void OnSlot2Attack(InputValue value)
    {
        if (value.isPressed) _playerCombat.Slot2_Attack();
    }

    public void OnSwitchSlot1Weapon(InputValue value)
    {
        _playerInventory.pickupSlot(1);
    }

    public void OnSwitchSlot2Weapon(InputValue value)
    {
        _playerInventory.pickupSlot(2);
    }

}
