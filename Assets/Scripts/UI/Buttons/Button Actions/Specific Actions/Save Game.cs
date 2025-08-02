using UnityEngine;

/// <summary>
/// This is a simple button script that causes the game to save.
/// 
/// REM-i
/// </summary>
public class SaveGame : ButtonBase
{
    /// <summary>
    /// Calls the savemanager and saves the game.
    /// </summary>
    protected override void OnButtonPressed()
    {
        //ServiceLocator.Get<SaveManager>().
    }
}
