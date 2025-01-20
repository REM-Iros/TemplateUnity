using UnityEngine;

/// <summary>
/// When pressed, calls the event that checks whether we have save files
/// when this button is pressed.
/// 
/// REM-i
/// </summary>
public class LoadMenuInitButton : ButtonBase
{
    /// <summary>
    /// On the button being pressed, we call the event.
    /// </summary>
    protected override void OnButtonPressed()
    {
        EventManager.InvokeMMENU_ChangeLoadSnapshot();
    }
}
