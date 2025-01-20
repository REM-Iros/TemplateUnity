using UnityEngine;

/// <summary>
/// Calls the save manager to change which save file we
/// are currently on.
/// 
/// REM-i
/// </summary>
public class SwitchCurrentLoadButton : ButtonBase
{
    [SerializeField]
    private LeftRightNav lRNav;

    /// <summary>
    /// When the button is pressed, calls the save manager to switch to the left or
    /// right save file
    /// </summary>
    protected override void OnButtonPressed()
    {
        ServiceLocator.Get<SaveManager>().FindNextAvailableFile(lRNav);
    }
}
