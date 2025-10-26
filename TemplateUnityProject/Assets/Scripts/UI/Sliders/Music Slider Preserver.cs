using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets the audio value of the sliders based on the playerprefs values for the different values.
/// This should store all the sliders, and should update the value based on what is stored.
/// 
/// REM-i
/// </summary>
public class MusicSliderPreserver : MonoBehaviour
{
    #region Slider Variables

    [Tooltip("This is the slider that handles the master volume")]
    [SerializeField, Header("Sliders")]
    private Slider _masterVolume;

    [Tooltip("This is the slider that handles the music volume")]
    [SerializeField]
    private Slider _musicVolume;

    [Tooltip("This is the slider that handles the sfx volume")]
    [SerializeField]
    private Slider _sfxVolume;

    #endregion

    /// <summary>
    /// When this is enabled, either set the volume to the playerpref, or to 1
    /// </summary>
    private void OnEnable()
    {
        // Set the volumes on enable
        if (_masterVolume != null)
        {
            _masterVolume.value = PlayerPrefs.GetFloat("MasterVolume", 100f);
            _masterVolume.onValueChanged.AddListener(value => UpdateMasterVolume(value));
        }

        if (_musicVolume != null)
        {
            _musicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 100f);
            _musicVolume.onValueChanged.AddListener(value => UpdateMusicVolume(value));
        }

        if (_sfxVolume != null)
        {
            _sfxVolume.value = PlayerPrefs.GetFloat("SFXVolume", 100f);
            _sfxVolume.onValueChanged.AddListener(value => UpdateSFXVolume(value));
        }
    }

    /// <summary>
    /// This is called every time the master slider changes
    /// </summary>
    /// <param name="volume"></param>
    private void UpdateMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// This is called every time the music slider changes
    /// </summary>
    /// <param name="volume"></param>
    private void UpdateMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// This is called every time the music slider changes
    /// </summary>
    /// <param name="volume"></param>
    private void UpdateSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// When this is disabled, unsubscribe from the value change events
    /// </summary>
    private void OnDisable()
    {
        // Set the volumes on enable
        if (_masterVolume != null)
        {
            _masterVolume.onValueChanged.RemoveListener(UpdateMasterVolume);
        }

        if (_musicVolume != null)
        {
            _musicVolume.onValueChanged.RemoveListener(UpdateMusicVolume);
        }

        if (_sfxVolume != null)
        {
            _sfxVolume.onValueChanged.RemoveListener(UpdateSFXVolume);
        }
    }
}
