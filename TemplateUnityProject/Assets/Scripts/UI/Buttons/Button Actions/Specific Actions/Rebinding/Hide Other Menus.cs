using UnityEngine;

/// <summary>
/// This is a simple script that hides all other menus in the list except
/// for the chosen one (objs specifically)
/// 
/// REM-i
/// </summary>
public class HideOtherMenus : ButtonBase
{
    #region Vars

    [Tooltip("This is the obj that will be shown when the button is clicked")]
    [SerializeField]
    private GameObject chosenObj;

    [Tooltip("This is all of the other objs that we want to hide when the button is pressed")]
    [SerializeField]
    private GameObject[] otherObjs;

    #endregion

    #region Methods

    /// <summary>
    /// On the button being pressed, we show specific menu and hide others
    /// </summary>
    protected override void OnButtonPressed()
    {
        // Show chosen obj
        chosenObj.SetActive(true);

        // Hide all the other objs
        foreach (GameObject obj in otherObjs)
        {
            obj.SetActive(false);
        }
    }

    #endregion
}
