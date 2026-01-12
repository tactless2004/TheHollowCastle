/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
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
* 2025/11/17 | Leyton McKinney | Add pause checking.
* 2025/12/08 | Peyton Lenard   | Added animation player and animLock for animation state control
* 2026/01/11 | Leyton McKinney | Use PlayerContext, and modify how animations are played.
************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerContext player;
    private GameManager _gameManager;

    private bool _paused = false;

    private void Awake()
    {
        if (!TryGetComponent(out player))
        {
            Debug.LogError("PlayerController could not find PlayerContext");
        }

        if (!GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out _gameManager))
        {
            Debug.LogError("GameManager could not be found!");
        }
        else
        {
            _gameManager.onGameStateChanged += HandleGameState;
        }
    }

    public void OnMove(InputValue value)
    {   
        // Don't process standard inputs when paused.
        if (_paused) return;

        Vector2 inputVector = value.Get<Vector2>();
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y);

        if (
            player.animation.IsInAttack && // Player is currently in attack animation
            direction != Vector3.zero && // Player is issue movement input
            player.animation.CanCancelToMovement // Player can cancel attack
        )
        {
            player.animation.InterruptAttack();
        }

        // Enact movement
        player.move.Direction = direction;

        // Play walk or idle if not attacking
        player.animation.TrySetMovement(direction != Vector3.zero);
    }

    public void OnSlot1Attack(InputValue value)
    {
        if (_paused || !value.isPressed) return;

        WeaponData weaponData = player.inventory.getWeaponData(1);
        if (weaponData == null) return;

        if (player.animation.TryPlayAttack(weaponData.animationName))
            player.weaponSpawner.Attack(weaponData);
    }

    public void OnSlot2Attack(InputValue value)
    {
        if (_paused || !value.isPressed) return;

        WeaponData weaponData = player.inventory.getWeaponData(2);
        if (weaponData == null) return;

        if (player.animation.TryPlayAttack(weaponData.animationName))
            player.weaponSpawner.Attack(weaponData);
    }

    public void OnSwitchSlot1Weapon(InputValue value)
    {
        // Don't process standard inputs when paused.
        if (_paused) return;

        player.inventory.pickupSlot(1);
    }

    public void OnSwitchSlot2Weapon(InputValue value)
    {
        // Don't process standard inputs when paused.
        if (_paused) return;

        player.inventory.pickupSlot(2);
    }

    public void OnPause(InputValue value)
    {
        // If gameManager is null, try to find it.
        if (_gameManager == null)
            reacquireGameManager();
        else
            _gameManager.PauseGame();
    }

    private void reacquireGameManager()
    {
        // Try to find the GameManager
        GameObject tmpGameManager = GameObject.FindGameObjectWithTag("GameManager");

        // If GameManager is not null, try to acquire it, if this fails print Error.
        if (tmpGameManager != null && !tmpGameManager.TryGetComponent(out _gameManager))
        {
            Debug.LogError("PlayerController attempted to Reacquire GameManager, but could not find it.");
        }
    }

    private void HandleGameState(GameState state)
    {
        _paused = state == GameState.GamePaused;
    }
}
