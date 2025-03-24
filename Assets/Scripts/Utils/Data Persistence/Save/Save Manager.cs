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
    private readonly bool _encryptSave = true;

    // Const variable that determines how many save files the manager checks for
    private const int _maxSaveFileIndex = 6;

    // Private array for having game data loaded in
    private GameData[] _files;

    // Store the array for the save file
    private int _fileIndex;

    #endregion

    #region Methods

    /// <summary>
    /// Start creates the file handler and immediately pulls data
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

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

    #region File Methods

    /// <summary>
    /// Called on startup of the game, checks if we have any files available
    /// </summary>
    /// <returns></returns>
    public bool IsThereAvailableFile()
    {
        // If we have a file, return true
        for (int i = 0; i < _files.Length; i++) 
        {
            if (_files != null)
            {
                return true;
            }
        }

        // Otherwise, we don't have an available file
        return false;
    }

    /// <summary>
    /// Method is called when load menu opens up, returns basic info
    /// </summary>
    /// <param name="index"></param>
    public void LoadFirstFileSnapshot()
    {
        // Iterate until we find the first non-null file and return that index
        for (int i = 0; i < _files.Length; i++)
        {
            if (_files[i] != null)
            {
                _fileIndex = i;
                break;
            }
        }
    }

    /// <summary>
    /// These next three methods return some values for the snapshots.
    /// There may be a better way to do this such as defining a snapshot
    /// class that you fill up, but I'm unsure.
    /// </summary>
    /// <returns></returns>
    public int GetSnapshotIndex()
    {
        return _fileIndex;
    }

    public Vector3 GetSnapshotPos()
    {
        return _files[_fileIndex]._currPlayerPosition;
    }

    public string GetSnapshotScene()
    {
        return _files[_fileIndex]._currPlayerScene;
    }

    public int GetSnapshotValue()
    {
        return _files[_fileIndex]._someVal;
    }

    /// <summary>
    /// This script will check for the next file available based on which direction the
    /// enum passed in states
    /// </summary>
    /// <param name="lAndR"></param>
    /// <returns></returns>
    public bool FindNextAvailableFile(LeftRightNav lAndR)
    {
        // Define some temp variables for storing info
        bool _isFound = false;
        int _iterationCount = _maxSaveFileIndex - 1;
        int _tempIndex = _fileIndex;

        // Iterate backwards until we find next available file
        while (!_isFound)
        {
            _tempIndex = (lAndR == LeftRightNav.Left ? _tempIndex-- : _tempIndex++);   

            // Wrap around if we go past the boundary
            if (_tempIndex < 0)
            {
                _tempIndex = _maxSaveFileIndex - 1;
            }
            else if (_tempIndex > _maxSaveFileIndex - 1)
            {
                _tempIndex = 0;
            }

            // If we find a non-empty file, change the index to that
            if (_files[_tempIndex] != null)
            {
                _isFound = true;
                _fileIndex = _tempIndex;
            }
            // Otherwise, we decrement the amount of times we want to iterate
            else
            {
                _iterationCount--;

                // Prevents inf loops
                if (_iterationCount == 0)
                {
                    Debug.Log("Save Manager could not find another file");
                    break;
                }
            }
        }

        return _isFound;
    }

    #endregion

    #endregion
}
