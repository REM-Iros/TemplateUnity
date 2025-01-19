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

    // Set the path
    private const string _saveName = "game_save_file";

    // File handler for getting the data
    private FileHandler _handler;

    // Encryption bool, set to true for encryption, false for no, default is true
    private bool _encryptSave = true;

    // Const variable that determines how many save files the manager checks for
    private const int _maxSaveFileIndex = 6;

    // Private array for having game data loaded in
    private GameData[] _files;

    #endregion

    #region Methods

    /// <summary>
    /// Start creates the file handler and immediately pulls data
    /// </summary>
    private void Start()
    {
        // Init handler
        _handler = new FileHandler(Application.persistentDataPath, _saveName, _encryptSave);

        // Offset because we have an array
        _files = new GameData[_maxSaveFileIndex - 1];

        LoadAllSavesOnStartup();
    }

    /// <summary>
    /// On a new game being called, the save manager will load the basic game
    /// data into the data manager.
    /// </summary>
    public void NewGame()
    {
        ServiceLocator.Get<DataManager>().LoadGameData(new GameData());
    }

    /// <summary>
    /// Method called on startup that loads in all saves to generate previews for the player
    /// NOTE: Might need to generate separate previews later, but for now, this seems fine.
    /// I don't think the JSON data will get soooo large that it kills performance.
    /// </summary>
    private void LoadAllSavesOnStartup()
    {
        // Load every file designated
        for (int i = 1; i < _maxSaveFileIndex; i++)
        {
            _files[i - 1] = _handler.Load(i);
        }
    }

    /// <summary>
    /// Save method called from menus, as well as autosave to store data to json
    /// </summary>
    /// <param name="index"></param>
    public void SaveGame(int index)
    {
        _handler.Save(DataManager.Instance.GameData, index);
    }

    /// <summary>
    /// Load method called from the menus, checks for the save game in the index and loads it
    /// </summary>
    public bool LoadGame(int index)
    {
        if (_files[index] != null)
        {
            ServiceLocator.Get<DataManager>().LoadGameData(_files[index]);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Method is called when load buttons start up and provides basic info for the save file
    /// </summary>
    /// <param name="index"></param>
    public void LoadFileSnapshot(int index)
    {

    }

    #endregion
}
