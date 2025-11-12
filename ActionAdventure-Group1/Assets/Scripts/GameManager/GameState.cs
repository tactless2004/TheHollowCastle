/************************************************************
 * COPYRIGHT:  Year
 * PROJECT: Name of Project or Assignment
 * FILE NAME: GameState.cs
 * DESCRIPTION: Short Description of script.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2000/01/01 | Your Name | Created class
 * 2025/11/4 | Chase Cone | Created from tutorial
 * 2025/11/12 | Chase Cone | Added paused state
 ************************************************************/
 
public enum GameState
{
    BootStrap,  // Initial boot state
    MainMenu,   // Main menu screen
    GamePlay,   // Active gameplay
    GamePaused, // Pause state
    GameOver    // Game over screen
}