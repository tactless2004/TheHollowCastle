/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionGame
* FILE NAME: buttonOnPressPlay.cs
* DESCRIPTION: Functionality for the play/restart button for
* the main menu and the end game menu 
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/05 | Issai Gutierrez | Created class
* 2025/11/05 | Issai Gutierrez | Added basic button functionality 
************************************************************/

using System;
using UnityEngine;

public class ButtonFunctionality : MonoBehaviour
{
    [Tooltip("Reference to the singleton GameManager instance")]
    [SerializeField]
    private GameManager _gameManager;

    /// <summary>
    /// Loads the first level of the game upon clicking the button 
    /// </summary>
    public void PlayGame()
    {
        // Load scene using gameManager
        _gameManager.ChangeGameState(GameState.GamePlay);

    }//end CustomMethod(int)


    /// <summary>
    /// Loads the current level that the player is playing.
    /// </summary>
    public void ResumeGame() 
    {
        // Load scene using gameManager
        _gameManager.ChangeGameState(GameState.GamePlay);
    }

    /// <summary>
    /// Exits the game. 
    /// </summary>
    public void ExitGame() 
    {
        #if UNITY_EDITOR
            // Stop play mode in the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Load scene using gameManager
            _gameManager.ChangeGameState(GameState.GameOver);
        #endif
    }


}//end NewMonoBehaviourScript
