using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a database singleton for all icons that would be used in UI. It should be easily accessible for any element that needs to reference it.
/// 
/// REM-i
/// </summary>
[CreateAssetMenu(fileName = "UIIconDatabase", menuName = "ScriptableObjects/UI/UIIconDatabase")]
public class UIControlIconDatabase : ScriptableObject
{
    private static UIControlIconDatabase _instance;
    public static UIControlIconDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<UIControlIconDatabase>("IconDatabase");
                _instance.InitializeIconDatabase();
            }
            return _instance;
        }
    }

    [SerializeField]
    private List<UIControlIconEntry> entries;
    
    [Tooltip("This is the dictionary that holds the icons for the UI. The key is the path of the icon, and the value is the sprite for the icon.")]
    private Dictionary<UIIconKey, Sprite> _iconDictionary;

    /// <summary>
    /// Called on creation, sets up the icon database by loading all icons from the resources folder and adding them to the dictionary. This is called on first access to the instance of the database.
    /// </summary>
    private void InitializeIconDatabase()
    {
        // If the dictionary is already initialized, we don't need to do anything
        if (_iconDictionary != null)
        {
            return;
        }

        // Initialize the dictionary
        _iconDictionary = new Dictionary<UIIconKey, Sprite>(entries.Count);

        // Iterate through all entries and add them to the dictionary if they are unique.
        foreach (var entry in entries)
        {
            var key = entry.key;

            if (_iconDictionary.ContainsKey(key))
            {
                Debug.LogWarning($"Duplicate icon entry found for control scheme: {entry.key.ControlScheme}, control path: {entry.key.ControlPath}. This entry will be skipped.");
            }
            else
            {
                _iconDictionary.Add(key, entry.icon);
            }
        }
    }

    /// <summary>
    /// This is called by external scripts to get the icon for a specific device and control path. It will return the sprite for the icon if it exists in the dictionary, or null if it does not exist.
    /// </summary>
    /// <param name="controlScheme"></param>
    /// <param name="controlPath"></param>
    /// <returns></returns>
    public Sprite GetIcon(string controlScheme, string controlPath)
    {
        UIIconKey key = new UIIconKey { ControlScheme = controlScheme, ControlPath = controlPath };

        // Get the icon from the dictionary using the device and control path as the key. If it exists, return the icon.
        if (_iconDictionary.TryGetValue(key, out var icon))
        {
            return icon;
        }

        Debug.LogWarning($"Icon not found for device: {controlScheme}, control path: {controlPath}. Returning null.");
        return null;
    }
}
