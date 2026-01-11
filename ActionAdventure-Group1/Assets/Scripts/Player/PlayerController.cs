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
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    private PlayerMove _playerMove;
    private PlayerInventory _playerInventory;
    private PlayerWeaponSpawner _playerWeaponSpawner;
    private GameManager _gameManager;
    private Animator playerAnimator;

    private bool _paused = false;

    public bool animLock = false;

    
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
        // Player animator is grabbed at start time, just in case the model isn't loaded at awake time.
        GameObject tmpPlayerModel = GameObject.FindGameObjectWithTag("PlayerModel"); 
        if (tmpPlayerModel != null)
            playerAnimator = tmpPlayerModel.GetComponent<Animator>();
        else 
            Debug.LogError("Player Controller could not find PlayerModel.");
    }

    private void Awake()
    {
        // Check if MoveTransform component DNE
        if (!TryGetComponent(out _playerMove))
        {
            Debug.LogError("PlayerMove component missing!");
        }

        if(!TryGetComponent(out _playerInventory))
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

        if (!TryGetComponent(out _playerWeaponSpawner))
        {
            Debug.LogError("PlayerWeaponSpawner component missing!");
        }

        animationState = AnimationState.Idle;
    }

    public void Update()
    {
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
        Attack(1);
    }

    public void OnSlot2Attack(InputValue value)
    {
        Attack(2);
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
        _paused = state == GameState.GamePaused;
    }

    private void Attack(int slot)
    {
        // Don't process standard inputs when paused.
        if (_paused) return;

        WeaponData weaponData = _playerInventory.getWeaponData(slot);
        // If weaponData is null (for whatever reason) just bail lest we throw a NRE
        if (weaponData == null) return;

        if (animationState == AnimationState.Attack || animationState == AnimationState.Damage)
        {
            return;
        }
        animationState = AnimationState.Attack;

        if (weaponData.name == "Sword")
        {
            playerAnimator.Play("SwordAttack");
            _playerWeaponSpawner.SpawnWeapon(weaponData.combatPrefab);
        }
        if (weaponData.name == "Hammer")
        {
            playerAnimator.Play("SwordAttack");
            _playerWeaponSpawner.SpawnWeapon(weaponData.combatPrefab);
        }
        if (weaponData.name == "Spear")
        {
            playerAnimator.Play("SpearAttack");
            _playerWeaponSpawner.SpawnWeapon(weaponData.combatPrefab);
        }
        if (weaponData.name == "Greatsword")
        {
            playerAnimator.Play("GreatswordAttack");
            _playerWeaponSpawner.SpawnWeapon(weaponData.combatPrefab);
        }
        if (weaponData.name == "Throwing Knife")
        {
            playerAnimator.Play("ThrowingKnifeAttack");
            _playerWeaponSpawner.SpawnWeapon(weaponData.combatPrefab);
        }
        animLock = true;        
    }

}
