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
************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerController : MonoBehaviour
{
    private PlayerMove _playerMove;
    private PlayerCombat _playerCombat;
    private PlayerInventory _playerInventory;
    private GameManager _gameManager;
    
    private bool _paused = false;

    public bool animLock = false;

    public Animator playerAnimator;
    public enum AnimationState
    {
        Idle,
        Walk,
        Attack,
        Damage,
        Death
    }
    public AnimationState animationState;
    private void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Animator>();
    }

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

        if (!GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out _gameManager))
        {
            Debug.LogError("GameManager could not be found!");
        }
        else
        {
            _gameManager.onGameStateChanged += HandleGameState;
        }

        animationState = AnimationState.Idle;
    }

    public void Update()
    {
        Debug.Log(animationState);
        if (animLock)
        {
            _playerMove.Direction = Vector3.zero;
        }
        if (animationState == AnimationState.Idle)
        {
            animLock = false;
        }
    }


        public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        
        // Don't process standard inputs when paused.
        if (_paused) return;

        if (animationState == AnimationState.Attack || animationState == AnimationState.Damage) {
            return;
        }

        if (inputVector == Vector2.zero && animationState != AnimationState.Damage)
        {
            animationState = AnimationState.Idle;
            playerAnimator.Play("Idle");
        }
        else
        {
            animationState = AnimationState.Walk;
            playerAnimator.Play("Walk");
        }
        // Vec2 -> Vec3
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y);
        _playerMove.Direction = direction;
    }

    public void OnSlot1Attack(InputValue value)
    {
        // Don't process standard inputs when paused.
        if (_paused) return;

        if (animationState == AnimationState.Attack || animationState == AnimationState.Damage)
        {
            return;
        }
        animationState = AnimationState.Attack;
        playerAnimator.Play("SwordAttack");
        animLock = true;

        if (value.isPressed) _playerCombat.Slot1_Attack();
    }

    public void OnSlot2Attack(InputValue value)
    {
        // Don't process standard inputs when paused.
        if (_paused) return;

        if (animationState == AnimationState.Attack || animationState == AnimationState.Damage)
        {
            return;
        }
        animationState = AnimationState.Attack;
        playerAnimator.Play("SpearAttack");
        animLock = true;

        if (value.isPressed) _playerCombat.Slot2_Attack();
    }

    public void OnSwitchSlot1Weapon(InputValue value)
    {
        // Don't process standard inputs when paused.
        if (_paused) return;

        _playerInventory.pickupSlot(1);
    }

    public void OnSwitchSlot2Weapon(InputValue value)
    {
        // Don't process standard inputs when paused.
        if (_paused) return;

        _playerInventory.pickupSlot(2);
    }

    public void OnPause(InputValue value)
    {
        // If gameManager is null, try to find it.
        if (_gameManager == null)
            reacquireGameManager();
        else
        {
            _gameManager.PauseGame();
        }
    }

    // Helper/util methods, not directly related to processing inputs
    private void reacquireGameManager()
    {
        if (!TryGetComponent(out _gameManager))
        {
            Debug.LogError("PlayerController attempted to reacquire GameManager, but failed!");
        }
    }

    private void HandleGameState(GameState state)
    {
        _paused = (state == GameState.GamePaused);
    }
}
