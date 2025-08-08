using System;
using UnityEngine;

/// <summary>
/// This is the game state manager, which is used to manage the state of the game.
/// Scripts will use this to check the state of the game, such as whether it is paused,
/// in a menu, a cutscene, in gameplay, etc.
/// 
/// REM-i
/// </summary>
public class GameStateManager : EagerSingleton<GameStateManager>
{
    public GameState CurrentGameState { get; private set; } = GameState.Playing;
    public event Action<GameState> OnGameStateChanged;

    /// <summary>
    /// This is called when the game needs to change the game state.
    /// </summary>
    /// <param name="newState"></param>
    public void SetGameState(GameState newState)
    {
        // If the new state is the same as the current state, we don't need to do anything
        if (CurrentGameState == newState)
        {
            return;
        }

        // If the new state is not the same as the current state, we need to change it
        CurrentGameState = newState;

        // Invoke the event to notify any listeners that the game state has changed
        OnGameStateChanged?.Invoke(CurrentGameState);
    }

    /// <summary>
    /// Called by other scripts to set the timescale of the game
    /// </summary>
    /// <param name="timeScale"></param>
    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
