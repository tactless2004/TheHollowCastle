/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: LockCursor.cs
* DESCRIPTION: Manages cursor lock state.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/16 | Leyton McKinney | Init
* 2025/11/17 | Leyton McKinney | Align cursor state with scene state.
*
************************************************************/

using Unity.VisualScripting;
using UnityEngine;


public class LockCursor : MonoBehaviour
{

    private GameManager gameManager;

    private void Awake()
    {
        if(!GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out gameManager)) {
            Debug.LogError($"{name} could not find GameManager");
        }

        // When a level is loaded, the cursor should initially be locked.
        // This might create issues down the line.
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        // Subscribe to GameState Publisher
        gameManager.onGameStateChanged += SetCursorLockState;
    }

    private void OnDisable()
    {
        // Unsubscribe to GameState Publisher
        gameManager.onGameStateChanged -= SetCursorLockState;
    }

    private void SetCursorLockState(GameState state)
    {
        // If in any State that is not game play, lock cursor.
        // this is generally menus.
        if (state != GameState.GamePlay)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
