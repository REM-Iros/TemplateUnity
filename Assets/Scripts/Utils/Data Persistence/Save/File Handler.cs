using System.IO;

public class FileHandler
{
    #region Vars

    //Directory path and file name strings
    private string _path = "";
    private string _fileName = "";

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    public FileHandler(string path, string fileName)
    {
        _path = path;
        _fileName = fileName;
    }

    #endregion
}
