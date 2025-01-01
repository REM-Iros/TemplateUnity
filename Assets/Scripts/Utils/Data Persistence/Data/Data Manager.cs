using UnityEngine;

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
    private void Start()
    {
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
