using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a parent class for UI sliders in the RPG game. It can be extended to create specific slider types such as health bars, mana bars, etc.
/// 
/// REM-i
/// </summary>
public class UISliderParent : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the UI slider that will be used for the script.")] 
    [SerializeField, Header("UI Components")]
    protected Slider _uiSlider;

    [Tooltip("This is the maximum value for the slider.")]
    protected float _maxValue = 100f;

    #endregion

    #region Methods

    /// <summary>
    /// This method is used to initialize the slider with a maximum value. It can be overriden by child classes for specific initialization logic.
    /// </summary>
    /// <param name="maxValue"></param>
    public virtual void Initialize(float maxValue)
    {
        // Set the maximum value
        _maxValue = maxValue;

        // Check if the UI slider is assigned, return error if not
        if (_uiSlider == null)
        {
            Debug.LogError("UI Slider is not assigned in the inspector.");
            return;
        }

        // Set the maximum value of the slider
        _uiSlider.maxValue = _maxValue;
    }

    /// <summary>
    /// Basic helper method to set the current slider value.
    /// </summary>
    /// <param name="valueToAdd"></param>
    public virtual void UpdateSliderValue(float newValue)
    {
        _uiSlider.value = Mathf.Clamp(newValue, 0, _maxValue);
    }

    #endregion
}
