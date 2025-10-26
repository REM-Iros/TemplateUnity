using UnityEngine;
using TMPro;

/// <summary>
/// This is a simple load panel that updates
/// the text with the index of the game being
/// pointed to right now.
/// </summary>
public class LoadGamePanel : MonoBehaviour
{
    #region Vars

    // Store the text objects
    [SerializeField] 
    private TextMeshProUGUI _indexText;
    [SerializeField] 
    private TextMeshProUGUI _sceneText;
    [SerializeField] 
    private TextMeshProUGUI _positionText;
    [SerializeField] 
    private TextMeshProUGUI _someValText;

    // Store a gameobject that checks if we have any files available.
    [SerializeField] private GameObject _noFilesFoundPanel;

    // Store the loading info panel too
    [SerializeField] private GameObject _fileInfoPanel;

    #endregion

    #region Methods

    /// <summary>
    /// On awake, we subscribe to the event manager for when the index changes.
    /// </summary>
    private void Awake()
    {
        EventManager.MMENU_ChangeLoadSnapshot += UpdateTexts;

        UpdateTexts();
    }

    /// <summary>
    /// We update the text to reflect the current index of the file
    /// </summary>
    private void UpdateTexts()
    {
        // Check if we have an available file
        if (!ServiceLocator.Get<SaveManager>().IsThereAvailableFile())
        {
            _noFilesFoundPanel.SetActive(true);
            _fileInfoPanel.SetActive(false);
            return;
        }

        ServiceLocator.Get<SaveManager>().LoadFirstFileSnapshot();

        // Update the texts
        _indexText.text = "File: " + (ServiceLocator.Get<SaveManager>().GetSnapshotIndex() + 1);
        _positionText.text = ServiceLocator.Get<SaveManager>().GetSnapshotPos().ToString();
        _sceneText.text = ServiceLocator.Get<SaveManager>().GetSnapshotScene().ToString();
        _someValText.text = ServiceLocator.Get<SaveManager>().GetSnapshotValue().ToString();

    }

    /// <summary>
    /// When this object is destroyed, unsub from the event
    /// </summary>
    private void OnDestroy()
    {
        EventManager.MMENU_ChangeLoadSnapshot -= UpdateTexts;
    }

    #endregion
}
