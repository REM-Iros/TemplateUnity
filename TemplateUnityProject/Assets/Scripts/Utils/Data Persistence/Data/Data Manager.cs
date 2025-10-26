/// <summary>
/// Data manager is for storing the current games data, and providing it when
/// a request is made to it. Should hold global data for progression and such,
/// not minor data that doesn't need to persist between scenes.
/// 
/// REM-i
/// </summary>
public class DataManager : EagerSingleton<DataManager>
{
    #region Vars

    // Gamedata var that the game will reference to perform actions
    public GameData GameData { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// Start inits the gamedata as null
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        GameData = null;
    }

    /// <summary>
    /// This method takes in gamedata and sets the data in this manager to equal it, pulled from loading the game.
    /// </summary>
    public void LoadGameData(GameData loadedData)
    {
        GameData = loadedData;
    }

    #endregion
}
