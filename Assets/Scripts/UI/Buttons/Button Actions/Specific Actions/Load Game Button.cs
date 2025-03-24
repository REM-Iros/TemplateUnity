using UnityEngine;

/// <summary>
/// The Load Game button script is called
/// by the buttons that appear in the load menu,
/// passing in the index for the game if it is present.
/// 
/// REM-i
/// </summary>
public class LoadGameButton : ButtonBase
{

    /// <summary>
    /// When the button is pressed, get the savemanager's file from that index, and then load
    /// the players current scene
    /// </summary>
    protected override void OnButtonPressed()
    {
        

        // Check for if the load file is present at the index
        if (!ServiceLocator.Get<SaveManager>().LoadGame(ServiceLocator.Get<SaveManager>().GetSnapshotIndex()))
        {
            Debug.LogError($"Save file at index {ServiceLocator.Get<SaveManager>().GetSnapshotIndex()} could not be found.");
            return;
        }

        // Change to the next scene
        ServiceLocator.Get<SceneControlManager>().ChangeScene(ServiceLocator.Get<DataManager>().GameData._currPlayerScene);
    }
}
