using UnityEngine;


/// <summary>
/// This is the gamedata that is saved and loaded in the game. It gets opened or created by
/// the Save Manager, and then passed on to the Data Manager. This will likely need to be updated
/// throughout the development cycle.
/// 
/// REM-i
/// </summary>
[System.Serializable]
public class GameData
{
    #region Vars

    #region Current Scene Values

    //Current Scene Data Values
    public Vector3 currPlayerPosition;
    public string currPlayerScene;

    #endregion

    public int someVal;

    #endregion

    /// <summary>
    /// Constructor used for defining default game data values when
    /// no data is found or a new game is created.
    /// </summary>
    public GameData()
    {
        //Store the current scene data
        currPlayerPosition = Vector3.zero;
        currPlayerScene = "";

        someVal = 0;
    }
}
