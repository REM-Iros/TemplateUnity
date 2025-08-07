using TMPro;
using UnityEngine;

/// <summary>
/// Fill in the information for the save button, and enable it to call the save manager to
/// save information and update the button.
/// 
/// REM-i
/// </summary>
public class PopulateSaveButton : ButtonBase
{
    #region Vars

    // The save index will be set on instantiation and is the save file this script is pointing to.
    private int _saveIndex;

    [Tooltip("This is the text field that will display the save name.")]
    [SerializeField, Header("Text Fields")]
    private TMP_Text _saveNameText;

    [Tooltip("This is the text field that will display the current position.")]
    [SerializeField]
    private TMP_Text _currentPositionText;

    #endregion

    #region Methods

    /// <summary>
    /// Called to set the save index by the generate save buttons script
    /// </summary>
    /// <param name="index"></param>
    public void SetSaveIndex(int index)
    {
        _saveIndex = index;
    }

    /// <summary>
    /// When the button is pressed, this method will be called to perform the save action.
    /// </summary>
    protected override void OnButtonPressed()
    {
        // Save the game at the index
        ServiceLocator.Get<SaveManager>().SaveGame(_saveIndex);

        // Populate the save fields with the new data from the save manager
        PopulateSaveFields();
    }

    /// <summary>
    /// This method will populate the save fields with the data from the save manager.
    /// </summary>
    public void PopulateSaveFields()
    {
        // Index + 1 to make it human readable
        _saveNameText.text = "Save: " + (_saveIndex + 1);

        if (ServiceLocator.Get<SaveManager>().Files[_saveIndex] != null)
        {
            // Get the current save data
            GameData saveData = ServiceLocator.Get<SaveManager>().Files[_saveIndex];

            // Set the current position text
            _currentPositionText.text = "Position: " + saveData.currPlayerPosition.ToString("F2");
        }
        else
        {
            // If no data, set to empty
            _currentPositionText.text = "Empty";
        }
    }

    #endregion
}
