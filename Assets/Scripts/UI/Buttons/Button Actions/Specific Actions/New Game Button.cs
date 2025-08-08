/// <summary>
/// New game button script to be... attached to
/// the new game button. Calls the Save Manager
/// to create a new game, and scene manager to change
/// the scene.
/// 
/// REM-i
/// </summary>
public class NewGameButton : ButtonBase
{
    /// <summary>
    /// Called by on button click, calls the save manager to
    /// create a new game.
    /// </summary>
    protected override void OnButtonPressed()
    {
        // Call the new game method to set the values of the data manager
        ServiceLocator.Get<SaveManager>().NewGame();

        // Set the game state to playing
        ServiceLocator.Get<GameStateManager>().SetGameState(GameState.Playing);

        // Load up the first scene for the new game
        ServiceLocator.Get<SceneControlManager>().ChangeScene(ServiceLocator.Get<DataManager>().GameData.currPlayerScene);
    }
}
