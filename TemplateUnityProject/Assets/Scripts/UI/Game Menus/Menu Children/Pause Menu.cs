using UnityEngine;

/// <summary>
/// This script handles the pause menu functionality. It should be used to pause the game,
/// and handle any UI elements related to the pause state.
/// 
/// REM-i
/// </summary>
public class PauseMenu : MenuBase
{
    /// <summary>
    /// On open, we need to set the game state and pause the game.
    /// </summary>
    public override void Open()
    {
        base.Open();

        // Pause the game time
        ServiceLocator.Get<GameStateManager>().SetTimeScale(0f);

        // Set the game state to paused
        ServiceLocator.Get<GameStateManager>().SetGameState(GameState.Paused);
    }

    public override void Close()
    {
        base.Close();

        // Resume the game time
        ServiceLocator.Get<GameStateManager>().SetTimeScale(1f);

        // Set the game state to playing
        ServiceLocator.Get<GameStateManager>().SetGameState(GameState.Playing);
    }
}
