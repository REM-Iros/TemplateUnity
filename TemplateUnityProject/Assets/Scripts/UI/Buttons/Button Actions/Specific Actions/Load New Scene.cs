using UnityEngine;

/// <summary>
/// This is a basic button script that loads up a new scene.
/// 
/// REM-i
/// </summary>
public class LoadNewScene : ButtonBase
{
    [Tooltip("This string is the name of the scene you want to change to.")]
    [SerializeField, Header("Scene Name")]
    private string _newSceneName;

    /// <summary>
    /// When the button is pressed, we use the service locator to
    /// change the scene to the scene given in the string we have stored.
    /// </summary>
    protected override void OnButtonPressed()
    {
        ServiceLocator.Get<SceneControlManager>().ChangeScene(_newSceneName);
    }
}
