/************************************************************
 * COPYRIGHT:  Year
 * PROJECT: Name of Project or Assignment
 * FILE NAME: GameManager.cs
 * DESCRIPTION: Short Description of script.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2000/01/01 | Your Name | Created class
 * 2025/11/4 | Chase Cone |  Created Class
 *2025/11/12 |  Chase Cone | Updated Functionality and added to bootstrap
 * 2025/11/16 | Chase Cone | Added scene manager functionality
 ************************************************************/
 
using System.Collections.Generic; 
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
   
    [SerializeField]
    [Tooltip("All the level scenes in the game, in the order they should be played.")]
    private List<string> _gameLevels = new List<string>();
   
    
    // Index of the currently active level in the levelScenes list
    private int _currentLevelIndex = 0;
   
    // Tracks the currently loaded primary scene (menu or level)
    private string _currentScene;
   
    //List of all loaded scenes
    private List<string> _loadedScenes = new List<string>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Set the initial game state to Main Menu
        ChangeGameState(GameState.MainMenu);

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
    private void ManageGameState()
    {
        // Unload all previously loaded scenes
        UnloadAllScenes();
        
        switch (CurrentState)
        {
            case GameState.MainMenu:
                Debug.Log("Game State: MainMenu");
                LoadScene(_mainMenuScene);
                break;

            case GameState.GamePlay:
                Debug.Log("Game State: GamePlay");
                
                //Load game level
                LoadScene(_gameLevels[_currentLevelIndex]);
   
                // Load the HUD as an overlay without setting it as the current scene
                LoadScene(_hudScene, false); 
                break;

            case GameState.GameOver:
                Debug.Log("Game State: GameOver");
                LoadScene(_gameOverScene);
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
    
    
    
    
 
}//end GameManager
