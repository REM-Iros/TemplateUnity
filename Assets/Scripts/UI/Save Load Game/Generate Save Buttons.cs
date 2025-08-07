using UnityEngine;

/// <summary>
/// This UI script on startup will generate save file snapshots/empty saves by
/// pulling from the data manager and save manager.
/// 
/// REM-i
/// </summary>
public class GenerateSaveButtons : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the save block prefab we will instantiate.")]
    [SerializeField, Header("Save Button Prefab")]
    private GameObject _savePrefab;

    [Tooltip("This is the parent gameobject that we need to spawn.")]
    [SerializeField, Header("Parent Object")]
    private GameObject _parentObj;

    #endregion

    #region Method

    /// <summary>
    /// On awake, generate the save prefab blocks.
    /// </summary>
    private void Awake()
    {
        InstantiateSaveButtons();
    }

    /// <summary>
    /// Generate the save blocks.
    /// </summary>
    private void InstantiateSaveButtons()
    {
        // Run through the max save files and generate save blocks with snapshots
        for (int i = 1; i < ServiceLocator.Get<SaveManager>().MaxSaveFileIndex; i++)
        {
            // Spawn the save button in
            GameObject saveButton = Instantiate(_savePrefab, _parentObj.transform);

            // Name the save button
            saveButton.name = "SaveButton_" + (i + 1);

            // Get the SaveButton component for setting details
            PopulateSaveButton snapshotPopulator = saveButton.GetComponent<PopulateSaveButton>();

            // Set the save index for the button
            snapshotPopulator.SetSaveIndex(i);

            // Fill the save fields
            snapshotPopulator.PopulateSaveFields();
        }
    }

    #endregion
}
