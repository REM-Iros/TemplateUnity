using System;
using System.IO;
using UnityEngine;

public class FileHandler
{
    #region Vars

    // Directory path and file name strings
    private string _path = "";
    private string _fileName = "";

    // Encryption vars, when you call the constructor, set the encryption to true if you want to encrypt
    private bool _encryptData = false;
    private readonly string encryptionCode = "jD9rLk2w8Z#nP$9T5m1G!8Vv";

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    public FileHandler(string path, string fileName, bool encryptData)
    {
        _path = path;
        _fileName = fileName;
        _encryptData = encryptData;
    }

    #endregion

    #region Methods

    /// <summary>
    /// This takes in the gamedata object to save, and writes it to json. Index is for multiple save file support.
    /// </summary>
    /// <param name="data"></param>
    public void Save(GameData data, int index)
    {
        // Get the index and the extension attached to the file
        string indexedFileName = $"{_fileName}_{index}.json"; 

        // Get full path and combine to ensure it works on all devices with different line endings
        string fullPath = Path.Combine(_path, indexedFileName);

        try
        {
            // Create directory to write to if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize the data to Json
            string dataToStore = JsonUtility.ToJson(data);

            // Optional encrypt data
            if (_encryptData)
            {
                dataToStore = Encrypt(dataToStore);
            }

            // Open/Create the file from the path, and write to it
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error when trying to save to file: {fullPath}, {e}");
        }
    }

    /// <summary>
    /// This loads the file if it is present, and returns the game data if found.
    /// </summary>
    public GameData Load(int index)
    {
        // Get the index and the extension attached to the file
        string indexedFileName = $"{_fileName}_{index}.json";

        // Create the full path
        string fullPath = Path.Combine(_path, indexedFileName);
        GameData data = null;

        // Checks if the file even exists.
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                // Open the file and read the data
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Optional decrypt data
                if (_encryptData)
                {
                    dataToLoad = Encrypt(dataToLoad);
                }

                // Convert the data from json to game data
                data = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error when trying to load from file: {fullPath}, {e}");
            }
        }

        return data;
    }

    /// <summary>
    /// Takes in the data and XOR's it to encrypt/decrypt it
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private string Encrypt(string data)
    {
        string cryptedData = "";

        //Loop through and xor data
        for (int i = 0; i < data.Length; i++)
        {
            cryptedData += (char)(data[i] ^ encryptionCode[i % encryptionCode.Length]);
        }

        return cryptedData;
    }

    #endregion
}
