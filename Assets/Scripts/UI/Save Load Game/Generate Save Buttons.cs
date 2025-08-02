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
        for (int i = 0; i < ServiceLocator.Get<SaveManager>().MaxSaveFileIndex; i++)
        {
            // Check if we have a save at index
            if (ServiceLocator.Get<SaveManager>().)
        }
    }

    #endregion
}
