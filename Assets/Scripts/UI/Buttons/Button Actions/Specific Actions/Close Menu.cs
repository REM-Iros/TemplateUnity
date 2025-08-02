using UnityEngine;

/// <summary>
/// This script inherits the button base script we wrote earlier and allows the user to 
/// close the menu and continue the game.
/// </summary>
public class CloseMenu : ButtonBase
{
    [Tooltip("This is the parent of the UI Pause menu that we disable when we resume.")]
    [SerializeField, Header("Gameobject to Hide")]
    private GameObject _uiParent;

    /// <summary>
    /// Disables the menu canvas and restarts the timer.
    /// </summary>
    protected override void OnButtonPressed()
    {
        // Continue the game
        Time.timeScale = 1.0f;

        // Disable the UI
        _uiParent.SetActive(false);
    }
}
