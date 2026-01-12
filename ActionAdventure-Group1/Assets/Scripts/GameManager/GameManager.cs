/************************************************************
 * COPYRIGHT:  2025
 * PROJECT: The Hollow Castle
 * FILE NAME: GameManager.cs
 * DESCRIPTION: Game State and Scene Manager.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2000/01/01 | Your Name | Created class
 * 2025/11/4 | Chase Cone |  Created Class
 * 2025/11/12 |  Chase Cone | Updated Functionality and added to bootstrap
 * 2025/11/16 | Chase Cone | Added scene manager functionality
 * 2025/11/17 | Chase Cone | Made scene manger load the first level
 * 2025/11/17 | Leyton McKinney | Modified pause behavior to not reload the level on un-pause.
 * 2025/11/17 | Leyton McKinney | Add Pub/Sub (Observer) pattern, so other components can be informed about state changes.
 * 2025/11/19 | Leyton McKinney | Allow the GameManager to be used when not starting in Bootstrap scene.
 * 2025/12/03 | Noah Zimmerman  | Added tracking amount of enemies currently spawned
 ************************************************************/

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


// The GameManager class is derived from the Singleton pattern to ensure there is only one instance of it in the game.
public class GameManager: Singleton<GameManager>
{
    [Header("Scene Management")]
    [SerializeField]
    [Tooltip("The main menu scene that loads when the game starts.")]
    private string _mainMenuScene;
   
    [SerializeField]
    [Tooltip("The HUD overlay that appears during gameplay.")]
    private string _hudScene;
   
    [SerializeField]
    [Tooltip("The pause menu overlay that appears when the game is paused.")]
    private string _pauseMenuScene;
   
    [SerializeField]
    [Tooltip("The Game Over scene that loads when the player loses or finishes the game.")]
    private string _gameOverScene;

    [SerializeField, Tooltip("Bootstrap scene containing the GameManager")]
    private string _bootstrapScene;

    [SerializeField]
    [Tooltip("All the level scenes in the game, in the order they should be played.")]
    private List<string> _gameLevels = new List<string>();
    
    // Index of the currently active level in the levelScenes list
    private int _currentLevelIndex = 0;
   
    // Tracks the currently loaded primary scene (menu or level)
    private string _currentScene;
   
    //List of all loaded scenes
    private List<string> _loadedScenes = new List<string>();

    // Event to inform subscribers when the GameState changes.
    public event System.Action<GameState> onGameStateChanged;
    

    void Start()
    {
        // Set the initial game state to Main Menu if starting in Bootstrap scene
        if (SceneManager.GetActiveScene().name == "Bootstrap")
            ChangeGameState(GameState.MainMenu);
        // If the game doesn't start in the boot strap scene, then presumably the game is startign in some level.
        else
        {
            Scene originalScene = SceneManager.GetActiveScene();
            _loadedScenes.Add(originalScene.name);

            // Load the bootstrap scene making it the primary scene
            SceneManager.LoadSceneAsync(_bootstrapScene);

            // Load the original scene on top of BootStrap
            SceneManager.LoadSceneAsync(originalScene.name, LoadSceneMode.Additive);

            // Bypass ChangeGameState() logic
            CurrentState = GameState.GamePlay;
        }

    }//end Start()
    
    // Reference to the current state of the game
    public GameState CurrentState {get; private set;} = GameState.BootStrap;
 
    /// <summary>
    /// Changes the current game state and triggers corresponding logic
    /// only if the new state is different from the current state.
    /// </summary>
    /// <param name="newState">The new game state to switch to.</param>
    public void ChangeGameState(GameState newState)
    {
        // Early exit if already in this state
        if (newState == CurrentState)
            return;
   
        // Update the current state and manage scenes
        CurrentState = newState;

        // Inform subscribers about the state change.
        onGameStateChanged?.Invoke(CurrentState);

        ManageGameState();

    }//end ChangeGameState
    
    /// <summary>
    /// Executes the logic associated with the current game state.
    /// This method is called whenever ChangeGameState() updates the state.
    /// </summary>
    /// <summary>
    /// Executes the logic associated with the current game state.
    /// This method is called whenever ChangeGameState() updates the state.
    /// </summary>

    /// <summary>
    /// Executes the logic associated with the current game state.
    /// This method is called whenever ChangeGameState() updates the state.
    /// </summary>
    private void ManageGameState()
    {
        // Unload all previously loaded scenes
        
        switch (CurrentState)
        {
            case GameState.MainMenu:
                UnloadAllScenes();
                Debug.Log("Game State: MainMenu");
                LoadScene(_mainMenuScene);
                break;

            case GameState.GamePlay:
                UnloadAllScenes();
                Debug.Log("Game State: GamePlay");
                
                //Load game level
                LoadScene(_gameLevels[_currentLevelIndex]);
   
                // Load the HUD as an overlay without setting it as the current scene
                LoadScene(_hudScene, false); 
                break;

            case GameState.GameOver:
                UnloadAllScenes();
                Debug.Log("Game State: GameOver");
                LoadScene(_gameOverScene);
                break;

            case GameState.GamePaused:
                Debug.Log("Game State: Paused");
                LoadScene(_pauseMenuScene);
                break;

            default:
                Debug.LogError($"[GameManager] Unknown GameState: {CurrentState}. No scenes loaded.");
                break;

        }//end switch(CurrentState)

    }//end ManageGameState()
    
    /// <summary>
    /// Loads a scene additively and tracks it in the _loadedScenes list.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    /// <param name="setAsCurrent">
    /// If true, sets this scene as the current primary scene (e.g., main level or menu).
    /// If false, the scene is treated as an overlay (e.g., HUD, pause menu) and does not become the current scene.
    /// </param>
    private void LoadScene(string sceneName, bool setAsCurrent = true)
    {
        // Load the scene additively so existing scenes are preserved
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    
        // Track the loaded scene
        _loadedScenes.Add(sceneName);

        // Optionally set as current scene
        if (setAsCurrent)
        {
            _currentScene = sceneName;
        }

    } // end LoadScene()
    
    /// <summary>
    /// Unloads a single scene and removes it from the _loadedScenes list if present.
    /// </summary>
    /// <param name="sceneName">The name of the scene to unload.</param>
    private void UnloadScene(string sceneName)
    {
        //Reference to "this" scene being passed
        Scene thisScene = SceneManager.GetSceneByName(sceneName);

        // Checks if "this" scene is loaded and unloads
        if (thisScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);

        }//end if (scene.isLoaded)
    

        // Safely remove scene from list if it exists
        if (_loadedScenes.Contains(sceneName))
        {
            _loadedScenes.Remove(sceneName);
        
        }//end if(_loadedScenes.Contains(sceneName))
    
    }//end UnloadScene()
    
    /// <summary>
    /// Unloads all currently loaded scenes except persistent ones
    /// and clears the _loadedScenes list.
    /// </summary>
    private void UnloadAllScenes()
    {
        foreach (string sceneName in new List<string>(_loadedScenes))
        {
            // Unload each scene safely
            UnloadScene(sceneName);
        }

        // Clear the list to remove any remaining references
        _loadedScenes.Clear();
    
    }//end UnloadAllScenes()
    
    /// <summary>
    /// Loads the next level in the levelScenes list while keeping the player in the GamePlay state.
    /// Unloads the current level, updates the current level index, and tracks the newly loaded scene.
    /// </summary>
    public void LoadNextLevel()
    {
        // Unload the current level scene if one is loaded
        if (_currentScene != null && _loadedScenes.Contains(_currentScene))
        {
            UnloadScene(_currentScene);

        }//end if(_currentScene)

        // Increment level index
        _currentLevelIndex++;

        // If no more levels, reset index, switch to GameOver, and exit
        if (_currentLevelIndex >= _gameLevels.Count)
        {
            _currentLevelIndex = 0;
            ChangeGameState(GameState.GameOver);
            return;

        }//end

        // Load the next level and set it as the current scene
        LoadScene(_gameLevels[_currentLevelIndex]);

        Debug.Log($"Loaded Level: {_gameLevels[_currentLevelIndex]}");

    }//end LoadNextLevel()
    
    public void PauseGame()
    {
        if (CurrentState == GameState.GamePlay)
        {
            ChangeGameState(GameState.GamePaused);
        } else
        {
            UnloadScene(_pauseMenuScene);
            // Intentionally don't use ChangeGameState(), because this causes the GameManager to reload the game level.
            CurrentState = GameState.GamePlay;
            onGameStateChanged?.Invoke(CurrentState);
        }
    }

    private void Update()
    {
        //Debug.Log(SceneManager.GetActiveScene().name);   
    }
}//end GameManager
