using UnityEngine;
using TMPro;

/// <summary>
/// This is the health bar component that is universally used for all actors (minus perhaps boss health bars). This
/// should display the health of an actor in combat, and update when damage is taken. This does not handle HP logic, however.
/// 
/// REM-i
/// </summary>
public class HealthBar : UISliderParent
{
    #region Vars

    [Tooltip("This is the text component that displays the health value.")]
    [SerializeField] 
    private TextMeshProUGUI _hpText;

    #endregion

    #region Methods

    /// <summary>
    /// Override to add health text update functionality.
    /// </summary>
    /// <param name="newValue"></param>
    public override void UpdateSliderValue(float newValue)
    {
        base.UpdateSliderValue(newValue);

        UpdateHealthSlider();
    }

    /// <summary>
    /// Called when the health value is updated, updates the text display accordingly.
    /// </summary>
    private void UpdateHealthSlider()
    {
        _hpText.text = _uiSlider.value + "/" + _uiSlider.maxValue;
    }

    #endregion
}
