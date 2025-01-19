using UnityEngine;

/// <summary>
/// Simple application quit script
/// 
/// REM-i
/// </summary>
public class ExitGameButton : ButtonBase
{
    /// <summary>
    /// Close the game down
    /// </summary>
    protected override void OnButtonPressed()
    {
        Application.Quit();
    }
}
