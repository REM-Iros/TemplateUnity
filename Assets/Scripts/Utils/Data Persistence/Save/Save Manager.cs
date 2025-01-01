using FMODUnity;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Save manager singleton will directly handle saving and loading data, pulling from files and
/// storing the data in the data manager. The save manager is not to be interfaced with by
/// other scripts and simply handles the saving and loading of data, the data manager will
/// be the one that every script will reference when they need save data. 
/// 
/// There will also be saving and loading through encryption, which is really not necessary, but it sounded like
/// a fun challenge to learn on the side so here we are.
/// 
/// Basic idea of save load system from here: https://www.youtube.com/watch?v=aUi9aijvpgs
/// 
/// REM-i
/// </summary>
public class SaveManager : EagerSingleton<SaveManager>
{
    #region Vars

    #region Save Vars

    // Set the path
    private const string _saveName = "game_save_file";

    // File handler for getting the data
    private FileHandler _handler;

    // Encryption bool, set to true for encryption, false for no, default is true
    private bool _encryptSave = true;

    // Const variable that determines how many save files the manager checks for
    private const int _maxSaveFileIndex = 21;

    #endregion



    #endregion

    #region Methods

    /// <summary>
    /// Start creates the file handler and immediately pulls data
    /// </summary>
    private void Start()
    {
        // Init handler
        _handler = new FileHandler(Application.persistentDataPath, _saveName, _encryptSave);

        LoadPreviews();
    }

    /// <summary>
    /// This loads up the previews for each of the save file slots
    /// </summary>
    private void LoadPreviews()
    {
        // TODO: Implement
    }

    /// <summary>
    /// Save method called from menus, as well as autosave to store data to json.
    /// </summary>
    /// <param name="index"></param>
    public void SaveGame(int index)
    {
        _handler.Save(DataManager.Instance.GameData, index);
    }

    /// <summary>
    /// Load method called from the menus, checks for the save game in the index and loads it
    /// </summary>
    public void LoadGame(int index)
    {
        DataManager.Instance.LoadGameData(_handler.Load(index));

        if (DataManager.Instance.GameData == null)
        {
            Debug.LogError($"Could not retrieve game data from path at index: {_saveName}_{index}.json");
        }
    }

    #endregion
}
